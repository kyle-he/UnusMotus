using UnityEngine;

/// <summary>
/// A controller attached to a normal player (left/right or up/down)
/// </summary>
public class PlayerController : BasePlayerController {
    /// <summary>
    /// Whether to allow horizontal movements
    /// </summary>
    [SerializeField]
    public bool _allowHorizontal = true;

    /// <summary>
    /// Whether to allow vertical movements
    /// </summary>
    [SerializeField]
    public bool _allowVertical = true;

    private bool _isMoving;

    protected void Awake() {
        var loc = GetInitialLocation();
        Object = new Player(loc, this);
    }

    protected override void Update() {
        var direction = Vector3.zero;

        if (_allowHorizontal) {
            // Check horizontal controller
            var value = Input.GetAxisRaw("Horizontal");
            if (Mathf.Abs(value) == 1f) {
                direction = Vector3.right * value;
            }
        }

        if (_allowVertical) {
            // Check vertical controller
            var value = Input.GetAxisRaw("Vertical");
            if (Mathf.Abs(value) == 1f) {
                direction = Vector3.up * value;
            }
        }

        if (direction == Vector3.zero) {
            // Return to non-moving state when key/controller is released
            _isMoving = false;
        } else if (!_isMoving && Object.Movement.AllowMoveInDirection(direction, initial: true)) {
            _isMoving = true;
            Object.Movement.MoveInDirection(direction, initial: true);
            PlayClip(_moveSound);
        }

        base.Update();
    }
}
