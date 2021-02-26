using UnityEngine;

public class LevelSelection : MonoBehaviour {
    public void LevelButton(int level) {
        LevelManager.Instance.SelectLevel("Level" + level);
    }

    /// <summary>
    /// Quit game
    /// </summary>
    public void QuitGame() {
        Application.Quit();
    }

    /// <summary>
    /// Select scene
    /// </summary>
    /// <param name="name">name of the scene</param>
    public void SceneSelection(string name) {
        LevelManager.Instance.SelectLevel(name);
    }

    /// <summary>
    /// At start, play music
    /// </summary>
    protected void Start() {
        MusicManager.Instance.PlayMainMusic();
    }
}
