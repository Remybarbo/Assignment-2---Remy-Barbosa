using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class menuScript : MonoBehaviour {

    // This function is called from the UI buttons on the menu screen
    public void triggerMenu(int trigger) {
        // The switch statement checks the value of trigger
        switch (trigger) {
            // If trigger is 0, load the gameScene scene
            case(0) :
                SceneManager.LoadScene("gameScene");
                break;
            // If trigger is 1, quit the application
            case(1) :
                Application.Quit();
                break;
        }
    }
}
