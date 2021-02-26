using UnityEngine;
using UnityEngine.SceneManagement;

public class HotkeyManager : MonoBehaviour {
    public static HotkeyManager Instance { get; private set; }

    protected void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Hotkey handler
    /// </summary>
    void Update() {
        var name = SceneManager.GetActiveScene().name;
        if (name.Contains("Level") && !name.Contains("Selection")) {
            if (Input.GetKeyDown(KeyCode.R)) {
                LevelManager.Instance.ReloadLevel();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SettingsMenu.Instance.toggleActive();
        }
    }
}
