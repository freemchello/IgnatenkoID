using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lesson1
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] public float _healthPoint = 100f;
        [SerializeField] public float _shieldPoint = 25f;
        [SerializeField] public Text _Health;
        [SerializeField] public Text _Shield;

        private void OnTriggerEnter(Collider Player) // при взаимодействии с коллайдером Игрока
        {
                //float HealthPoint = _healthPoint;
                //float ShieldPoint = _shieldPoint;


            if (Player.CompareTag("HealthUp") && _healthPoint < 100)
            {
                GameObject healthObject = GameObject.FindGameObjectWithTag("HealthUp");
                var hpComponent = healthObject.GetComponent<HealthHeart>();
                float HealthUp = hpComponent._hpup;
                _healthPoint += HealthUp;
                if (_healthPoint >= 100)
                {
                    _healthPoint = 100;
                }
                
            }

            if (Player.CompareTag("ShieldUp"))
            {
                GameObject shieldObj = GameObject.FindGameObjectWithTag("ShieldUp");
                var shieldComponent = shieldObj.GetComponent<ShieldPointUp>();
                float shieldUP = shieldComponent._mpup;

                _shieldPoint += shieldUP;
                if (_shieldPoint >= 100)
                {
                    _shieldPoint = 100;
                }
            }

            if (Player.CompareTag("Mine"))
            {
                GameObject mineObject = GameObject.FindGameObjectWithTag("Mine");
                var mineComponent = mineObject.GetComponent<Smash>();
                float mineDamage = mineComponent._damageMine;
                Debug.Log("MINE DAMAGE(PlayerHealth.cs)");

                if (_shieldPoint > 0)
                {
                    _shieldPoint -= mineDamage;

                    if (_shieldPoint <= 0)
                    {
                        _healthPoint += _shieldPoint;
                    }
                }
                else _healthPoint -= mineDamage;
            }

            if (Player.CompareTag("Bullet"))
            {
               
                GameObject BulletObj = GameObject.FindGameObjectWithTag("Bullet");
                var BulletComponent = BulletObj.GetComponent<Bullet>();
                var enemyDamage = BulletComponent._bulletDamage;
                Debug.Log("ENEMYBULLET DAMAGE(PlayerHealth.cs)");

                if (_shieldPoint > 0)
                {
                    _shieldPoint -= enemyDamage;

                    if (_shieldPoint <= 0)
                    {
                        _healthPoint += _shieldPoint;
                    }
                }
                else _healthPoint -= enemyDamage; 
            }
            
            if (Player.CompareTag("Trap"))
            {
                GameObject TrapObj = GameObject.FindGameObjectWithTag("Trap");
                var TrapComponent = TrapObj.GetComponent<Trap>();
                var trapDamage = TrapComponent._trapDamage;
                Debug.Log("TRAP DAMAGE(PlayerHealth.cs)");

                if (_shieldPoint >= 0)
                {
                    _shieldPoint -= trapDamage;

                    if (_shieldPoint < 0)
                    {
                        _healthPoint += _shieldPoint;
                    }
                }
                else _healthPoint -= trapDamage;
            }
        }

        private void Update()
        {
            _Health.text = _healthPoint.ToString();
            _Shield.text = _shieldPoint.ToString();

            if (_shieldPoint <= 0)
                _shieldPoint = 0;

            if (_healthPoint <= 0)  
                Destroy(gameObject);
        }
    }
}