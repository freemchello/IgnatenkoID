
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Lesson1
{
    public class Enemy : MonoBehaviour 
    {
        [SerializeField] private Player _player;
        [SerializeField] private float _speedRotate;

        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _spawnPosition;

        [SerializeField] private float startTimeShots;

        [SerializeField] private float timeShots;
        [SerializeField] private bool _isFire;
        [SerializeField] private bool _isLook;
        [SerializeField] private Animator _anim;

        void Awake()
        {
            _anim = GetComponent<Animator>();
            _player = FindObjectOfType<Player>();
        }

        void Update()
        {
            //_anim.SetBool("IsRotate", ??? != Vector3.zero);

            Ray ray = new Ray(_spawnPosition.position, transform.forward);
           
            if (Physics.Raycast(ray, out RaycastHit hitFire, 6))
            {
                Debug.DrawRay(_spawnPosition.position, transform.forward * hitFire.distance, Color.blue);
                Debug.DrawRay(hitFire.point, hitFire.normal, Color.cyan);

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
            if (Vector3.Distance(transform.position, _player.transform.position) <= 7)
            {
                _isLook = true;
            }
        
        }

        void FixedUpdate()
        {

            if (_isFire)
            {
                _isFire = false;
                Fire();
            }
            if (_isLook)
            {
                _isLook = false;
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
        }
    }
}