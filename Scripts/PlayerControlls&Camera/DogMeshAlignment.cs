using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMeshAlignment : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] float rayDistance = 10;

    [SerializeField]LayerMask layerMask;
    RaycastHit hit;
    Quaternion rotation;

    PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
    }

    private void Update()
    { 
        Rotate();
    }

    void Rotate()
    {
        if (playerMovement.GetComponent<Rigidbody>().velocity.magnitude > 0.2f)
        {
            if (Physics.Raycast(transform.position, -transform.up, out hit, rayDistance, layerMask))
            {
                if (playerMovement.IsGrounded())
                {
                    rotation = Quaternion.FromToRotation(transform.up, hit.normal);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation * playerMovement.transform.rotation, rotationSpeed * Time.deltaTime);
                }
            }
        }
    }
}
