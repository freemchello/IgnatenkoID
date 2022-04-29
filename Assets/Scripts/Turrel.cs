using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lesson1
{
    public class Turrel : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private float _speedRotate;

        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _spawnPosition;

        [SerializeField] private float startTimeShots;
        private float timeShots;
        private bool _spawnPoint;
        private bool _LookChange;

        private void Start()
        {

            _player = FindObjectOfType<Player>();

        }
        void Update()
        {
            if (timeShots <= 0)
            {
                timeShots = startTimeShots;
                if (Vector3.Distance(transform.position, _player.transform.position) <= 6)
                {
                    _spawnPoint = true;
                }

            }
            else
            {
                timeShots -= Time.deltaTime;
            }

            if (Vector3.Distance(transform.position, _player.transform.position) <= 7)
            {
                _LookChange = true;
            }
        }

        void FixedUpdate()
        {

            if (_spawnPoint)
            {
                _spawnPoint = false;
                Fire();
            }
            if (_LookChange)
            {
                _LookChange = false;
                Look();
            }

        }
        private void Fire()
        {
            var bulletObj = Instantiate(_bulletPrefab, _spawnPosition.position, _spawnPosition.rotation);
            var bullet = bulletObj.GetComponent<Bullet>();
            


        }
        private void Look()
        {

            var direction = _player.transform.position - transform.position;
            direction.Set(direction.x, 0, direction.z);
            var stepRotate = Vector3.RotateTowards(transform.forward,
                direction,
                _speedRotate * Time.fixedDeltaTime,
                0f);

            transform.rotation = Quaternion.LookRotation(stepRotate);
        }


    }
}