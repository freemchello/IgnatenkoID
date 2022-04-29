using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Lesson1
{

    public class Player : MonoBehaviour
    {

        [SerializeField] private float jumpPower = 5f; //сила прыжка
        private readonly Vector3 jumpDirection = Vector3.up; //Прыжок

        public bool isGrounded { get; private set; } //Булевая земли

        private Rigidbody _rb;

        public GameObject shieldPrefab; //Префаб щита
        public GameObject minePrefab; //Префаб мины
        public GameObject swordPrefab;  //Префаб меча***
        public Transform spawnPosition; //позиция спавна щита
        public Transform mineSpawn;//позиция спавна мины

        private bool _isSpawnShield;//булевая щита
        private bool _isSpawnMine;

        public int level = 1;//Уровень игрока

        private Vector3 _direction;
        public float speed = 2f; //скорость игрока
        public float speedRotate = 150f; //скорость обзора мышью
        private bool _isSprint; //булевая бега
        [SerializeField] private Animator _anim;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
        }

        void Start()
        {
            this._rb = this.GetComponent<Rigidbody>(); //Прыжок
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) //Прыжок
                this.Jump(); //Прыжок

            if (Input.GetMouseButtonDown(2))//Назначение клавиши для
                _isSpawnShield = true;//щита(спавн)

            if (Input.GetKeyDown(KeyCode.Q))//Назначение клавиши для
                _isSpawnMine = true;//гранаты(спавн)
            //if (Input.GetKeyDown(KeyCode.Escape))
            //    GameMenu.Pause();

            _direction.x = Input.GetAxis("Horizontal"); //Управление
            _direction.z = Input.GetAxis("Vertical");   //Управление
            _isSprint = Input.GetButton("Sprint");      //Управление(спринт)


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


        private void Jump() //Прыжок
        {
            if (this.isGrounded) //Прыжок
                this._rb.AddForce(this.jumpDirection * this.jumpPower, ForceMode.Impulse); //Прыжок
        }
        private void Move(float delta) //Управление
        {

            var fixedDirection = transform.TransformDirection(_direction);
            transform.position += fixedDirection * (_isSprint ? speed * 2 : speed) * delta;

        }


        private void OnCollisionEnter(Collision other) //Прыжок
        {
            var ground = other.gameObject.GetComponentInParent<Ground>();
            if (ground)
                this.isGrounded = true;
        }

        private void OnCollisionExit(Collision other) //Прыжок
        {
            var ground = other.gameObject.GetComponentInParent<Ground>();
            if (ground)
                this.isGrounded = false;
        }
        

    }
}