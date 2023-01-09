using UnityEngine;

public class HoldabelObject : MonoBehaviour, IHoldabelObject
{
    [SerializeField]private Vector3 rotationInMouth;
    [SerializeField]private Vector3 positionInMouth;

    public void RotateOnPickUp(GameObject whatToRotate)
    {
        whatToRotate.transform.localRotation = Quaternion.Euler(rotationInMouth);
        whatToRotate.transform.localPosition += positionInMouth;
    }
}
