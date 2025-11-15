using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private BaseGate baseGate;
    [SerializeField] private SpawnMonsterTrucks spawnMTrucks;
    [SerializeField] private SpawnBikers spawnBikers;
    
    [Header("UI")]
    [SerializeField] private Button EngineButton;
    [SerializeField] private Animator screenFadeAnim;
    [SerializeField] private Animator logoFadeAnim;
    [SerializeField] private AnimationClip fadeClip;
    [SerializeField] private GameObject StartScreen;
    [SerializeField] private AudioSource engineAudio;
    [SerializeField] private GameObject pausePanel;
    
    [SerializeField] private GameObject[] friendlyCars;
    
    [Header("UI Elements")]
    [SerializeField] private GameObject factoryCanvas;
    [SerializeField] private TextMeshProUGUI fuelText;


    [Header("Animations")]
    [SerializeField] private Animator bikerAnim;

    [Header("Triggers")] [SerializeField] private GameObject endCollider;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (!pausePanel.activeInHierarchy) 
            {
                PauseGame();
            }
            else if (pausePanel.activeInHierarchy) 
            {
                ContinueGame();   
            }
        } 
    }
    private void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    } 
    private void ContinueGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    private enum GameState
    {
        Paused, Intro, DriveToCanyon, BikerAttack, FuelUp, RamTruckAttack, End
    }
    
    [SerializeField] private GameState gameState = GameState.Paused;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void BeginGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        EngineButton.gameObject.SetActive(false);
        StartCoroutine(RemoveStartScreen());
        StartCoroutine(Intro());
        engineAudio.Play();
    }
    

    
    IEnumerator RemoveStartScreen()
    {
        screenFadeAnim.SetTrigger("Fade");
        logoFadeAnim.SetTrigger("Fade");
        yield return new WaitForSeconds(fadeClip.length);
        StartScreen.SetActive(false);
        
    }

    IEnumerator Intro()
    {
        yield return new WaitForSeconds(5f);
        baseGate.OpenGate();
        yield return new WaitForSeconds(2f);
        DriveToCanyon();
    }

    public void DriveToCanyon()
    {
        foreach (GameObject car in friendlyCars)
        {
            FriendlyAI friendlyAI = car.GetComponent<FriendlyAI>();
            friendlyAI.SetCurrentTarget();
        }
    }
    
    public void BikerAttack()
    {
        bikerAnim.SetTrigger("Jump");
        spawnBikers.Spawn1stPhase();
    }

    public void FactoryEvent()
    {
        endCollider.SetActive(true);
        StartCoroutine(FuelUp());
    }

    private IEnumerator FuelUp()
    {
        
        factoryCanvas.SetActive(true);
        yield return new WaitForSeconds(5f);
        fuelText.text = "Fuel Completed!";
        yield return new WaitForSeconds(2f);
        fuelText.text = "Return to Base!";
        yield return new WaitForSeconds(2f);
        factoryCanvas.SetActive(false);
        yield return new WaitForSeconds(5f);
        RamTruckAttack();
        spawnBikers.Spawn2ndPhase();
    }

    public void RamTruckAttack()
    {
        spawnMTrucks.Spawn();
    }
    
    public void End()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    
    
    
    
    
    
    
}
