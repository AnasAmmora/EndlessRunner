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
        Debug.Log(GameManager.Instance.isStarted);

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
        animator.SetLayerWeight(1, 1f);
        animator.SetTrigger("Attack");
    }
    private void EndAttackAnimation()
    {
        animator.SetLayerWeight(1, 0f);
        isAttacking = false;
    }
}
