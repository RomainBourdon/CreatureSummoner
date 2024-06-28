using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{

    private GameObject thirdPersonCamera;
    private Transform characterTransform;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer = true;
    private float gravityValue = -9.81f;

    [Header("Movement Options")]
    [Range(0.1f, 10f)] public float playerSpeed = 2.0f;
    [Range(0.1f, 10f)] public float jumpHeight = 1.0f;
    [Range(0.1f, 10f)] public float sprintMultiplier = 2.0f;

    private void Start()
    {
        thirdPersonCamera = Camera.main.gameObject;
        controller = gameObject.AddComponent<CharacterController>();
        characterTransform = GetComponent<Transform>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3((thirdPersonCamera.transform.forward.x * Input.GetAxis("Vertical")), 0, (thirdPersonCamera.transform.forward.z * Input.GetAxis("Vertical")));
        move += new Vector3((thirdPersonCamera.transform.right.x * Input.GetAxis("Horizontal")), 0, (thirdPersonCamera.transform.right.z * Input.GetAxis("Horizontal")));
        controller.Move(move * Time.deltaTime * playerSpeed);
        

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}