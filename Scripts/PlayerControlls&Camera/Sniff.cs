using UnityEngine;
using UnityEngine.UI;

public class Sniff : MonoBehaviour
{
    [HideInInspector] public Image sniffColor;
    public Image rightMouseImage;
    public Image leftMouseImage;
    [HideInInspector] public ParticleSystem particleSystem;

    private ParticleSystem.MainModule _particles;
    private Color newColor;
    private Color curColor;
    [Range(0.5f,3)][SerializeField] private float colorChangeSpeed;

    [SerializeField] private Color inZoneColor;
    [SerializeField] private Color redZoneColor;
    [SerializeField] private Color yellowZoneColor;
    [SerializeField] private Color greenZoneColor;

    private float shineInterval = 0.5f;
    private float shineCounter = 0f;
    private float shineColorFlag = 1;

    private Transform camera;

    [Header("For Debug Only")]
    public ItemDigZone sniffableObject;
    
    private void Start()
    {
        _particles = particleSystem.main;
        _particles.startColor = Color.white;
        sniffColor.color = inZoneColor;
        camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Sniffing();
            ParticleEffect();
        }
        else
        {
            var shape = particleSystem.shape;
            CheckIfInsideZone();
        }

        if (particleSystem.isPlaying)
        {
            curColor = Color.Lerp(_particles.startColor.color, newColor, Time.deltaTime * colorChangeSpeed);
            _particles.startColor = curColor;
        }

        UIpopup();
    }

    private void UIpopup()
    {
        if (sniffableObject != null)
        {
            if (sniffableObject._curRangeState == ItemDigZone.RangeStates.Yellow || sniffableObject._curRangeState == ItemDigZone.RangeStates.Red)
            {
                leftMouseImage.enabled = true;
                rightMouseImage.enabled = false;
            }
            else if (sniffableObject._curRangeState == ItemDigZone.RangeStates.Green)
            {
                leftMouseImage.enabled = false;
                rightMouseImage.enabled = true;
            }
        }
        else
        {
            leftMouseImage.enabled = false;
            rightMouseImage.enabled = false;
        }
    }

    private void StopParticles()
    {
        if (!particleSystem.isStopped)
        {
            particleSystem.Stop();
            sniffColor.gameObject.SetActive(false);
        }
    }

    private void PlayParticles()
    {
        if (!particleSystem.isPlaying)
        {
            particleSystem.Play();
            sniffColor.gameObject.SetActive(true);
        }
    }

    private void ParticleEffect()
    {
        if (sniffableObject != null)
        {
            float diffX = sniffableObject.transform.position.x - transform.position.x;
            float diffZ = sniffableObject.transform.position.z - transform.position.z;

            Vector3 sub = new Vector3(diffX, 0, diffZ);
            
            Vector3 cameraTemp = new Vector3(camera.forward.x, 0, camera.forward.z);

            float angle = Vector3.Angle(cameraTemp, sub);

            PlayParticles();

            if (sniffableObject._curRangeState == ItemDigZone.RangeStates.Green)
            {
                newColor = greenZoneColor;  
            }
            else if (sniffableObject._curRangeState == ItemDigZone.RangeStates.Yellow)
            {
                newColor = yellowZoneColor;
            }
            else if (sniffableObject._curRangeState == ItemDigZone.RangeStates.Red)
            {
                newColor = redZoneColor;
            }

            float temp = 1 - Mathf.InverseLerp(0, 180, angle);

            var shape = particleSystem.shape;

            if (sniffableObject._curRangeState != ItemDigZone.RangeStates.Green)
            {
                shape.radius = 0.7f * (1 - temp);    
                shape.scale = 0.1f * new Vector3(1 - temp, 1 - temp, 1 - temp);  
                _particles.startLifetime = Mathf.Lerp(0.8f, 0.3f, temp);
                _particles.maxParticles = Mathf.FloorToInt(Mathf.Lerp(0f, 0f, temp)); // 10-30
                _particles.startSpeed = Mathf.Lerp(1.3f, 0f, temp);
                newColor.a = temp;
            }
        }
        else
        {
            StopParticles();
        }
    }

    void CheckIfInsideZone()
    {
        if (sniffableObject != null)
        {
            PlayParticles();
            
            sniffColor.color = inZoneColor;
            newColor = inZoneColor;
        }
        else
        {
            sniffColor.color = inZoneColor;
            StopParticles();
        }
    }

    void Sniffing()
    {
        if (sniffableObject != null)
        {
            if (sniffableObject._curRangeState == ItemDigZone.RangeStates.Green)
            {
                sniffColor.color = curColor;
            }
            else if (sniffableObject._curRangeState == ItemDigZone.RangeStates.Yellow)
            {
                sniffColor.color = curColor;
            }
            else if (sniffableObject._curRangeState == ItemDigZone.RangeStates.Red)
            {
                sniffColor.color = curColor;
            }
        }
        else
        {
            sniffColor.color = Color.white;
        }
    }
}
