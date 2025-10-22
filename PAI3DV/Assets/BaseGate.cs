using UnityEngine;

public class BaseGate : MonoBehaviour
{
    [SerializeField] private GameObject gateRight;
    [SerializeField] private GameObject gateLeft;
    
    [SerializeField] private Transform gateRightClosed;
    [SerializeField] private Transform gateRightOpen;
    [SerializeField] private Transform gateLeftClosed;
    [SerializeField] private Transform gateLeftOpen;


    private enum State
    {
        Open, Closed
    }
    
    [SerializeField] private State gateState = State.Closed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveGate();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(gateState == State.Open) gateState = State.Closed;
            else if(gateState == State.Closed) gateState = State.Open;
        }
    }

    void MoveGate()
    {
        switch (gateState)
        {
            case State.Open:
                gateRight.transform.position = gateRightClosed.position;
                gateLeft.transform.position = gateLeftClosed.position;
                break;
            case State.Closed:
                gateRight.transform.position = gateRightOpen.position;
                gateLeft.transform.position = gateLeftOpen.position;
                break;
        }
    }
}
