using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SniffUIeffect : MonoBehaviour
{
    [HideInInspector]public Sniff sniff;
    private Volume _volume;
    private Vignette _vignette;
    private ChromaticAberration _chromatic;
    [SerializeField] private float _vignetteIntensity = 0.4f;
    [SerializeField] private float _chromAberIntensity = 0.2f;

    [SerializeField] private float effectSpeed = 0.1f;

    private PlayerMovement _playerMovement;

    public bool useOnlyOnSniffHold;

    private void Awake()
    {
        _playerMovement = Camera.main.transform.parent.GetComponentInChildren<PlayerMovement>();
        
        _volume = GetComponentInChildren<Volume>();
        Vignette tmp;
        if (_volume.profile.TryGet<Vignette>(out tmp))
        {
            _vignette = tmp;
        }

        ChromaticAberration tmp1;
        if (_volume.profile.TryGet<ChromaticAberration>(out tmp1))
        {
            _chromatic = tmp1;
        }
    }

    void Update()
    {
        if (useOnlyOnSniffHold) EffectWhenSniff();
        else Effect();
    }

    private void EffectWhenSniff()
    {
        if (_playerMovement.IsSniffing())
        {
            _vignette.intensity.value = Mathf.Lerp(_vignette.intensity.value, _vignetteIntensity, Time.deltaTime * effectSpeed);
            _chromatic.intensity.value = Mathf.Lerp(_chromatic.intensity.value, _chromAberIntensity, Time.deltaTime * effectSpeed);
        }
        else
        {
            _vignette.intensity.value = Mathf.Lerp(_vignette.intensity.value, 0, Time.deltaTime * effectSpeed);
            _chromatic.intensity.value = Mathf.Lerp(_chromatic.intensity.value, 0, Time.deltaTime * effectSpeed);
        }
    }

    private void Effect()
    {
        if (sniff.sniffableObject != null)
        {
            _vignette.intensity.value = Mathf.Lerp(_vignette.intensity.value, _vignetteIntensity, Time.deltaTime * effectSpeed);
            _chromatic.intensity.value = Mathf.Lerp(_chromatic.intensity.value, _chromAberIntensity, Time.deltaTime * effectSpeed);
        }
        else
        {
            _vignette.intensity.value = Mathf.Lerp(_vignette.intensity.value, 0, Time.deltaTime * effectSpeed);
            _chromatic.intensity.value = Mathf.Lerp(_chromatic.intensity.value, 0, Time.deltaTime * effectSpeed);
        }
    }
}
