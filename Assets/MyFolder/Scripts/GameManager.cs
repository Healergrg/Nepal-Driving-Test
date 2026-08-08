using System.Collections; // <-- NEW: Required for Coroutines (Countdowns)
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TutorialManager tutorialScript;
    [Header("Trial Rules")]
    public int totalMarks = 100;
    public int passingMarks = 70;
    public int redLightPenalty = 15; 

    [Header("UI Connections")]
    public TextMeshProUGUI marksText;
    public GameObject startScreen; 
    public GameObject failScreen;
    public GameObject passScreen; 

    [Header("Start Sequence")]
    public TextMeshProUGUI countdownText; // <-- NEW: Slot for the 3..2..1 text
    public AudioSource whistleAudio;      // <-- NEW: Slot for the Citi sound

    private bool isGameOver = false;

    void Start()
    {
        Time.timeScale = 0f; // Freeze time at the start
        
        if (startScreen != null) startScreen.SetActive(true);
        if (failScreen != null) failScreen.SetActive(false);
        if (passScreen != null) passScreen.SetActive(false);
        if (countdownText != null) countdownText.gameObject.SetActive(false); // Hide countdown at first
        
        UpdateUI(); 
    }

    public void StartDriving()
    {
        if (startScreen != null) startScreen.SetActive(false); // Hide the main menu
        
        // Instead of unfreezing time instantly, we start the countdown!
        StartCoroutine(CountdownRoutine()); 
    }

    // <-- NEW: The Countdown Sequence
    IEnumerator CountdownRoutine()
    {
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(true); // Show the text
            
            countdownText.text = "3";
            // We MUST use 'Realtime' because Time.timeScale is currently 0!
            yield return new WaitForSecondsRealtime(1f); 
            
            countdownText.text = "2";
            yield return new WaitForSecondsRealtime(1f);
            
            countdownText.text = "1";
            yield return new WaitForSecondsRealtime(1f);
            
            countdownText.text = "GO!";
            if (whistleAudio != null) whistleAudio.Play(); // Blow the whistle!
            if (tutorialScript != null) tutorialScript.StartTutorial();
            
            yield return new WaitForSecondsRealtime(1f);
            countdownText.gameObject.SetActive(false); // Hide the "GO!" text
        }
        else
        {
            if (whistleAudio != null) whistleAudio.Play();
        }

        Time.timeScale = 1f; // UNFREEZE time so the car can finally drive!
    }

    public void DeductMarks(int amount, string reason)
    {
        if (isGameOver) return; 

        totalMarks -= amount;
        if (totalMarks < 0) totalMarks = 0; 
        
        UpdateUI(); 

        if (totalMarks < passingMarks)
        {
            TriggerFailState();
        }
    }

    public void UpdateUI()
    {
        if (marksText != null)
        {
            // Check the LanguageManager to see if Nepali is toggled ON
            if (LanguageManager.isNepali)
            {
                marksText.text = "अंक: " + totalMarks; // Nepali translation
            }
            else
            {
                marksText.text = "Marks: " + totalMarks; // Default English
            }
        }
    }

    private void TriggerFailState()
    {
        isGameOver = true;
        if (failScreen != null) failScreen.SetActive(true); 
        Time.timeScale = 0f; 
    }

    public void TrialPassed()
    {
        if (isGameOver) return; 

        isGameOver = true;
        if (passScreen != null) passScreen.SetActive(true); 
        Time.timeScale = 0f; 
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}
