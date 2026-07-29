// using UnityEngine;
// using TMPro;
// using UnityEngine.SceneManagement;

// public class GameManager : MonoBehaviour
// {
//     [Header("Trial Rules")]
//     public int totalMarks = 100;
//     public int passingMarks = 80;
//     public int redLightPenalty = 15; // <-- I added this missing piece back!

//     [Header("UI Connections")]
//     public TextMeshProUGUI marksText;
//     public GameObject failScreen;
//     public GameObject passScreen; 

//     private bool isGameOver = false;

//     void Start()
//     {
//         Time.timeScale = 1f; 
//         if (failScreen != null) failScreen.SetActive(false);
//         if (passScreen != null) passScreen.SetActive(false);
        
//         UpdateUI(); 
//     }

//     public void DeductMarks(int amount, string reason)
//     {
//         if (isGameOver) return; 

//         totalMarks -= amount;
//         if (totalMarks < 0) totalMarks = 0; 
        
//         UpdateUI(); 

//         if (totalMarks < passingMarks)
//         {
//             TriggerFailState();
//         }
//     }

//     private void UpdateUI()
//     {
//         if (marksText != null) marksText.text = "Marks: " + totalMarks;
//     }

//     private void TriggerFailState()
//     {
//         isGameOver = true;
//         if (failScreen != null) failScreen.SetActive(true); 
//         Time.timeScale = 0f; 
//     }

//     public void TrialPassed()
//     {
//         if (isGameOver) return; // Prevent winning if they already failed

//         isGameOver = true;
//         if (passScreen != null) passScreen.SetActive(true); // Show Win Screen
//         Time.timeScale = 0f; // Freeze game
//         Debug.Log("Congratulations! You passed with " + totalMarks + " marks.");
//     }

//     public void RestartGame()
//     {
//         Time.timeScale = 1f; 
//         SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
//     }
// }
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Trial Rules")]
    public int totalMarks = 100;
    public int passingMarks = 80;
    public int redLightPenalty = 15; 

    [Header("UI Connections")]
    public TextMeshProUGUI marksText;
    public GameObject startScreen; // <-- NEW: Added slot for Start Menu
    public GameObject failScreen;
    public GameObject passScreen; 

    private bool isGameOver = false;

    void Start()
    {
        // FREEZE TIME at the very beginning so the car doesn't roll away!
        Time.timeScale = 0f; 
        
        // Show the Start Guide, hide the victory/fail screens
        if (startScreen != null) startScreen.SetActive(true);
        if (failScreen != null) failScreen.SetActive(false);
        if (passScreen != null) passScreen.SetActive(false);
        
        UpdateUI(); 
    }

    // <-- NEW: This function runs when they click "Start Engine"
    public void StartDriving()
    {
        if (startScreen != null) startScreen.SetActive(false); // Hide the menu
        Time.timeScale = 1f; // UNFREEZE time so they can drive
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

    private void UpdateUI()
    {
        if (marksText != null) marksText.text = "Marks: " + totalMarks;
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