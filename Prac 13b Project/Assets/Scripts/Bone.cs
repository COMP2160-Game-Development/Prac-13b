using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    [SerializeField] private float size = 0.5f;
    [SerializeField] private Color creatureColour = Color.magenta;

    void Update()
    {
        // You'll be putting some code here.
    }

    void OnDrawGizmos()
    {
        Gizmos.color = creatureColour;
        Gizmos.DrawSphere(transform.position, size);
    }
}
