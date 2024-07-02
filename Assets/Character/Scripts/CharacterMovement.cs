using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{

    private GameObject thirdPersonCamera;
    private Transform characterTransform;
    private CharacterController characterController;
    SwapController swapController;
    private Vector3 playerVelocity;
    private bool groundedPlayer = true;
    private float gravityValue = -9.81f;
    GameObject currentTarget;

    [Header("Movement Options")]
    [Range(0.1f, 10f)] public float playerSpeed = 2.0f;
    [Range(0.1f, 10f)] public float jumpHeight = 1.0f;
    [Range(0.1f, 10f)] public float sprintMultiplier = 2.0f;
    [Range(0.1f, 100f)] public float maxDistance = 10.0f;

    private void Start()
    {
        thirdPersonCamera = Camera.main.gameObject;
        characterController = gameObject.GetComponent<CharacterController>();
        characterTransform = GetComponent<Transform>();
        swapController = thirdPersonCamera.GetComponent<SwapController>();
    }

    void Update()
    {
        currentTarget = swapController.GetCurrentTarget();
        if (currentTarget == gameObject)
        {
            groundedPlayer = characterController.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }


            Vector3 move = new Vector3((thirdPersonCamera.transform.forward.x * Input.GetAxis("Vertical")), 0, (thirdPersonCamera.transform.forward.z * Input.GetAxis("Vertical")));
            move += new Vector3((thirdPersonCamera.transform.right.x * Input.GetAxis("Horizontal")), 0, (thirdPersonCamera.transform.right.z * Input.GetAxis("Horizontal")));
            characterController.Move(move * Time.deltaTime * playerSpeed);


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
            characterController.Move(playerVelocity * Time.deltaTime);
        }
        else
        {
            Quaternion _lookRotation = Quaternion.LookRotation((currentTarget.transform.position - transform.position).normalized);

            //over time
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * 3);

            Vector3 ahead = _lookRotation * Vector3.forward;

            if (((transform.position.x - currentTarget.transform.position.x) > maxDistance) ||
                ((transform.position.y - currentTarget.transform.position.y) > maxDistance) ||
                ((transform.position.z - currentTarget.transform.position.z) > maxDistance) ||
                ((transform.position.x - currentTarget.transform.position.x) < -maxDistance) ||
                ((transform.position.y - currentTarget.transform.position.y) < -maxDistance) ||
                ((transform.position.z - currentTarget.transform.position.z) < -maxDistance))
            {
                characterController.GetComponent<CharacterController>().Move(ahead * Time.deltaTime * 4);
            }
        }
    }
}