using UnityEngine;

public class HighlightStick : MonoBehaviour
{

    RaycastHit hit;

    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material originalMaterial;
    [SerializeField] private LayerMask playerLayer;

    private GameObject lastHitObject;

    private void Update()
    {

        if(Physics.CheckSphere(transform.position, 2f, playerLayer))
        {
            transform.GetComponent<MeshRenderer>().sharedMaterial = highlightMaterial;
        }
        else
        {
            transform.GetComponent<MeshRenderer>().sharedMaterial = originalMaterial;
        }
    }
}
