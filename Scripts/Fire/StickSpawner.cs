using UnityEngine;

public class StickSpawner : MonoBehaviour
{

    public GameObject campFireGroup;
    public GameObject stick;
    public FireData fireData;
    private Transform trees;
    private int numOfStickToSpawn;

    [SerializeField] private Terrain terrian;

    void Start()
    {
        campFireGroup = GameObject.FindWithTag("CampFireGroup");

        numOfStickToSpawn = fireData.NumOfSitcksToSpawn;

        trees = GameObject.FindGameObjectWithTag("Tree").transform;

        SpawnNearTrees();
    }

    private void SpawnNearTrees()
    {
        foreach (Transform tree in trees)
        {
            for (int i = 0; i < numOfStickToSpawn; i++)
            {
                float randomZ = Random.Range(-5f, 5f);
                float randomX = Random.Range(-5f, 5f);

                float yValue = Terrain.activeTerrain.SampleHeight(new Vector3(randomX, 0, randomZ));

                Vector3 spawnPos = tree.transform.position + new Vector3(randomX, yValue, randomZ);

                int random = Random.Range(0, 360);
                
                Instantiate(stick, spawnPos, Quaternion.Euler(0,random, 0));
            }
        }
    }


    public void SpawnOneRandomly()
    {
        float randomX = Random.Range(-50f, 50f);
        float randomZ = Random.Range(-70f, 70f);

        float yValue = Terrain.activeTerrain.SampleHeight(new Vector3(randomX, 0, randomZ));

        yValue += 1;

        Vector3 spawnPos = campFireGroup.transform.position + new Vector3(randomX, yValue, randomZ);

        Instantiate(stick, spawnPos, Quaternion.identity);
    }
}
