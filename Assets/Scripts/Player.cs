using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Lesson1
{

    public class Player : MonoBehaviour
    {

        [SerializeField] private float jumpPower = 5f; //���� ������
        private readonly Vector3 jumpDirection = Vector3.up; //������

        public bool isGrounded { get; private set; } //������� �����

        private Rigidbody _rb;

        public GameObject shieldPrefab; //������ ����
        public GameObject minePrefab; //������ ����
        public GameObject swordPrefab;  //������ ����***
        public Transform spawnPosition; //������� ������ ����
        public Transform mineSpawn;//������� ������ ����

        private bool _isSpawnShield;//������� ����
        private bool _isSpawnMine;

        public int level = 1;//������� ������

        private Vector3 _direction;
        public float speed = 2f; //�������� ������
        public float speedRotate = 150f; //�������� ������ �����
        private bool _isSprint; //������� ����
        [SerializeField] private Animator _anim;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
        }

        void Start()
        {
            this._rb = this.GetComponent<Rigidbody>(); //������
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) //������
                this.Jump(); //������

            if (Input.GetMouseButtonDown(2))//���������� ������� ���
                _isSpawnShield = true;//����(�����)

            if (Input.GetKeyDown(KeyCode.Q))//���������� ������� ���
                _isSpawnMine = true;//�������(�����)
            //if (Input.GetKeyDown(KeyCode.Escape))
            //    GameMenu.Pause();

            _direction.x = Input.GetAxis("Horizontal"); //����������
            _direction.z = Input.GetAxis("Vertical");   //����������
            _isSprint = Input.GetButton("Sprint");      //����������(������)


            _anim.SetBool("IsWalking", _direction != Vector3.zero);
            //_anim.SetBool("IsJump", this.isGrounded = false);
            _anim.SetBool("IsSprint", Input.GetKeyDown(KeyCode.LeftShift));

        }

        private void FixedUpdate()
        {
            if (_isSpawnShield)
            {
                _isSpawnShield = false;
                SpawnShield();

            }
            Move(Time.fixedDeltaTime);

            if (Input.GetKey(KeyCode.Mouse1))
            {
                transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * speedRotate * Time.fixedDeltaTime, 0f));

            }

            if (_isSpawnMine)
            {
                _isSpawnMine = false;
                SpawnMine();
            }

        }

        private void SpawnShield()
        {
            var shieldObj = Instantiate(shieldPrefab, spawnPosition.position, spawnPosition.rotation);
            var shield = shieldObj.GetComponent<Shield>();
            //shield.Init(10 * level);
            shield.transform.SetParent(spawnPosition);

        }

        private void SpawnMine()
        {
            var mineObj = Instantiate(minePrefab, mineSpawn.position, mineSpawn.rotation);
            var mine = mineObj.GetComponent<Smash>();
        }


        private void Jump() //������
        {
            if (this.isGrounded) //������
                this._rb.AddForce(this.jumpDirection * this.jumpPower, ForceMode.Impulse); //������
        }
        private void Move(float delta) //����������
        {

            var fixedDirection = transform.TransformDirection(_direction);
            transform.position += fixedDirection * (_isSprint ? speed * 2 : speed) * delta;

        }


        private void OnCollisionEnter(Collision other) //������
        {
            var ground = other.gameObject.GetComponentInParent<Ground>();
            if (ground)
                this.isGrounded = true;
        }

        private void OnCollisionExit(Collision other) //������
        {
            var ground = other.gameObject.GetComponentInParent<Ground>();
            if (ground)
                this.isGrounded = false;
        }
        

    }
}