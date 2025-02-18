using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    [SerializeField] private GameObject explosionParticle;

    override
    public void DestroyEnemy(bool instant)
    {
        if (instant) 
        {
            DestroyEnemyAfterDelay();
        }
        else
        {
            Invoke("DestroyEnemyAfterDelay", 1f);
        }   
    }
    private void DestroyEnemyAfterDelay()
    {
        Instantiate(explosionParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
