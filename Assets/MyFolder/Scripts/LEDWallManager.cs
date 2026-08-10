using UnityEngine;
using TMPro;
using System.Collections;

public class LEDWallManager : MonoBehaviour
{
    [Header("Settings")]
    public TextMeshPro ledText; // The 3D text on the wall
    public float timeBetweenQuotes = 3f; // How long each quote stays fully visible
    public float fadeSpeed = 1f; // How fast the text fades in and out (1 second is usually perfect)

    [Header("English Quotes")]
    public string[] englishQuotes;

    [Header("Nepali Quotes")]
    public string[] nepaliQuotes;

    private int currentIndex = 0;

    void Start()
    {
        // Start the looping timer as soon as the game starts
        if (englishQuotes.Length > 0)
        {
            StartCoroutine(ChangeQuoteRoutine());
        }
    }

    IEnumerator ChangeQuoteRoutine()
    {
        // Ensure text starts fully visible
        Color startColor = ledText.color;
        startColor.a = 1f;
        ledText.color = startColor;

        while (true) 
        {
            // 1. CHANGE TEXT
            if (LanguageManager.isNepali && nepaliQuotes.Length > currentIndex)
            {
                ledText.text = nepaliQuotes[currentIndex];
            }
            else
            {
                ledText.text = englishQuotes[currentIndex];
            }

            // 2. FADE IN
            yield return StartCoroutine(FadeText(1f)); // 1f means fully visible

            // 3. WAIT WHILE READING
            yield return new WaitForSeconds(timeBetweenQuotes);

            // 4. FADE OUT
            yield return StartCoroutine(FadeText(0f)); // 0f means fully invisible

            // Move to the next quote
            currentIndex++;
            if (currentIndex >= englishQuotes.Length)
            {
                currentIndex = 0;
            }
        }
    }

    // This is the animation engine that handles the smooth transition
    IEnumerator FadeText(float targetAlpha)
    {
        Color currentColor = ledText.color;
        float startAlpha = currentColor.a;
        float timePassed = 0f;

        while (timePassed < fadeSpeed)
        {
            timePassed += Time.deltaTime;
            // Calculate the smooth transition
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timePassed / fadeSpeed);
            
            // Apply the new transparency to the text
            currentColor.a = alpha;
            ledText.color = currentColor;
            
            yield return null; // Wait for the next frame
        }
    }
}