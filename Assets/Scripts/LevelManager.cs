using UnityEngine;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;

    private void Awake() {
        if (main == null) {
            main = this;
            DontDestroyOnLoad(gameObject); // Optional: Keeps the LevelManager persistent across scenes
        }
        else {
            Destroy(gameObject); // Ensures only one instance exists
        }
    }

    // Optional: Additional level management methods
    public void ResetLevel() {
        // Logic to reset the level
    }
}

