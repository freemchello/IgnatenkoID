using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smash : MonoBehaviour
{
    public float Radius;
    public float Force;
    [SerializeField] private ParticleSystem _EffectPrefab;
    [SerializeField] public float _Speed = 0.2f;
    [SerializeField] public float _damageMine = 7;
    public Rigidbody _rb1;
    public bool Active;


    private void Start()
    {
        this._rb1.AddForce(transform.forward * _Speed, ForceMode.Impulse);
        
    }

    void Smashing()
    {
        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, Radius);

        foreach (var iter in overlappedColliders)
        {
            Rigidbody rigidbody = iter.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(Force , transform.position, Radius);
            }
        }
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider mine) //при взаимодействии с триггерами активируется и разрушается
    {
        if (mine.CompareTag("Ground") || mine.CompareTag("Enemy") || mine.CompareTag("Wall"))
        {
            Smashing();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        var particle = Instantiate(_EffectPrefab);
        particle.transform.position = collision.contacts[0].point; //создание эффекта взрыва в точке соприкосновения
        //particle.transform.rotation = Quaternion.Euler(collision.contacts[0].normal);//Разворот эффекта перпендикулярно точке сопрокосновения
        var lifetime = particle.main.duration + particle.main.startLifetimeMultiplier + 1f;
        Destroy(particle.gameObject, lifetime);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Radius);//рисует зону поражения
    }
}