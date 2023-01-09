using UnityEngine;
using Cinemachine;

public class StartMenu : MonoBehaviour
{
    public static bool gameHasStarted;
    
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject startMenu;
    
    private CinemachineFreeLook _cinemachineFreeLook;
    private CinemachineComposer _cinemachineComposer;
    
    private float CMtempFOV = 0;
    [SerializeField] private float CMstartFieldOfView = 15;

    [SerializeField] private Vector3 CMstartViewOffset;

    [SerializeField] private CinemachineVirtualCamera startMenuCamera;
    
    
    
    private PlayerMovement _playerMovement;
    

    private void Awake()
    {
        _cinemachineFreeLook = Camera.main.transform.parent.GetComponentInChildren<CinemachineFreeLook>();
        _cinemachineComposer = _cinemachineFreeLook.GetRig(1).GetCinemachineComponent<CinemachineComposer>();
        
        _playerMovement = Camera.main.gameObject.transform.parent.GetComponentInChildren<PlayerMovement>();
        Cursor.lockState = CursorLockMode.Confined;
        StartSetup();
    }

    private void Start()
    {
        _cinemachineFreeLook.enabled = false;
    }

    private void StartSetup()
    {
        _playerMovement.enabled = false;
        
        //Cinemachine setup
        CMtempFOV = _cinemachineFreeLook.m_Lens.FieldOfView;
        _cinemachineComposer.m_TrackedObjectOffset = CMstartViewOffset;
        _cinemachineFreeLook.m_Lens.FieldOfView = CMstartFieldOfView;

    }

    public void StartGame()
    {
        inGameUI.SetActive(true);
        startMenu.SetActive(false);
        _cinemachineFreeLook.enabled = true;
        _cinemachineFreeLook.m_Lens.FieldOfView = CMtempFOV;
        
        _cinemachineComposer.m_TrackedObjectOffset = Vector3.zero;
        
        _playerMovement.enabled = true;
        PauseMenu.canPauseGame = true;
        StartMenu.gameHasStarted = true;
        Cursor.lockState = CursorLockMode.Locked;
        startMenuCamera.Priority = -10;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        _cinemachineFreeLook.m_Lens.FieldOfView = CMtempFOV;
    }
}
