using UnityEngine;

public class MainMenu : MonoBehaviour {

    /// <summary>
    /// Play Button
    /// </summary>
    public void PlayButton() {
        LevelManager.Instance.SelectLevel("Level Selection");
    }

    /// <summary>
    /// Settings button handler
    /// </summary>
    public void SettingsButton() {
        SettingsMenu.Instance.Open();
    }
    /// <summary>
    /// Play main sound track on main menu
    /// </summary>
    protected void Start() {
        MusicManager.Instance.PlayMainMusic();
    }
}
