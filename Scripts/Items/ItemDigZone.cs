using System.Collections;
using UnityEngine;

public class ItemDigZone : MonoBehaviour
{
    [SerializeField] private GameObject _itemToBeDugUp;
    
    [Tooltip("How far in front of dog the item appears.")]
    [SerializeField][Range(0.1f, 4)] private float _popUpDistance;
    [Tooltip("How high the item jumps out of the ground.")]
    [SerializeField][Range(0.1f, 15)] private float _popUpSpeed;
    [Tooltip("Time until the item pops up from the ground")]
    [SerializeField][Range(0.1f, 15)] private float _timeToDig;
    
    
    [SerializeField] private float _outerRadius;
    [SerializeField] private float _yellowRadius;
    [SerializeField] private float _greenRadius;
    private const float _minRadius = 0;
    
    public RangeStates _curRangeState;
    [SerializeField] private GameObject _player;
    private Sniff _sniff;

    public enum RangeStates
    {
        OutOfRange,
        Red,
        Yellow,
        Green,
        DugUp
    }

    private void Start()
    {
        PlayerMovement.digEvent.AddListener(DigUpItem);
        SphereCollider sc = GetComponent<SphereCollider>();
        _player = GameObject.FindWithTag("Player");
        _sniff = _player.GetComponentInParent<Sniff>();
        sc.radius = _outerRadius;
        _curRangeState = RangeStates.OutOfRange;
    }

    private void DigUpItem()
    {
        if (PauseMenu.gameIsPaused) return;

        if (_curRangeState != RangeStates.Green) return;
        ItemIsDugUp();
        StartCoroutine(DigUp());
    }

    IEnumerator DigUp()
    {
        yield return new WaitForSeconds(_timeToDig);
        GameObject go = Instantiate(_itemToBeDugUp, _player.transform.position + (_player.transform.forward * _popUpDistance), Quaternion.identity);
        Rigidbody rb = go.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * _popUpSpeed, ForceMode.Impulse);
        FMODUnity.RuntimeManager.PlayOneShot("event:/ItemSounds/ItemSpawn");
        _sniff.sniffableObject = null;
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (_curRangeState == RangeStates.DugUp) return;
       
        if (other.CompareTag("Player"))
        {
            _sniff.sniffableObject = this;
            
            float distToItem = Vector3.Distance(transform.position, _player.transform.position);
            float value = Remap(_minRadius, _outerRadius, 0, 1, distToItem);
            
            float yellowRad = Remap(_minRadius, _outerRadius, 0, 1, _yellowRadius);
            float greenRad = Remap(_minRadius, _outerRadius, 0, 1, _greenRadius);

            if (value > yellowRad)
            {
                _curRangeState = RangeStates.Red;
            }
            else if (value < yellowRad && value > greenRad)
            {
                _curRangeState = RangeStates.Yellow;
            }
            else if (value < greenRad)
            {
                _curRangeState = RangeStates.Green;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_curRangeState == RangeStates.DugUp) return;
        
        _curRangeState = RangeStates.OutOfRange;
        _sniff.sniffableObject = null;
    }

    private void ItemIsDugUp()
    {
        _curRangeState = RangeStates.DugUp;
        _sniff.sniffableObject = null;
    }

    static float Remap( float iMin, float iMax, float oMin, float oMax, float v ) 
    {
        float t = Mathf.InverseLerp( iMin, iMax, v );
        return Mathf.Lerp( oMin, oMax, t );
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _outerRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _yellowRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _greenRadius);
    }
}
