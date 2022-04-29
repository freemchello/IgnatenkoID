using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Lesson1
{
    public class EnemyHealth : MonoBehaviour
    {
            [SerializeField] public float _enemyHealth = 100f;

        private void OnTriggerEnter(Collider Enemy) // при взаимодействии с коллайдером Игрока
        {
            

            if (Enemy.CompareTag("Mine"))
            {
                GameObject mineObject = GameObject.FindGameObjectWithTag("Mine");
                var mineComponent = mineObject.GetComponent<Smash>();
                float mineDamage = mineComponent._damageMine;

                Debug.Log("MINE DAMAGE(PlayerHealth.cs)");

                _enemyHealth -= mineDamage;
            }
            
        }

        private void Update()
        {
            if (_enemyHealth <= 0)
                Destroy(gameObject);
        }
    } 
}
