using System;
using UnityEngine;
using UnityEngine.Events;

public class ObjectGrab : MonoBehaviour
{
    [SerializeField] private float sphereSize = 1.2f;
    [SerializeField] private LayerMask holdableObject;
    [SerializeField] private Transform mouthPosition;
    bool holdingItem = false;
    [HideInInspector] public GameObject heldObject;

    private PlayerMovement player;

    bool requestPickup; // set to true when you want to pick up a item and false when dropping
    bool dropItem;

    [NonSerialized] public static UnityEvent pickup = new UnityEvent(); // added for GrabObjectiveItem script

    public bool HoldingItem { get { return holdingItem; } set { holdingItem = value; } }
    public GameObject HeldObject { get { return heldObject; } set { heldObject = value; } }
    public bool RequestPickUp { get { return requestPickup; } set { requestPickup = value; } }

    private DetectFire detectFire;

    private void Start()
    {
        player = transform.parent.GetComponent<PlayerMovement>();
        detectFire = transform.parent.GetComponentInChildren<DetectFire>();
    }

    private void Update()
    {
        Inputs();
    }

    private void FixedUpdate()
    {
        GrabItemFromGround();
    }

    private void GrabItemFromGround()
    {
        if (!holdingItem && requestPickup)
        {
            requestPickup = false;
            Collider[] hits = Physics.OverlapSphere(transform.position, sphereSize, holdableObject);
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
                    holdingItem = true;
                    heldObject = hits[indexWanted].gameObject;
                    PickUpObject(heldObject, false, false, true);

                }
                else
                {
                    heldObject = hits[0].gameObject;
                    PickUpObject(heldObject, false, false, true);

                }
                pickup.Invoke(); // added for GrabObjectiveItem script
            }
        }
        else if (holdingItem && dropItem)
        {
            dropItem = false;
            holdingItem = false;
            PickUpObject(heldObject, true, true, false);
        }
    }

    void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.E) && !holdingItem && !player.IsSniffing()) //Pick up item
        {
            requestPickup = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && holdingItem && detectFire.FireAround().Length == 0) //drop item
        {
            dropItem = true;
        }
    }

    void PickUpObject(GameObject whatToPickUp, bool colliderEnabled, bool dropObject, bool holdingItem)
    {
        this.holdingItem = holdingItem;


        if (!dropObject)
        {
            Destroy(whatToPickUp.GetComponent<Rigidbody>());
            whatToPickUp.transform.SetParent(mouthPosition);
            whatToPickUp.transform.position = mouthPosition.position;
            whatToPickUp.GetComponent<HoldabelObject>().RotateOnPickUp(whatToPickUp);
        }
        else
        {
            whatToPickUp.transform.parent = null;
            whatToPickUp.AddComponent<Rigidbody>();
            whatToPickUp.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
        }

        whatToPickUp.GetComponent<Collider>().enabled = colliderEnabled;
        FMODUnity.RuntimeManager.PlayOneShot("event:/ItemSounds/PickupItem");
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sphereSize);
    }
}
