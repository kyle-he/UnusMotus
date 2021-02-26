using System.Collections;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public Dialogue dialogue;

    /// <summary>
    /// Start dialogue
    /// </summary>
    public void StartDialogue(bool showDialogue = true) {
        if (PlayerPrefs.GetInt("showDialogue", 1) == 1 && showDialogue) {
            StartCoroutine(TriggerDialogue());
        }
    }

    /// <summary>
    /// Start dialogue
    /// </summary>
    IEnumerator TriggerDialogue() {
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

}