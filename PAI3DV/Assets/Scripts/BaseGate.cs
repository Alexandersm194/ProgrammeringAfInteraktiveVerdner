using UnityEngine;

public class BaseGate : MonoBehaviour
{
    [SerializeField] private GameObject gateRight;
    [SerializeField] private GameObject gateLeft;
    
    [SerializeField] private Transform gateRightClosed;
    [SerializeField] private Transform gateRightOpen;
    [SerializeField] private Transform gateLeftClosed;
    [SerializeField] private Transform gateLeftOpen;


    [SerializeField] private float openSpeed = 1f;    
    
    [SerializeField] private AudioSource gateSound;
    private bool open = false;

    private float leftT = 0f;
    private float rightT = 0f;

    public void OpenGate()
    {
        open = true;
        gateSound.Play();
    }
    void Update()
    {
        if (open)
        {
            MoveGate(); 
        }
    }

    public void MoveGate()
    {
        
        leftT += openSpeed * Time.deltaTime;
        rightT += openSpeed * Time.deltaTime;
                
        gateRight.transform.position = Vector3.Lerp(gateRightClosed.transform.position, gateRightOpen.transform.position, rightT);
        gateLeft.transform.position = Vector3.Lerp(gateLeftClosed.transform.position, gateLeftOpen.transform.position, leftT);
        
        leftT = Mathf.Clamp01(leftT);
        rightT = Mathf.Clamp01(rightT);
    }
}
