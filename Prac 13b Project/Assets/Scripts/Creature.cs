using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Creature : MonoBehaviour
{
    //Input
    private PlayerActions playerActions;
    private InputAction mouseMovement;
    [SerializeField] private Vector3 mousePos;

    //Movement
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float turnSpeed = 5;
    [SerializeField] private float distanceFromTarget = 1;

    private Camera cam;

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

    private Vector3 toMouse = new Vector3();
    private Vector3 direction = new Vector3();

    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(mouseMovement.ReadValue<Vector2>().x, mouseMovement.ReadValue<Vector2>().y,
            cam.farClipPlane));
        mousePos.z = 0;

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
