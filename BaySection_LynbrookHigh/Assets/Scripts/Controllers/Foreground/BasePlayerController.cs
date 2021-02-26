using System.Collections;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Base class for a player controller.
/// </summary>
public abstract class BasePlayerController : ObjectController {

    /// <summary>
    /// The object that this controller is controlling.
    /// </summary>
    public ForegroundObject Object { get; protected set; }

    /// <summary>
    /// The sprite the GameObject should switch to when sticky.
    /// </summary>
    [SerializeField]
    private Sprite _stickySprite;

    private ParticleSystem _particleSystem;
    private SpriteRenderer _spriteRenderer;
    private Sprite _originalSprite;
    protected AudioClip _moveSound;
    protected AudioClip _deathSound;
    protected AudioClip _finishSound;

    protected virtual void Start() {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalSprite = _spriteRenderer.sprite;
        _moveSound = Resources.Load<AudioClip>("Move");
        _deathSound = Resources.Load<AudioClip>("Explosion");
        _finishSound = Resources.Load<AudioClip>("ButtonEffect");
        MusicManager.Instance.PlayGameMusic();
    }

    protected virtual void Update() {
        if (Object.Sticking) {
            ChangeSprite(_stickySprite);
        } else {
            ChangeSprite(_originalSprite);
        }

        if (Object.Location == null && !Object.Movement.Animating) {
            Object.Movement.Animating = true;

            if (Object.Finished) {
                _spriteRenderer.DOFade(0, 0.12f).OnComplete(DestroySelf);
                PlayClip(_finishSound);
            } else if (Object.Dead) {
                if (_particleSystem != null) {
                    _spriteRenderer.DOFade(0, 0.01f);
                    _particleSystem.Stop();
                    _particleSystem.Play();
                    PlayClip(_deathSound);
                } else {
                    DestroySelf();
                    PlayClip(_deathSound);
                }
            }
        }
    }

    protected void OnParticleSystemStopped() {
        DestroySelf();
    }

    protected virtual void ChangeSprite(Sprite newSprite) {
        _spriteRenderer.sprite = newSprite;
    }

    public void PlayClip(AudioClip clip) {
        var source = Tilemap.GetComponent<AudioSource>();
        source.clip = clip;
        source.Play();
    }
}
