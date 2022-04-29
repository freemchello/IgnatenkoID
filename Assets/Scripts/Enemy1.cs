
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;


namespace Lesson1
{
    public class Enemy1 : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private float _speedRotate;
        [SerializeField] private Patrol _patrol;

        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _spawnPosition;

        [SerializeField] private float startTimeShots;

        [SerializeField] private float timeShots;
        [SerializeField] private bool _isFire;
        [SerializeField] private bool _isLook;
       
        [SerializeField] public float speed;
        [SerializeField] private Animator _anim;

        private NavMeshAgent _agent;

        void Awake()
        {
            _anim = GetComponent<Animator>();
            _player = FindObjectOfType<Player>();
            _agent = GetComponent<NavMeshAgent>();
            _patrol = GetComponent<Patrol>();
        }
        //private void Start()
        //{
        //    // _agent.SetDestination(_player.transform.position);
        //}
        void Update()
        {
            _anim.SetBool("IsRuning", _spawnPosition.position != Vector3.zero);

            Ray ray = new Ray(_spawnPosition.position, transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hitFire, 12))
            {
                //Debug.DrawRay(_spawnPosition.position, transform.forward * hitFire.distance, Color.blue);
                //Debug.DrawRay(hitFire.point, hitFire.normal, Color.cyan);

                if (timeShots <= 0)
                {
                    timeShots = startTimeShots;
                    if (hitFire.collider.CompareTag("Player"))
                    {
                        _isFire = true;
                    }

                }
                else
                {
                    timeShots -= Time.deltaTime;
                }

                
            }
            if (Vector3.Distance(transform.position, _player.transform.position) <= 15)
            {
                _patrol.enabled = false;//выкл скрипта патруль
                //Debug.Log("СТОП ПАТРУЛЬ");
                _isLook = true;

            }

        }


        void FixedUpdate()
        {
            if (NavMesh.SamplePosition(_agent.transform.position, out NavMeshHit navMeshHit, 0.2f, NavMesh.AllAreas))
            //print(NavMesh.GetAreaCost((int)Mathf.Log(navMeshHit.mask, 2)));

            if (_isFire)
            {
               _isFire = false;
               Fire();
            }
            if (_isLook)
            {
               _patrol.enabled = true;//вкл скрипта патруль
               _isLook = false;
               //Debug.Log("СТАРТ ПАТРУЛЬ");
               Look();
            }
                
        }
        private void Fire() //Атака
        {
            var bulletObj = Instantiate(_bulletPrefab, _spawnPosition.position, _spawnPosition.rotation);
            var bullet = bulletObj.GetComponent<Bullet>();
        }
        private void Look() //Наблюдение
        {
            var direction = _player.transform.position - transform.position;
            direction.Set(direction.x, 0, direction.z); //ограничение по оси Y
            var stepRotate = Vector3.RotateTowards(transform.forward,
                                                   direction,
                                                   _speedRotate * Time.fixedDeltaTime,
                                                   0f);

            transform.rotation = Quaternion.LookRotation(stepRotate);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        
    }
}