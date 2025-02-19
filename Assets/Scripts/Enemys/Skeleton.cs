using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    [SerializeField] private GameObject explosionParticle;
    [SerializeField] protected GameObject skeletonCharacter;

    private Animator animator;
    private Rigidbody[] ragdollRigidbodies;
    private Collider[] ragdollColliders;
    private Collider[] mainColliders;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        mainColliders = GetComponents<Collider>();
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();


        DisableRagdoll(); // Ensure ragdoll is off when the game starts
    }

    override
    public void DestroyEnemy(bool instant)
    {
        if (instant)
        {
            ActivateRagdoll();
            DestroyEnemyAfterDelay();
        }
        else
        {
            ActivateRagdoll();
            Invoke("DestroyEnemyAfterDelay", 1f);
        }
        DisableMainColliders();
    }

    private void DestroyEnemyAfterDelay()
    {
        Instantiate(explosionParticle, skeletonCharacter.transform.position, skeletonCharacter.transform.rotation);
        Destroy(gameObject);
    }

    private void DisableRagdoll()
    {
        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = true;
        }
        foreach (Collider col in ragdollColliders)
        {
            if (col != GetComponent<Collider>()) // Keep the main collider enabled
                col.enabled = false;
        }
        animator.enabled = true; // Animator is on at the start
    }

    private void ActivateRagdoll()
    {
        animator.enabled = false; // Disable Animator
        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = false;
        }
        foreach (Collider col in ragdollColliders)
        {
            col.enabled = true;
        }
    }

    private void DisableMainColliders()
    {
        foreach (Collider col in mainColliders)
        {
            col.enabled = false;
        }
    }
}
