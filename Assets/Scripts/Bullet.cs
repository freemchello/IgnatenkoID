using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lesson1
{
    public class Bullet : MonoBehaviour
    {
        public Rigidbody _rb;

        [SerializeField] public float _fireForce = 0.01f; //�������� ��������
        [SerializeField] public float lifeTime = 3f;//����� ����� ����
        [SerializeField] public float _bulletDamage = 10f;//���� �� ����

        private void Start()
        {
            this._rb.AddForce(transform.forward * _fireForce, ForceMode.Impulse);
        }

        private void FixedUpdate()
        {
            Destroy(gameObject, lifeTime);
        }

        private void OnTriggerEnter(Collider bullet) //��� �������������� � ���������� ���� �����������
        {
            if (bullet.CompareTag("Player") || bullet.CompareTag("Wall") || bullet.CompareTag("Ground"))
            {
                Debug.Log("DESTROY BULLET");
                Destroy(gameObject);
            }
        }
    }
}
