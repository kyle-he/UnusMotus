using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public static LevelManager Instance { get; private set; }

    [SerializeField]
    private Animator _animation;
    private float _transitionTime = 0.3f;

    private bool _showDialogue = true;

    public int levelOn = 1;

    protected void Awake() {
        if (Instance == null) {
            SceneManager.sceneLoaded += OnSceneLoaded;
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Quit game
    /// </summary>
    public void QuitGame() {
        Application.Quit();
    }

    /// <summary>
    /// Reload level and start animation
    /// </summary>
    public void ReloadLevel() {
        StartCoroutine(_loadLevel(SceneManager.GetActiveScene().name, showDialogue: false));
    }

    /// <summary>
    /// Select level and start animation
    /// </summary>
    /// <param name="name">name of the scene</param>
    public void SelectLevel(string name) {
        StartCoroutine(_loadLevel(name));
    }

    /// <summary>
    /// Load level
    /// </summary>
    private IEnumerator _loadLevel(string name, bool showDialogue = true) {
        _animation.SetTrigger("Start");

        yield return new WaitForSeconds(_transitionTime);

        _showDialogue = showDialogue;

        SceneManager.LoadScene(name);
        GameManager.Instance.ResetBoard();
    }

    /// <summary>
    /// Trigger dialogue when scene is loaded
    /// </summary>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        DialogueTrigger dialogueTrigger = FindObjectOfType<DialogueTrigger>();
        if (dialogueTrigger) {
            dialogueTrigger.StartDialogue(_showDialogue);
        }
    }

    /// <summary>
    /// Get current level number from level name
    /// </summary>
    public int getLevelNumber(){
        return int.Parse(SceneManager.GetActiveScene().name.Replace("Level", ""));
    }

    /// <summary>
    /// Load the next level in build index
    /// </summary>
    public void LoadNextLevel() {
        var scenesInBuild = new List<string>();
        for (var i = 0; i < SceneManager.sceneCountInBuildSettings; i++) {
            var scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            var lastSlash = scenePath.LastIndexOf("/");
            scenesInBuild.Add(scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".") - lastSlash - 1));
        }

        levelOn = getLevelNumber();
        var name = "Level" + (levelOn + 1);

        if (scenesInBuild.Contains(name)) {
            SelectLevel(name);
        } else if (GameManager.Instance.GameMode == GameMode.Relaxed) {
            SelectLevel("Level Selection");
        } else {
            GameManager.Instance.levelOn = levelOn;
            SelectLevel("Game Over");
        }
    }
}
