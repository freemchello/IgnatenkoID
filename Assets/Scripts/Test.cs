using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    private float health = 100f;
    public GameObject prefabAsteroid;
    public float radiusArea;

    void Start()
    {
        var show = new Show(2);
        show.ShowTime();
    }



    private void Update()
    {
        health -= Time.deltaTime;

        if (Mathf.Approximately(health, 0))
        {
            Debug.Log("Health = 0");
            enabled = false;
        }
        int[] values = new int[6];
        var value = values[Random.Range(0, values.Length)];

        Vector3 point = Random.insideUnitSphere;

        Instantiate(prefabAsteroid, transform.position + point * radiusArea, Random.rotation);
    }

    public class Show
    {
        private float _time;
        public Show(float time)
        {
            _time = time;
        }
        public void ShowTime()
        {
        Debug.Log($"Show time {_time}");
        }
    }

   
}