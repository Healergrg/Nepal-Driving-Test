using UnityEngine;
using TMPro;

public class LanguageManager : MonoBehaviour
{
    public static bool isNepali = false; 

    [Header("Main Menu UI")]
    public TextMeshProUGUI mainTitleText;
    public TextMeshProUGUI howToPlayButtonText;
    public TextMeshProUGUI languageButtonText;

    [Header("Controls Screen UI")]
    public TextMeshProUGUI controlsText; 
    public TextMeshProUGUI takeTestButtonText;

    [Header("Rules Screen UI")]
    public TextMeshProUGUI rulesTitleText; // <--- NEW: The big header!
    public TextMeshProUGUI rulesText;
    public TextMeshProUGUI startEngineButtonText;

    void Start()
    {
        UpdateText();
    }

    public void ToggleLanguage()
    {
        isNepali = !isNepali; 
        UpdateText();
        
        GameManager gm = FindFirstObjectByType<GameManager>();
        if (gm != null) { gm.UpdateUI(); }
    }

    private void UpdateText()
    {
        if (isNepali)
        {
            // --- MAIN MENU ---
            if (mainTitleText != null) mainTitleText.text = "नेपाल ड्राइभिङ ट्रायल सिमुलेसन";
            if (howToPlayButtonText != null) howToPlayButtonText.text = "कसरी खेल्ने";
            if (languageButtonText != null) languageButtonText.text = "ENG"; 

            // --- CONTROLS SCREEN ---
            if (controlsText != null) controlsText.text = "ड्राइभिङ कन्ट्रोलहरू\nW : अगाडि बढ्न (ग्यास)\nS : ब्रेक\nA / D : बायाँ / दायाँ मोड्न\nSPACE : ह्यान्डब्रेक\nLeft SHIFT (होल्ड) : क्लच (गियर फेर्न होल्ड गर्नुहोस्!)\nE : गियर बढाउन (UP)\nQ : गियर घटाउन (DOWN - रिभर्सको लागि 'R' मा झार्नुहोस्)\nLeft / Right ARROW : इन्डिकेटर अन / अफ\nC : क्यामेरा परिवर्तन";
            if (takeTestButtonText != null) takeTestButtonText.text = "ड्राइभ गर्न टेस्ट दिनुहोस्";

            // --- RULES SCREEN ---
            if (rulesTitleText != null) rulesTitleText.text = "ट्रायल नियम र स्कोरिङ"; // <--- NEW: Translates the title!
            if (rulesText != null) rulesText.text = "सुरुवाती अंक: १००\nपास हुने अंक: ७० (७० भन्दा तल झर्न नदिनुहोस्!)\n\nअंक कटौती:\n-१० अंक : ट्राफिक कोनलाई ठक्कर दिएमा।\n-१० अंक : मोड्नु अघि इन्डिकेटर (साइड लाइट) अन गर्न बिर्सेमा।\n-१० अंक : मोडिसकेपछि इन्डिकेटर अफ गर्न बिर्सेमा।\n-१५ अंक : रातो ट्राफिक लाइट काटेमा।\n\nशुभकामना!";
            if (startEngineButtonText != null) startEngineButtonText.text = "इन्जिन सुरु गर्नुहोस्";
        }
        else
        {
            // --- MAIN MENU ---
            if (mainTitleText != null) mainTitleText.text = "NEPAL DRIVING TRIAL SIMULATION";
            if (howToPlayButtonText != null) howToPlayButtonText.text = "HOW TO PLAY";
            if (languageButtonText != null) languageButtonText.text = "नेपाली"; 

            // --- CONTROLS SCREEN ---
            if (controlsText != null) controlsText.text = "DRIVING CONTROLS\nW : Throttle / Gas\nS : Foot Brake\nA / D : Steer Left / Right\nSPACE : Handbrake\nLeft SHIFT (Hold) : Clutch (Must hold to shift gears!)\nE : Shift Gear UP\nQ : Shift Gear DOWN (Shift down to 'R' to reverse)\nLeft / Right ARROW : Turn Indicators On / Off\nC : Change Camera View";
            if (takeTestButtonText != null) takeTestButtonText.text = "TAKE TEST TO DRIVE";

            // --- RULES SCREEN ---
            if (rulesTitleText != null) rulesTitleText.text = "TRIAL RULES & SCORING"; // <--- NEW!
            if (rulesText != null) rulesText.text = "Starting Marks: 100\nPassing Score: 70 (Do not drop below 70!)\n\nPOINT DEDUCTIONS:\n-10 Marks : Hitting a Traffic Cone.\n-10 Marks : Failing to turn ON your side light (indicator) before a turn.\n-10 Marks : Forgetting to turn OFF your side light after finishing a turn.\n-15 Marks : Running a Red Traffic Light.\n\nGood luck";
            if (startEngineButtonText != null) startEngineButtonText.text = "START ENGINE";
        }
    }
}