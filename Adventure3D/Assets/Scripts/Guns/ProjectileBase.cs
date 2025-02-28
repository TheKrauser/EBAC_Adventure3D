using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float damage;
    public float speed;

    [SerializeField] private float timeToDestroy = 3f;

    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.Damage(damage);
            Destroy(gameObject);
        }
    }
}
