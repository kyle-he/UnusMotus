using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A script that manages the dialogue for a scene.
/// </summary>
public class DialogueManager : MonoBehaviour {

    [SerializeField]
    public Text _nameText;

    [SerializeField]
    public Text _dialogueText;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private AudioSource _typingAudio;

    private Queue<string> _sentences;

    void Start() {
        _sentences = new Queue<string>();
    }

    /// <summary>
    /// Get keyboard hotkeys
    /// </summary>
    void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            DisplayNextSentence();
        } else if (Input.GetKeyDown(KeyCode.P)) {
            EndDialogue();
        }
    }

    /// <summary>
    /// Start dialogue
    /// </summary>
    public void StartDialogue(Dialogue dialogue) {
        StartCoroutine(_startDialogue(dialogue));
    }

    /// <summary>
    /// Start dialogue
    /// </summary>
    private IEnumerator _startDialogue(Dialogue dialogue) {
        if (dialogue.sentences.Length == 0) yield break;

        _animator.SetBool("IsOpen", true);
        _nameText.text = dialogue.name;

        _sentences.Clear();
        foreach (string sentence in dialogue.sentences) {
            _sentences.Enqueue(sentence);
        }

        yield return new WaitForSeconds(0.5f);
        DisplayNextSentence();
    }

    /// <summary>
    /// Display next sentence in queue
    /// </summary>
    public void DisplayNextSentence() {
        if (_sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = _sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    /// <summary>
    /// Type sentence
    /// </summary>
    IEnumerator TypeSentence(string sentence) {
        _typingAudio.Play();
        _dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) {
            _dialogueText.text += letter;
            yield return new WaitForSeconds(0.005f);
        }
        _typingAudio.Stop();
    }

    /// <summary>
    /// End dialogue
    /// </summary>
    public void EndDialogue() {
        _typingAudio.Stop();
        _animator.SetBool("IsOpen", false);
    }

}