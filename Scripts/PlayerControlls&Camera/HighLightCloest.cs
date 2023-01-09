using UnityEngine;

public class HighLightCloest : MonoBehaviour
{
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material highlightObjectiveItem;
    public Material originalMaterial;
    [SerializeField] private LayerMask holdableObject;

    [HideInInspector] public GameObject heldObject;
    [HideInInspector] public GameObject lastHeldObject;

    private ObjectGrab objectGrab;
    private bool isHolding = false; 

    void Start()
    {
        objectGrab = GetComponent<ObjectGrab>();
    }

    void Update()
    {
        isHolding = objectGrab.HoldingItem;

        Collider[] hits = Physics.OverlapSphere(transform.position, 1.2f, holdableObject);
        if (hits.Length != 0)
        {
            if (hits.Length >= 2)
            {
                int indexWanted = 0;
                float[] distances = new float[hits.Length];
                distances[0] = Vector3.Distance(transform.position, hits[0].transform.position);

                for (int i = 1; i < hits.Length; i++)
                {
                    distances[i] = Vector3.Distance(transform.position, hits[i].transform.position);
                    if (distances[i] > distances[indexWanted])
                    {
                        indexWanted = i;
                    }
                }

                if (lastHeldObject != null) lastHeldObject.transform.GetComponentInChildren<MeshRenderer>().sharedMaterial = originalMaterial;

                if (!isHolding)
                {
                    heldObject = hits[indexWanted].gameObject;
                    originalMaterial = heldObject.GetComponentInChildren<MeshRenderer>().sharedMaterial;
                    lastHeldObject = heldObject;
                    if (lastHeldObject.tag == "Stick") {
                        lastHeldObject.transform.GetComponentInChildren<MeshRenderer>().sharedMaterial = highlightMaterial;
                    }
                    else
                    {
                        lastHeldObject.transform.GetComponentInChildren<MeshRenderer>().sharedMaterial = highlightObjectiveItem;
                    }
                   
                }

            }
            else
            {

                if (lastHeldObject != null) lastHeldObject.transform.GetComponentInChildren<MeshRenderer>().sharedMaterial = originalMaterial;

                if (!isHolding)
                {
                    heldObject = hits[0].gameObject;
                    originalMaterial = heldObject.GetComponentInChildren<MeshRenderer>().sharedMaterial;
                    lastHeldObject = heldObject;
                    if (lastHeldObject.tag == "Stick")
                    {
                        lastHeldObject.transform.GetComponentInChildren<MeshRenderer>().sharedMaterial = highlightMaterial;
                    }
                    else
                    {
                        lastHeldObject.transform.GetComponentInChildren<MeshRenderer>().sharedMaterial = highlightObjectiveItem;
                    }
                }
            }
        }
        else
        {
            if (lastHeldObject != null) lastHeldObject.transform.GetComponentInChildren<MeshRenderer>().sharedMaterial = originalMaterial;
        }
    }
}
