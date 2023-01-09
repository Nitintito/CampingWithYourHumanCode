using UnityEngine;

public class SetCirculatePoint : MonoBehaviour
{
    [SerializeField] FireData fireData;
    Transform fire;

    private void Start()
    {
        fire = transform.parent;
        Vector3 tmp = fire.localPosition;
        transform.localPosition = tmp.normalized * fireData.CirculatePointRange;
    }
}
