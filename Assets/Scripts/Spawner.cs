using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lesson1
{
    public class Spawner : MonoBehaviour
    {
        public GameObject[] enemy;
        public Transform[] spawnPoint;

        private int rand;
        private int randPosition;
        public float startTimeBtwSpawns;
        private float timeBtwSpawns;

        void Start()
        {
            timeBtwSpawns = startTimeBtwSpawns;
        }

        void Update()
        {
            if (timeBtwSpawns <= 4)
            {
                rand = Random.Range(0, enemy.Length);
                randPosition = Random.Range(0, spawnPoint.Length);
                Instantiate(enemy[rand], spawnPoint[randPosition].transform.position, Quaternion.identity);
                timeBtwSpawns = startTimeBtwSpawns;
            }
            else
            {
                timeBtwSpawns -= Time.deltaTime;
            }
        }
    }
}