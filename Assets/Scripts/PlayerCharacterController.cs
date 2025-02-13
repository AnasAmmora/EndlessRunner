using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float jumpHeight = 10f;
    

    private float gravity = -55;
    private CharacterController characterController;
    private Vector3 velocity;
    [SerializeField] private bool isGrounded;



    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {


        isGrounded = Physics.CheckSphere(transform.position, 0.4f, groundLayers, QueryTriggerInteraction.Ignore);


        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        characterController.Move(new Vector3(0, 0, runSpeed) * Time.deltaTime);

        // Vertical Velocity
        characterController.Move(velocity * Time.deltaTime);
    }


}
