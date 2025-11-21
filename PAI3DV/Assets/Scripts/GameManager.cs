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
    [SerializeField] private CompassScript compass;
    
    [Header("UI")]
    [SerializeField] private Button EngineButton;
    [SerializeField] private Animator screenFadeAnim;
    [SerializeField] private AnimationClip fadeClip;
    [SerializeField] private GameObject StartScreen;
    [SerializeField] private AudioSource engineAudio;
    [SerializeField] private GameObject pausePanel;
    
    [SerializeField] private GameObject[] friendlyCars;
    
    [Header("UI Elements")]
    [SerializeField] private GameObject factoryCanvas;
    [SerializeField] private TextMeshProUGUI fuelText;
    [SerializeField] private Button exitButton;

    [Header("Animations")]
    [SerializeField] private Animator bikerAnim;

    [SerializeField] private AudioSource bikerSound;

    [Header("Triggers")] [SerializeField] private GameObject endCollider;
    
    
    [Header("Factory Settings")]
    [SerializeField] private TransportScript transportScript;
    [SerializeField] private GameObject factoryEventTrigger;
    [SerializeField] private GameObject tracktorBeamUp;
    [SerializeField] private GameObject tracktorBeamDown;
    [SerializeField] private AnimationClip tracktorBeamUpAnim;
    [SerializeField] private AnimationClip tracktorBeamDownAnim;
    private float tracktorBeamUpSpeed;
    private float tracktorBeamDownSpeed;
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
        tracktorBeamUpSpeed = tracktorBeamUpAnim.length;
        tracktorBeamDownSpeed = tracktorBeamDownAnim.length;
        
        exitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
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
        yield return new WaitForSeconds(fadeClip.length);
        StartScreen.SetActive(false);
        
    }

    IEnumerator Intro()
    {
        compass.NewObjectiveText();
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
        compass.SetTarget(friendlyCars[0].transform);
        compass.NewObjectiveText();
    }
    
    public void BikerAttack()
    {
        bikerSound.Play();
        bikerAnim.SetTrigger("Jump");
        spawnBikers.Spawn1stPhase();
        compass.NewObjectiveText();
        compass.SetTarget(factoryEventTrigger.transform);
    }

    public void FactoryEvent()
    {
        endCollider.SetActive(true);
        StartCoroutine(FuelUp());
    }
    

    private IEnumerator FuelUp()
    {
        factoryCanvas.SetActive(true);
        fuelText.text = "Stand by!";
        yield return new WaitForSeconds(2f);
        transportScript.CrystalBoxUp();
        fuelText.text = "Retrieving crystals!";
        tracktorBeamUp.SetActive(true);
        yield return new WaitForSeconds(tracktorBeamUpSpeed);
        transportScript.FuelPodDown();
        tracktorBeamUp.SetActive(false);
        tracktorBeamDown.SetActive(true);
        fuelText.text = "Delivering fuelpod!";
        yield return new WaitForSeconds(tracktorBeamDownSpeed);
        tracktorBeamDown.SetActive(false);
        fuelText.text = "Transaction completed! Return to base";
        compass.NewObjectiveText();
        compass.SetTarget(baseGate.transform);
        factoryEventTrigger.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        factoryCanvas.SetActive(false);
        yield return new WaitForSeconds(6f);
        RamTruckAttack();
        spawnBikers.Spawn2ndPhase();
    }

    public void RamTruckAttack()
    {
        compass.NewObjectiveText();
        spawnMTrucks.Spawn();
    }
    
    public void End()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    
    
    
    
    
    
    
}
