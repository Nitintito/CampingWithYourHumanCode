using System;
using UnityEngine;
using UnityEngine.Events;

public class DetectFire : MonoBehaviour
{
    public LayerMask fireLayer;
    [SerializeField] private VoidEvent onVoidFireUp;
    [SerializeField] private IntEvent onFireUp;
    [NonSerialized] public static UnityEvent burnObjectiveItem = new UnityEvent();
    public bool firstTimeRefuling = false;
    private GameObject heldObject;
    public GameObject burnButtonImage;

    private void Update()
    {

        if(FireAround().Length != 0)
        {
            Collider[] hits = FireAround();
            ObjectGrab objectGrab = transform.parent.GetComponentInChildren<ObjectGrab>();

            if (objectGrab.HoldingItem)
            {
                burnButtonImage.SetActive(true);
            }
            else
            {
                burnButtonImage.SetActive(false);
            }
            
            foreach (Collider hit in hits)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (objectGrab.HoldingItem)
                    {
                        if(objectGrab.HeldObject.CompareTag("Stick"))
                        {
                            firstTimeRefuling = true;
                            onFireUp.Raise(hit.transform.parent.gameObject.GetComponent<FireMechanics>().Id);
                            onVoidFireUp.Raise();
                            Debug.Log(hit.transform.parent.gameObject.GetComponent<FireMechanics>().Id);
                            objectGrab.HoldingItem = false;
                            Destroy(objectGrab.HeldObject);
                            objectGrab.HeldObject = null;
                            objectGrab.RequestPickUp = false;
                        }
                        else if(objectGrab.HeldObject.CompareTag("ObjectiveItem"))
                        {
                            burnObjectiveItem.Invoke();
                            onFireUp.Raise(hit.transform.parent.gameObject.GetComponent<FireMechanics>().Id);
                            onVoidFireUp.Raise();
                            Debug.Log(hit.transform.parent.gameObject.GetComponent<FireMechanics>().Id);
                            objectGrab.HoldingItem = false;
                            Destroy(objectGrab.HeldObject);
                            objectGrab.HeldObject = null;
                            objectGrab.RequestPickUp = false;
                        }
                    }
                }
            }
        }
        else
        {
            burnButtonImage.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 1.2f);
    }

    public Collider[] FireAround()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 1.2f, fireLayer);
        return hits;
    }

}
