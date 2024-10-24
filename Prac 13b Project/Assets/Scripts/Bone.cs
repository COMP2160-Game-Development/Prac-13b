using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    [SerializeField] private float size;

    [SerializeField] private Color creatureColour = Color.magenta;

    void Update()
    {
    }

    void OnDrawGizmos()
    {
        Gizmos.color = creatureColour;
        Gizmos.DrawSphere(transform.position, size);
    }
}
