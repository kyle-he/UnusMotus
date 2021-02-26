using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {
    public static SettingsMenu Instance { get; private set; }

    public AudioMixer musicMixer;
    public AudioMixer soundMixer;

    public Slider musicSlider;
    public Slider soundSlider;
    public Toggle dialogueToggle;

    private Animator _animator;

    protected void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    protected void Start() {
        _animator = GetComponent<Animator>();
        PrefSet();
    }

    public bool isOpen = false;

    /// <summary>
    /// Open settings
    /// </summary>
    public void Open() {
        PrefSet();
        _animator.SetBool("openSettings", true);
        isOpen = true;
    }

    /// <summary>
    /// Reset preferences in PLayer Prefs
    /// </summary>
    public void Reset() {
        musicSlider.value = 0;
        soundSlider.value = 0;
        dialogueToggle.isOn = true;
    }

    /// <summary>
    /// Set preference in PLayer Prefs
    /// </summary>
    public void PrefSet() {
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        float soundVolume = PlayerPrefs.GetFloat("SoundVolume");
        bool showDialogue = getPrefBool("showDialogue");

        musicSlider.value = musicVolume;
        soundSlider.value = soundVolume;
        dialogueToggle.isOn = showDialogue;
    }

    /// <summary>
    /// Set music volume
    /// </summary>
    public void SetMusicVolume(float volume) {
        musicMixer.SetFloat("MusicVolume", volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    /// <summary>
    /// Set sound volume
    /// </summary>
    public void SetSoundVolume(float volume) {
        soundMixer.SetFloat("SoundVolume", volume);
        PlayerPrefs.SetFloat("SoundVolume", volume);
    }

    /// <summary>
    /// Toggle dialogue
    /// </summary>
    public void SetDialogue(bool showDialogue) {
        setPrefBool("showDialogue", showDialogue);
    }

    /// <summary>
    /// Get preference 
    /// </summary>
    private bool getPrefBool(string key) {
        // player prefs does not support booleans
        return PlayerPrefs.GetInt(key, 1) == 1 ? true : false;
    }

    /// <summary>
    /// Set preference boolean
    /// </summary>
    private void setPrefBool(string key, bool value) {
        // player prefs does not support booleans
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }

    /// <summary>
    /// Close settings
    /// </summary>
    public void Close() {
        _animator.SetBool("openSettings", false);
        isOpen = false;
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Toggle active
    /// </summary>
    public void toggleActive() {
        if (isOpen) {
            Close();
        } else {
            Open();
        }
    }
}
