using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour {
    private Text _text;

    [SerializeField]
    private Text _names;
    [SerializeField]
    private Text _scores;
    [SerializeField]
    private Text _moves;
    [SerializeField]
    private Text _levels;

    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Text _description;

    private List<LeaderboardEntry> _entries;

    /// <summary>
    /// Edit input box and get user score
    /// </summary>
    protected void Awake() {
        _text = GetComponent<Text>();
        LeaderboardEntry entry = GameManager.Instance.GetLeaderboardEntry();
        _description.text = $"You scored {entry.score}, reaching level {entry.level} in {entry.moves} moves. Please enter your name if you would like to enter the global leaderboard:";
    }
    /// <summary>
    /// Get leaderboard and play music
    /// </summary>
    protected void Start() {
        StartCoroutine(GetLeaderboard());
        MusicManager.Instance.PlayMainMusic();
    }

    /// <summary>
    /// Get leaderboard from api 
    /// </summary>
    IEnumerator GetLeaderboard() {
        var www = UnityWebRequest.Get("https://api.unusmotus.com/leaderboard");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError) {
            _text.text = "Error loading leaderboard.";
            yield break;
        }

        _entries = JsonHelper.FromJson<LeaderboardEntry>("{\"Items\": " + www.downloadHandler.text + "}");
        _updateEntries();
    }

    /// <summary>
    /// Update leaderboard display
    /// </summary>
    private void _updateEntries() {
        var entries = _entries.ToList();
        entries.Add(GameManager.Instance.GetLeaderboardEntry());
        entries.Sort((x, y) => -x.score.CompareTo(y.score));

        _names.text = String.Join("\n", entries.Take(10).Select(x => x.name));
        _scores.text = String.Join("\n", entries.Take(10).Select(x => x.score));
        _moves.text = String.Join("\n", entries.Take(10).Select(x => x.moves));
        _levels.text = String.Join("\n", entries.Take(10).Select(x => x.level));
    }

    /// <summary>
    /// Update player name
    /// </summary>
    public void updatePlayerName(string playerName) {
        GameManager.Instance.playerName = playerName;
        _updateEntries();
    }

    /// <summary>
    /// Submit score to leaderboard api
    /// </summary>
    public void SubmitScore() {
        var entry = GameManager.Instance.GetLeaderboardEntry();
        StartCoroutine(GameManager.Instance.SendScore(entry));
        closeInput();
    }

    /// <summary>
    /// Close the input screen (animator)
    /// </summary>
    public void closeInput() {
        _animator.SetBool("isOpen", false);
    }
}

public class JsonHelper {
    public static List<T> FromJson<T>(string json) {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    [Serializable]
    private class Wrapper<T> {
        public List<T> Items;
    }
}


[Serializable]
public class LeaderboardEntry {
    public string name;
    public int score;
    public int moves;
    public int level;

    public LeaderboardEntry(string name, int score, int moves, int reachedLevel) {
        this.name = name;
        this.score = score;
        this.moves = moves;
        this.level = reachedLevel;
    }
}