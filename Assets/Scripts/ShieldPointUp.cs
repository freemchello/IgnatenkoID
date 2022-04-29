using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lesson1
{
    public class ShieldPointUp : MonoBehaviour
    {
        public float _mpup = 100f; //���������� ���������� �� ������� �������� �����

        private void OnTriggerEnter(Collider shield) //��� �������������� � ����������� ����
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            var mpPlayer = player.GetComponent<PlayerHealth>();
            float mpCheck = mpPlayer._shieldPoint;

            if (shield.CompareTag("Player") && mpCheck < 100)
            {
                Destroy(gameObject);
            }
        }
    }
}