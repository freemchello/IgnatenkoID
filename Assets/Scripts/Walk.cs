using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Lesson1
{
    public class Walk : MonoBehaviour
    {
        [SerializeField] [Range(0, 360)] private float ViewAngle = 90f;
        [SerializeField] private float ViewDistance = 15f;
        [SerializeField] private float DetectionDistance = 3f;
        [SerializeField] private Transform EnemyEye;
        [SerializeField] private Transform Target;
        [SerializeField] private float rotationSpeed;

        [SerializeField] private float speed;
        [SerializeField] public Transform[] moveSpots;
        [SerializeField] private float startWaitTime;
        private float waitTime;
        private int randomSpot;

        [SerializeField] private NavMeshAgent agent;

        private void Awake()
        {
            waitTime = startWaitTime;
            randomSpot = Random.Range(0, moveSpots.Length);
            agent = GetComponent<NavMeshAgent>();

        }

        private void Start()
        {
            Patrol();
        }

        private void Update()
        {
            float distanceToPlayer = Vector3.Distance(Target.transform.position, agent.transform.position);
            if (distanceToPlayer <= DetectionDistance || IsInView())
            {
                RotateToTarget();
                MoveToTarget();
            }
            else
            {
                Patrol();
            }

        }

        private void RotateToTarget()
        {
            var direction = Target.position - transform.position;
            var stepRotate = Vector3.RotateTowards(transform.forward,
                direction,
                rotationSpeed * Time.fixedDeltaTime,
                0f);
            transform.rotation = Quaternion.LookRotation(stepRotate);
        }

        private bool IsInView()
        {
            float realAngle = Vector3.Angle(EnemyEye.forward, Target.position - EnemyEye.position);
            RaycastHit hit;
            if (Physics.Raycast(EnemyEye.transform.position, Target.position - EnemyEye.position, out hit, ViewDistance))
            {
                if (realAngle < ViewAngle / 2f && Vector3.Distance(EnemyEye.position, Target.position) <= ViewDistance && hit.transform == Target.transform)
                {
                    return true;
                }
            }
            return false;
        }

        private void MoveToTarget()
        {
            agent.SetDestination(Target.position);
        }

        

        private void Patrol()
        {
            agent.SetDestination(moveSpots[randomSpot].position);
            if (Vector3.Distance(agent.nextPosition, moveSpots[randomSpot].position) <= 2f)
            {
                if (waitTime <= 0)
                {
                    randomSpot = Random.Range(0, moveSpots.Length);
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
    }
}
