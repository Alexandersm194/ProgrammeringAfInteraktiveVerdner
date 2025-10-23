using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button EngineButton;
    [SerializeField] private Animator screenFadeAnim;
    [SerializeField] private Animator logoFadeAnim;
    [SerializeField] private AnimationClip fadeClip;
    [SerializeField] private GameObject StartScreen;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginGame()
    {
        StartCoroutine(RemoveStartScreen());
        
    }
    

    
    IEnumerator RemoveStartScreen()
    {
        Cursor.lockState = CursorLockMode.Locked;
        EngineButton.gameObject.SetActive(false);
        screenFadeAnim.SetTrigger("Fade");
        logoFadeAnim.SetTrigger("Fade");
        yield return new WaitForSeconds(fadeClip.length);
        StartScreen.SetActive(false);
        
    }
    
    
}
