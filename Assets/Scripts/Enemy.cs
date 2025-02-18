using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject explosionParticle;

    public void DestroyEnemy()
    {
        Invoke("DestroyEnemyAfterDelay", 1f);
    }

    private void DestroyEnemyAfterDelay()
    {
        Instantiate(explosionParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
