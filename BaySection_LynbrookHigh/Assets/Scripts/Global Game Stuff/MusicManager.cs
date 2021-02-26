using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class MusicManager : MonoBehaviour {
    public static MusicManager Instance { get; private set; }

    [SerializeField]
    private IntroLoop _mainMusic;

    [SerializeField]
    private IntroLoop _gameMusic;

    protected void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    protected void Update() {
        _mainMusic.Update();
        _gameMusic.Update();
    }

    /// <summary>
    /// Play main music
    /// </summary>
    public void PlayMainMusic() {
        if (_gameMusic.Playing) _gameMusic.Stop();
        if (!_mainMusic.Playing) _mainMusic.Play();
    }

    /// <summary>
    /// Play game music
    /// </summary>
    public void PlayGameMusic() {
        if (_mainMusic.Playing) _mainMusic.Stop();
        if (!_gameMusic.Playing) _gameMusic.Play();
    }

    /// <summary>
    /// Stop all music
    /// </summary>
    public void StopMusic() {
        _mainMusic.Stop();
        _gameMusic.Stop();
    }

}


[Serializable]
public class IntroLoop {
    public AudioSource Intro;
    public AudioSource Loop;
    public bool Continue = false;
    public bool Playing { get; private set; }

    /// <summary>
    /// Play sound
    /// </summary>
    public void Play() {
        if (!Continue) {
            Intro.Stop();
            Loop.Stop();
        }
        Intro.volume = 0;
        Loop.volume = 1;
        Playing = true;
        Intro.Play();
        Intro.DOFade(1f, 0.1f);
    }

    /// <summary>
    /// Stop sound
    /// </summary>
    public void Stop() {
        Playing = false;
        Intro.DOFade(0f, 0.1f).OnComplete(Intro.Pause);
        Loop.DOFade(0f, 0.1f).OnComplete(Loop.Pause);
    }

    public void Update() {
        if (Playing && !Intro.isPlaying && !Loop.isPlaying) {
            Loop.Play();
        }
    }
}