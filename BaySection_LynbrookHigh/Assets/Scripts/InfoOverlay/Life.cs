using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour {
    [SerializeField]
    private int _idx;

    private Image _image;
    private Sprite _lifeActiveSprite;
    private Sprite _lifeGoneSprite;

    protected void Awake() {
        _image = GetComponent<Image>();
        _lifeActiveSprite = Resources.Load<Sprite>("LifeActive");
        _lifeGoneSprite = Resources.Load<Sprite>("LifeGone");
    }

    protected void Update() {
        if (GameManager.Instance.Lives >= _idx && _image.sprite != _lifeActiveSprite) {
            _image.sprite = _lifeActiveSprite;
        }

        if (GameManager.Instance.Lives < _idx && _image.sprite != _lifeGoneSprite) {
            _image.sprite = _lifeGoneSprite;
        }
    }
}
