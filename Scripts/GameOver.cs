using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private GameObject _ghost;
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
    [SerializeField] private GameObject gameOverUi;
    [SerializeField] private GameObject gameEndUi;
    [SerializeField] private GameObject restartButton;
    private bool gameHasEnded;

    private void Start()
    {
        ObjectiveItemUI.burnedAllItems.AddListener(BurnedAllItems);
        AI.gameOver.AddListener(GhostAttackedCamp);
        gameOverUi.SetActive(false);
        gameEndUi.SetActive(false);
        restartButton.SetActive(false);
    }

    void GhostAttackedCamp()
    {
        CinemachineInputPause.isLocked = true;
        PlayerMovement.disableControls = true;
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            StartCoroutine(GameOver1());
        }
    }
    
    IEnumerator GameOver1()
    {
        GetGhost();
        yield return new WaitForSeconds(0.4f);
        _cinemachineVirtualCamera.Follow = _ghost.transform;
        _cinemachineVirtualCamera.LookAt = _ghost.transform;
        _cinemachineVirtualCamera.Priority = 100;
        yield return new WaitForSeconds(8);
        gameOverUi.SetActive(true);
        _cinemachineVirtualCamera.Priority = -100;
        yield return new WaitForSeconds(5);
        restartButton.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    void BurnedAllItems()
    {
        CinemachineInputPause.isLocked = true;
        
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            StartCoroutine(GameEnd());
        }
    }

    IEnumerator GameEnd()
    {
        GetGhost();
        
        yield return new WaitForSeconds(0.4f);
        _cinemachineVirtualCamera.Follow = _ghost.transform;
        _cinemachineVirtualCamera.LookAt = _ghost.transform;
        _cinemachineVirtualCamera.Priority = 100;
        yield return new WaitForSeconds(4);
        _ghost.SetActive(false);
        yield return new WaitForSeconds(4);
        _cinemachineVirtualCamera.Priority = -100;
        
        //gameEndUi.SetActive(true);
        CinemachineInputPause.isLocked = false;
        SceneManager.LoadScene(1);
        //yield return new WaitForSeconds(15);
        //gameEndUi.SetActive(false);
    }

    private void GetGhost()
    {
        _ghost = GameObject.FindWithTag("AI");
        Debug.Log("ghost object:" + _ghost.name);
    }

    public void RestartButton()
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }
}
