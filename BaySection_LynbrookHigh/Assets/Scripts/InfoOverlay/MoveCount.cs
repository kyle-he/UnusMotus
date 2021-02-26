using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveCount : MonoBehaviour {
    private Text _text;

    protected void Awake() {
        _text = GetComponent<Text>();
    }

    protected void Update() {
        _text.text = GameManager.Instance.Moves.ToString();
    }
}
