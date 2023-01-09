using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FireMechanics : MonoBehaviour
{
    [SerializeField] private FireData fireData;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private Slider slider;

    private Fire fire;

    private Light light;

    private float ratioLT;
    private float ratioRT;

    private int id;

    private bool startBurnout;
    public float startBurnoutMultiplier = 50;
    private float burnoutMultiplier = 1;
    

    public float curIntensity;
    [SerializeField] private float maxIntensity;
    [SerializeField] private float minIntensity;
    [SerializeField] private float flickerSpeed;
    private float lerpValue;
    private float random;
    

    public int Id { get => id; set => id = value; }
    public Fire Fire { get => fire; set => fire = value; }

    private void Awake()
    {
        Fire = new Fire(fireData);
        Fire.CurrentTimeLeft = fireData.MaxTimerValue;
        Fire.MaxTimeLeft = fireData.MaxTimerValue;

        light = transform.GetComponent<Light>();
        Id = GetInstanceID();

        if (slider != null)
        {
            slider.maxValue = fire.MaxTimeLeft;
            slider.value = fire.CurrentTimeLeft;
        }
        
        random = Random.Range(0.0f, 65535.0f);
    }

    private void Start()
    {
        curIntensity = fireData.FirelightIntensity;
        light.range = fireData.FireLightRange;

        ratioLT = fireData.FirelightIntensity / fireData.MaxTimerValue;
        ratioRT = fireData.FireLightRange / fireData.MaxTimerValue;
    }

    void Update()
    {
        if (slider != null)
        {
            slider.value = fire.CurrentTimeLeft;
        }
        
        Flicker();

        if (!StartMenu.gameHasStarted) return;

        if (!startBurnout)
        {
            var emission = particleSystem.emission;
            emission.rateOverTime = Fire.CurrentTimeLeft * 2 / 3;
            Fire.CurrentTimeLeft -= Time.deltaTime * burnoutMultiplier;
            curIntensity -= Time.deltaTime * ratioLT * burnoutMultiplier;
            light.range -= Time.deltaTime * ratioRT * burnoutMultiplier;
        }
        else
        {
            Fire.CurrentTimeLeft -= Time.deltaTime * startBurnoutMultiplier;
            curIntensity -= Time.deltaTime * ratioLT * startBurnoutMultiplier;
            light.range -= Time.deltaTime * ratioRT * startBurnoutMultiplier;
        }

    }

    void Flicker()
    {
        lerpValue += flickerSpeed * Time.deltaTime;
        float noise = Mathf.PerlinNoise(random, lerpValue);
        light.intensity = Mathf.Lerp(minIntensity + curIntensity, maxIntensity + curIntensity, noise);
    }

    public void FireUp(int id)
    {
        if (id == this.id)
        {
            Fire.RestoreFire();
            curIntensity = Mathf.Clamp(light.intensity + fireData.EachFireWoodRestore * ratioLT, 0, fireData.FirelightIntensity);
            light.range = Mathf.Clamp(light.intensity + fireData.EachFireWoodRestore * ratioRT, 0, fireData.FireLightRange);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 9.5f);
    }

    public void QuickBurnoutOnStart(float seconds)
    {
        StartCoroutine(Burnout(seconds));
    }

    private IEnumerator Burnout(float seconds)
    {
        startBurnout = true;
        yield return new WaitForSeconds(seconds);
        startBurnout = false;
    }

}
