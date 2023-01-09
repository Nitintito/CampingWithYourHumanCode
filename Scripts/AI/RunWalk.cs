using UnityEngine;

public class RunWalk : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void RunWalkAnimation()
    {
        transform.parent.GetComponent<AI>().RunWalkAnimation();
    }
}
