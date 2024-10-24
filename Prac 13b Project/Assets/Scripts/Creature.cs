using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Creature : MonoBehaviour
{
    //Interface
    private PlayerActions playerActions;
    private InputAction mouseMovement;
    private Camera cam;
    private Vector3 mousePos;

    //Navigation
    private Vector3 toMouse;
    private Vector3 direction;

    //Movement
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float turnSpeed = 5;
    [SerializeField] private float distanceFromTarget = 1;

    void Awake()
    {
        playerActions = new PlayerActions();
        mouseMovement = playerActions.Movement.MouseMovement;
    }

    void Start()
    {
        cam = Camera.main;
    }

    void OnEnable()
    {
        mouseMovement.Enable();
    }

    void OnDisable()
    {
        mouseMovement.Disable();
    }

    void Update()
    {
        // Getting the mouse position into world coordinates.
        mousePos = mouseMovement.ReadValue<Vector2>();
        mousePos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.farClipPlane));
        mousePos.z = 0;

        // Finding the relative position of the mouse, then moving and rotating in relation to it.
        toMouse = mousePos - transform.position;
        direction = Vector3.Normalize(toMouse);
        if (toMouse.magnitude > distanceFromTarget)
        {
            transform.Translate(transform.TransformDirection(Vector3.up) * moveSpeed * Time.deltaTime);
            float dotProduct = Vector3.Dot(transform.TransformDirection(-Vector3.right), direction);
            transform.Rotate(transform.forward * turnSpeed * dotProduct * Time.deltaTime);
        }
    }
}
