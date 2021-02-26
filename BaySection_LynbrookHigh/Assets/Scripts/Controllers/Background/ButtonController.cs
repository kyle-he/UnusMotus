using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

/// <summary>
/// A controller to represent a button that activates/deactivate a laser.
/// </summary>
public class ButtonController : BackgroundObjectController {

    /// <summary>
    /// The laser controller that this button is attached to.
    /// </summary>
    [SerializeField]
    private List<LaserController> _laserControllers;

    /// <summary>
    /// Whether this button needs to be held down.
    /// </summary>
    public bool HoldDown = true;

    private SpriteRenderer _spriteRenderer;
    private Light2D _light;
    private AudioSource _sound;
    private Color _origColor;

    protected void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _light = GetComponent<Light2D>();
        _sound = GetComponent<AudioSource>();
        _origColor = _spriteRenderer.color;
    }

    protected void Awake() {
        var loc = GetInitialLocation();
        Object = new Button(loc, this, _laserControllers);
    }

    /// <summary>
    /// Deactivates this button.
    /// </summary>
    public void PressDown() {
        _spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f);
        _light.intensity = 0;
        _sound.Play();
    }

    /// <summary>
    /// Activates this button.
    /// </summary>
    public void PressUp() {
        _spriteRenderer.color = _origColor;
        _light.intensity = 1;
        _sound.Play();
    }
}
