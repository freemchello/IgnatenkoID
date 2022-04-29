using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lesson1
{
    public class Spawn : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private float _timeCooldown;
        private List<GameObject> _enemies;
        public Transform[] spawnPoint;
        private int randPosition;


        private void Awake()
        {
            _enemies = new List<GameObject>();
        }


        void Start()
        {
            StartCoroutine(Spawner(5));
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.K))
            {
                StopCoroutine(nameof(Spawner));
            }
        }
        private IEnumerator Spawner(int count)
        {
            for (int i = 0; i < count; i++)
            {
                randPosition = Random.Range(0, spawnPoint.Length);
                _enemies.Add(Instantiate(_enemyPrefab, spawnPoint[randPosition].transform.position/*transform.position*/, Quaternion.identity));
                yield return new WaitForSeconds(_timeCooldown);

                if (i == 2)//количество созданных объектов
                yield break;
            }

            yield return new WaitForSeconds(10f);

            foreach (var enemy in _enemies)

            {
                Destroy(enemy);
                yield return new WaitForSeconds(_timeCooldown);
            }
            _enemies.Clear();

        }

    }
}