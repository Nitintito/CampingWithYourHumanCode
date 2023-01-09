using UnityEngine;

[CreateAssetMenu(menuName = "Data/FireData", fileName = "FireData")]
public class FireData : ScriptableObject
{

    [SerializeField] private float maxTimerValue = 30f;
    public float MaxTimerValue { get { return maxTimerValue; } private set { maxTimerValue = value; } }

    [SerializeField] private float eachFirewoodRestore;
    public float EachFireWoodRestore { get { return eachFirewoodRestore; } set { eachFirewoodRestore = value; } }

    [SerializeField] [Range(0, 40f)] private float fireLightRange;
    public float FireLightRange { get { return fireLightRange; } set { fireLightRange = value; } }

    [SerializeField] [Range(0, 50f)] private float fireLightIntensity;
    public float FirelightIntensity { get { return fireLightIntensity; } set { fireLightIntensity = value; } }


    [SerializeField] [Range(1f, 10f)] private float circulatePointRange;
    public float CirculatePointRange { get => circulatePointRange; set => circulatePointRange = value; }

    [SerializeField] private int numOfSitcksToSpawn;
    public int NumOfSitcksToSpawn { get => numOfSitcksToSpawn; set => numOfSitcksToSpawn = value; }
    
}

