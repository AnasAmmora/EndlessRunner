using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float timeToDestroy = 1.5f;
    [SerializeField] private float damage = 100f;

    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void OnTriggerEnter(Collider other)
    {

        Destroy(gameObject);


        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.DestroyEnemy();
        }
    }
}
