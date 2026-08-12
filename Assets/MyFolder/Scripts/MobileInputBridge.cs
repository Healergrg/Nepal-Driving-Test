using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class MobileInputBridge : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Name this button (e.g., Gas, Brake, Left, Right)")]
    public string buttonName; 
    
    // The master dictionary that tracks all mobile buttons
    private static Dictionary<string, bool> mobileInputs = new Dictionary<string, bool>();

    void Start()
    {
        if (!mobileInputs.ContainsKey(buttonName))
        {
            mobileInputs.Add(buttonName, false);
        }
    }

    // Triggers when a thumb touches the screen
    public void OnPointerDown(PointerEventData eventData)
    {
        mobileInputs[buttonName] = true;
    }

    // Triggers when the thumb lifts off
    public void OnPointerUp(PointerEventData eventData)
    {
        mobileInputs[buttonName] = false;
    }

    // This is the magic command your car script will use to check if a button is pressed
    public static bool CheckButton(string name)
    {
        if (mobileInputs.ContainsKey(name))
        {
            return mobileInputs[name];
        }
        return false;
    }
}