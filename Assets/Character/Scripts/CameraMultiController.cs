using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Unity Camera controller by Gaz Robinson
/// 2020
/// 
/// Has both Orbit and Free functionality.
/// </summary>

public class CameraMultiController : MonoBehaviour
{

    [Header("Transforms to Control")]
    [Tooltip("Should be the Character transform by default")]

    public Transform followTarget = null;
    public Vector3 focalOffset = new Vector3(0.0f, 0.0f, 0.0f);

    [Header("Camera Options")]

    [Range(0.1f, 100f)] public float orbitSensitivity = 20.0f;
    [Range(0.1f, 1000f)] public float scrollSensitivity = 200.0f;

    private Vector3 orbitAngles = new Vector3(37.5f, 45.0f, 0.0f);

    public float viewDistance = 5.0f;

    private bool active = false;

    void Start()
    {
        OrbitUpdate();
    }

    void LateUpdate()
    {
        //Activate camera controls on mouse click
        if (Input.GetMouseButtonDown(0))
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
            active = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
            active = false;
        }

        viewDistance = Mathf.Clamp(viewDistance + Input.mouseScrollDelta.y * Time.deltaTime * -scrollSensitivity, 2.0f, 300.0f);

        if (active)
        {
            OrbitUpdate();
        }
        transform.position = (followTarget ? followTarget.position : Vector3.zero) + focalOffset - transform.forward * viewDistance;

    }
    void OrbitUpdate()
    {
        orbitAngles.x -= Input.GetAxisRaw("Mouse Y") * Time.deltaTime * orbitSensitivity;
        orbitAngles.x = Mathf.Clamp(orbitAngles.x, 0, 90);
        orbitAngles.y -= Input.GetAxisRaw("Mouse X") * Time.deltaTime * -orbitSensitivity;

        transform.localRotation = Quaternion.AngleAxis(orbitAngles.y, Vector3.up);
        transform.localRotation *= Quaternion.AngleAxis(orbitAngles.x, Vector3.right);
    }
}

