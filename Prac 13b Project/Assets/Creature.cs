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

    //Appearance
    [SerializeField] private Color creatureColour = Color.magenta;
    [SerializeField] private float size = 1f;

    //Movement
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float turnSpeed = 5;
    [SerializeField] private float distanceFromTarget = 1;

    

    void Awake()
    {
        playerActions = new PlayerActions();
        mouseMovement = playerActions.Movement.MouseMovement;
    }

    void OnEnable()
    {
        mouseMovement.Enable();
    }

    void OnDisable()
    {
        mouseMovement.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mouseMovement.ReadValue<Vector2>().x, mouseMovement.ReadValue<Vector2>().y,
            Camera.main.farClipPlane));
        mousePos.z = 0;
        Vector3 toMouse = mousePos - transform.position;
        Vector3 direction = Vector3.Normalize(toMouse);
        if (toMouse.magnitude > distanceFromTarget)
        {
            transform.Translate(transform.TransformDirection(Vector3.up) * moveSpeed * Time.deltaTime);
            float dotProduct = Vector3.Dot(transform.TransformDirection(-Vector3.right), direction);
            transform.Rotate(transform.forward * turnSpeed * dotProduct * Time.deltaTime);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = creatureColour;
        Gizmos.DrawSphere(transform.position, size);
    }
}
