using UnityEngine;
using TMPro; // <-- NEW: Required to talk to the UI Text

public class SlopeStopCheck : MonoBehaviour
{
    [Header("Settings")]
    public GameManager gameManager;
    public string slopeName = "Up Slope"; 
    public float requiredWaitTime = 5f; 

    [Header("UI Connections")]
    public TextMeshProUGUI timerText; // <-- NEW: Connects to your screen text

    private float currentWaitTime = 0f;
    private bool hasCompletedWait = false;
    private ProManualCarController activeCar = null;

    void Start()
    {
        // Hide the text when the game starts so it's not floating on the screen
        if (timerText != null) timerText.gameObject.SetActive(false); 
    }

    void OnTriggerEnter(Collider other)
    {
        ProManualCarController car = other.GetComponentInParent<ProManualCarController>();
        
        if (car != null && activeCar == null)
        {
            activeCar = car;
            currentWaitTime = 0f;       
            hasCompletedWait = false;   
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (activeCar != null)
        {
            // Ensure the text is visible as long as the car is inside the box
            if (timerText != null && !timerText.gameObject.activeSelf)
            {
                timerText.gameObject.SetActive(true); 
            }

            // Only run the timer if they haven't finished the 5 seconds yet
            if (!hasCompletedWait)
            {
                if (Mathf.Abs(activeCar.currentSpeed) < 0.5f)
                {
                    currentWaitTime += Time.deltaTime; 
                    
                    // Calculate how many seconds are left and round it up to a clean number (5, 4, 3...)
                    float timeLeft = requiredWaitTime - currentWaitTime;
                    
                    if (timerText != null)
                    {
                        timerText.color = Color.yellow;
                        timerText.text = "WAIT: " + Mathf.Ceil(timeLeft).ToString() + "s";
                    }

                    // Did they hit 5 seconds?
                    if (currentWaitTime >= requiredWaitTime)
                    {
                        hasCompletedWait = true;
                        if (timerText != null)
                        {
                            timerText.color = Color.green;
                            timerText.text = "SUCCESS! YOU CAN GO!";
                        }
                    }
                }
                else
                {
                    // They rolled or inched forward! Reset the timer.
                    currentWaitTime = 0f; 
                    if (timerText != null)
                    {
                        timerText.color = Color.red;
                        timerText.text = "STOP COMPLETELY!";
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        ProManualCarController car = other.GetComponentInParent<ProManualCarController>();
        
        if (car != null && car == activeCar)
        {
            if (hasCompletedWait == false)
            {
                // Failed!
                gameManager.DeductMarks(100, "Did not wait 5 seconds on the " + slopeName);
            }
            
            // Hide the text completely when the car drives away
            if (timerText != null) timerText.gameObject.SetActive(false);
            
            activeCar = null; 
        }
    }
}
