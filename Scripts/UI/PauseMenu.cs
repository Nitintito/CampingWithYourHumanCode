using Cinemachine;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private CinemachineFreeLook _cinemachineFreeLook;
    public static bool canPauseGame;
    public static bool gameIsPaused;

    private void Awake()
    {
        _cinemachineFreeLook = Camera.main.transform.parent.GetComponentInChildren<CinemachineFreeLook>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canPauseGame) PauseGame();
    }
    public void PauseGame()
    {
        gameIsPaused = gameIsPaused == false ? gameIsPaused = true : gameIsPaused = false;
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        _cinemachineFreeLook.enabled = _cinemachineFreeLook.enabled == false ? _cinemachineFreeLook.enabled = true : _cinemachineFreeLook.enabled = false;
        
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        pauseMenu.SetActive(pauseMenu.activeSelf != true);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
