using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private BaseGate baseGate;
    
    [SerializeField] private Button EngineButton;
    [SerializeField] private Animator screenFadeAnim;
    [SerializeField] private Animator logoFadeAnim;
    [SerializeField] private AnimationClip fadeClip;
    [SerializeField] private GameObject StartScreen;
    [SerializeField] private AudioSource engineAudio;
    
    [SerializeField] private GameObject[] friendlyCars;
    
    [Header("UI Elements")]
    [SerializeField] private GameObject factoryCanvas;
    [SerializeField] private TextMeshProUGUI fuelText;


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
        engineAudio.Play();
        Cursor.lockState = CursorLockMode.Locked;
        EngineButton.gameObject.SetActive(false);
        StartCoroutine(RemoveStartScreen());
        StartCoroutine(Intro());
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
        
    }

    public void FactoryEvent()
    {
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
    }

    public void RamTruckAttack()
    {
        
    }
    
    IEnumerator End()
    {
        yield return new WaitForSeconds(2f);
    }
    
    
    
    
    
    
    
    
}
