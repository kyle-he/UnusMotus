using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public enum GameMode {
    Relaxed,
    Challenge
}

/// <summary>
/// A class that manages a full game instance, with a board and objects.
/// </summary>
public class GameManager {
    private static GameManager _instance = null;

    public string playerName = "player 1";

    /// <summary>
    /// The number of lives the player has.
    /// </summary>
    /// <value>_lives</value>
    public int Lives { get; private set; } = 3;

    /// <summary>
    /// The number of moves the player has made.
    /// </summary>
    public int Moves { get; private set; } = 0;

    /// <summary>
    /// The game mode the player is in.
    /// </summary>
    public GameMode GameMode { get; private set; } = GameMode.Challenge;

    /// <summary>
    /// The grid of foreground objects in this game.
    /// </summary>
    public Grid<ForegroundObject> Foreground { get; private set; } = new Grid<ForegroundObject>();

    /// <summary>
    /// The grid of background objects in this game.
    /// </summary>
    public Grid<BackgroundObject> Background { get; private set; } = new Grid<BackgroundObject>();

    public int levelOn { get; set; } = 1;

    /// <summary>
    /// Retrieves the global game manager instance.
    /// </summary>
    /// <value>The game manager singleton</value>
    public static GameManager Instance {
        get {
            if (_instance == null) {
                _instance = new GameManager();
            }
            return _instance;
        }
    }

    /// <summary>
    /// Subtracts a life from the user.
    /// </summary>
    private IEnumerator _subtractLife() {
        Lives--;
        yield return new WaitForSeconds(1);
        if (Lives <= 0 && GameMode == GameMode.Challenge) {
            levelOn = LevelManager.Instance.getLevelNumber();
            LevelManager.Instance.SelectLevel("Game Over");
        } else {
            LevelManager.Instance.ReloadLevel();
        }
    }


    /// <summary>
    /// Resets the board in this game.
    /// </summary>
    public void ResetBoard() {
        Foreground = new Grid<ForegroundObject>();
        Background = new Grid<BackgroundObject>();
    }

    /// <summary>
    /// Resets the internal visited state of the board.
    /// </summary>
    private void _resetVisited() {
        foreach (var obj in Foreground.GetAll()) {
            obj.Visited = false;
        }
    }

    /// <summary>
    /// Cleans up the board and readies it for future actions.
    /// </summary>
    public void CleanupBoard() {
        _resetVisited();
        foreach (var obj in Foreground.GetAll()) {
            if (!(obj is StickyPlayer)) {
                obj.Sticking = false;
            }
        }
        foreach (var obj in Foreground.GetAll()) {
            if (obj is StickyPlayer) {
                obj.PropagateSticking();
            }
        }
        _resetVisited();
    }

    /// <summary>
    /// Called at the end of a move.
    /// </summary>
    public void FinishMove() {
        var dead = false;

        CleanupBoard();
        foreach (var obj in Foreground.GetAll()) {
            if (obj.Finished) {
                obj.PropagateDelete();
            }
        }
        _resetVisited();
        var delete = new List<ForegroundObject>();
        foreach (var obj in Foreground.GetAll()) {
            if (obj.Finished || obj.Dead) {
                Debug.Log(obj);
                delete.Add(obj);
            }
            if (obj.Dead && obj is Player) dead = true;
        }
        foreach (var obj in delete) {
            obj.RemoveSelf();
        }
        Foreground.ResetLimbo();
        CleanupBoard();
        Moves++;

        // Check if finished with level
        var finished = true;
        foreach (var obj in Foreground.GetAll()) {
            if (obj is Player) finished = false;
        }
        if (finished && !dead) {
            LevelManager.Instance.LoadNextLevel();
        }
        if (dead) {
            LevelManager.Instance.StartCoroutine(_subtractLife());
        }
    }

    /// <summary>
    /// Set Gamemode to relaxed
    /// </summary>
    public void setGameModeRelaxed() {
        GameMode = GameMode.Relaxed;
    }

    /// <summary>
    /// Set Gamemode to challenge
    /// </summary>
    public void setGameModeChallenge() {
        GameMode = GameMode.Challenge;
        Lives = 3;
        Moves = 0;
    }

    /// <summary>
    /// Get leaderboard entry and calculate score
    /// </summary>
    /// <param name="name">name of the gamemode</param>
    public LeaderboardEntry GetLeaderboardEntry() {
        int score = (int)(20000 * levelOn / Math.Sqrt((double)Moves));
        return new LeaderboardEntry(playerName, score, Moves, levelOn);
    }

    /// <summary>
    /// Sends a score to the online leaderboard.
    /// </summary>
    /// <param name="entry">Leaderboard Entry</param>
    /// <returns>Coroutine</returns>
    public IEnumerator SendScore(LeaderboardEntry entry) {
        var json = JsonUtility.ToJson(entry);
        UnityWebRequest www1 = new UnityWebRequest("https://api.unusmotus.com/leaderboard", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        www1.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www1.SetRequestHeader("Content-Type", "application/json");
        yield return www1.SendWebRequest();
    }
}
