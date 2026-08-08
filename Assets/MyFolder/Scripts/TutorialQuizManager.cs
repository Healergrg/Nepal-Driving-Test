using UnityEngine;
using TMPro; 

public class TutorialQuizManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject menuPanel;
    public GameObject guidePanel;
    public GameObject quizPanel;

    [Header("Quiz UI Elements")]
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI[] optionTexts; 
    public TextMeshProUGUI feedbackText; 

    [Header("Game Connection")]
    public GameManager gameManager; 

    private int currentQuestionIndex = 0;

    // --- ENGLISH ARRAYS ---
    private string[] engQuestions = {
        "Which key is used to apply the gas/throttle?",
        "What must you HOLD down to shift gears?",
        "Which key shifts the gear UP?",
        "How do you turn on your side lights (indicators)?"
    };
    private string[][] engOptions = {
        new string[] { "Spacebar", "W", "S" },
        new string[] { "Left SHIFT", "Left CTRL", "Spacebar" },
        new string[] { "Q", "C", "E" },
        new string[] { "A / D keys", "Left / Right Arrows", "Mouse Click" }
    };

    // --- NEPALI ARRAYS ---
    private string[] nepQuestions = {
        "ग्यास/थ्रोटल बढाउन कुन कुञ्जी (key) प्रयोग गरिन्छ?",
        "गियर परिवर्तन गर्न के होल्ड गर्नुपर्छ?",
        "कुन कुञ्जीले गियर बढाउँछ (UP)?",
        "तपाईंको साइड लाइट (इन्डिकेटर) कसरी अन गर्ने?"
    };
    private string[][] nepOptions = {
        new string[] { "Spacebar", "W", "S" },
        new string[] { "Left SHIFT", "Left CTRL", "Spacebar" },
        new string[] { "Q", "C", "E" },
        new string[] { "A / D keys", "Left / Right Arrows", "माउस क्लिक" }
    };

    // The correct button index for each question (0 = first button, 1 = second, 2 = third)
    private int[] correctAnswers = { 1, 0, 2, 1 }; 

    // --- BUTTON FUNCTIONS ---

    public void OpenGuide()
    {
        menuPanel.SetActive(false);
        guidePanel.SetActive(true);
    }

    public void StartQuiz()
    {
        guidePanel.SetActive(false);
        quizPanel.SetActive(true);
        currentQuestionIndex = 0;
        feedbackText.text = ""; 
        LoadQuestion();
    }

    private void LoadQuestion()
    {
        if (LanguageManager.isNepali)
        {
            questionText.text = nepQuestions[currentQuestionIndex];
            for (int i = 0; i < optionTexts.Length; i++)
            {
                optionTexts[i].text = nepOptions[currentQuestionIndex][i];
            }
        }
        else
        {
            questionText.text = engQuestions[currentQuestionIndex];
            for (int i = 0; i < optionTexts.Length; i++)
            {
                optionTexts[i].text = engOptions[currentQuestionIndex][i];
            }
        }
    }

    public void CheckAnswer(int buttonIndex)
    {
        CancelInvoke("RestartToGuide"); 

        if (buttonIndex == correctAnswers[currentQuestionIndex])
        {
            currentQuestionIndex++;

            if (currentQuestionIndex >= engQuestions.Length)
            {
                Debug.Log("Quiz Passed! Starting Game...");
                quizPanel.SetActive(false);
                guidePanel.SetActive(false); 
                
                // if (gameManager != null) { gameManager.StartGame(); }
            }
            else
            {
                feedbackText.text = LanguageManager.isNepali ? "सही! अर्को प्रश्न..." : "Correct! Next question...";
                feedbackText.color = Color.green;
                LoadQuestion();
            }
        }
        else
        {
            feedbackText.text = LanguageManager.isNepali ? "गलत! गाइड ध्यान दिएर पढ्नुहोस्।" : "Incorrect! Read the guide carefully.";
            feedbackText.color = Color.red;
            Invoke("RestartToGuide", 2f); 
        }
    }

    private void RestartToGuide()
    {
        quizPanel.SetActive(false);
        guidePanel.SetActive(true);
    }
}