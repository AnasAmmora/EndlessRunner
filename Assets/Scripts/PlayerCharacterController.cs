using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEditorInternal;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private float gravity = -55;



    private CharacterController characterController;
    private Animator animator;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isAttacking = false;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        HandleIdleAnimation();

        if (!GameManager.Instance.isStarted || GameManager.Instance.isPaused)
            return;

        HandleGravity();
        HandleMovement();
        HandleAttack();
    }

    private void HandleGravity()
    {
        isGrounded = Physics.CheckSphere(transform.position, 0.4f, groundLayers, QueryTriggerInteraction.Ignore);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2 * gravity);
            HandleJumpAnimation();
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        characterController.Move(velocity * Time.deltaTime);
    }
    private void HandleMovement()
    {
        characterController.Move(new Vector3(0, 0, runSpeed) * Time.deltaTime);
    }
    private void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.A) && !isAttacking) 
        {
            isAttacking = true; 
            HandleAttackAnimation();


            Invoke(nameof(EndAttackAnimation), 1f);
        }


    }
    private void HandleIdleAnimation()
    {
        animator.SetBool("Started", GameManager.Instance.isStarted);
    }

    private void HandleJumpAnimation()
    {
        animator.SetTrigger("Jump");
    }

    private void HandleAttackAnimation()
    {
        StartCoroutine(SmoothLayerWeightChange(1f, 0.2f));
        animator.SetTrigger("Attack");
    }

    private void EndAttackAnimation()
    {
        StartCoroutine(SmoothLayerWeightChange(0f, 0.2f));
        isAttacking = false;
    }

    private IEnumerator SmoothLayerWeightChange(float targetWeight, float duration)
    {
        float startWeight = animator.GetLayerWeight(1);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newWeight = Mathf.Lerp(startWeight, targetWeight, elapsedTime / duration);
            animator.SetLayerWeight(1, newWeight);
            yield return null;
        }

        animator.SetLayerWeight(1, targetWeight);
    }
}
