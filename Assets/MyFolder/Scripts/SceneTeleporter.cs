using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTeleporter : MonoBehaviour
{
    [Header("Name of the Scene to Load")]
    public string sceneToLoad = "Nepal Driving Test"; // Make sure this perfectly matches your driving track scene name!

    void OnTriggerEnter(Collider other)
    {
        // Starter Assets tags the character as "Player" automatically
        if (other.CompareTag("Player")) 
        {
            // Unlock the mouse cursor before loading the next scene (Starter Assets hides the mouse)
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            // Teleport to the driving track!
            SceneManager.LoadScene(sceneToLoad); 
        }
    }
}