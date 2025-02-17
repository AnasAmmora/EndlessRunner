using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacterController : MonoBehaviour
{
    [Header("Movement Setting")]
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private float gravity = -55;

    [Header("Attack Setting")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bulletInHand;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float shootForce;
    [SerializeField] private float timeBetweenShots = 1f;
    [SerializeField] private Slider reloadingProgresSlider;
    //bools
    bool isAttacking = false;
    bool readyToAttack = true;


    private CharacterController characterController;
    private Animator animator;
    private Vector3 velocity;
    private bool isGrounded;
    


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        reloadingProgresSlider.value = 1;
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
        //Attack Input
        isAttacking = Input.GetKey(KeyCode.A);

        if (readyToAttack && isAttacking)
        {
            reloadingProgresSlider.value = 0;
            readyToAttack = false;

            HandleAttackAnimation();  
            StartCoroutine(ReloadingProgress());
            Invoke("Attack", timeBetweenShots / 2);
            Invoke("EndAttack", timeBetweenShots);
        }
    }
    private void Attack()
    {
        Vector3 bulletDirection = attackPoint.forward;

        GameObject currentBullet = Instantiate(bullet, attackPoint.position, attackPoint.rotation); //


        currentBullet.GetComponent<Rigidbody>().AddForce(bulletDirection.normalized * shootForce, ForceMode.Impulse);
        bulletInHand.SetActive(false);
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

    private void EndAttack()
    {
        StartCoroutine(SmoothLayerWeightChange(0f, 0.2f));
        readyToAttack = true;
        bulletInHand.SetActive(true);
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
    private IEnumerator ReloadingProgress()
    {
        float elapsedTime = 0f;

        while (elapsedTime < timeBetweenShots)
        {
            elapsedTime += Time.deltaTime;


            reloadingProgresSlider.value = Mathf.Clamp01(elapsedTime / timeBetweenShots);

            yield return null; 
        }
        reloadingProgresSlider.value = 1f;
    }
}



