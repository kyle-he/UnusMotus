using UnityEngine;
using DG.Tweening;

/// <summary>
/// A delegate that defines the movement mechanics of a foreground object.
/// Collision refers to when a foreground object pushes, pulls, or slides on another object.
/// </summary>
public interface MovementDelegate {

    /// <summary>
    /// Whether the object is currently animating.
    /// </summary>
    bool Animating { get; set; }

    /// <summary>
    /// The foreground object that this delegate is attached to.
    /// </summary>
    ForegroundObject Object { get; }

    /// <summary>
    /// A method that is called when an object is moved in a given direction.
    /// </summary>
    /// <param name="direction">The direction</param>
    /// <param name="initial">Whether this is the initial move, or one caused by other movements</param>
    void MoveInDirection(Vector3 direction, bool initial = false);

    /// <summary>
    /// A method that defines whether an object can move in a given direction.
    /// </summary>
    /// <param name="direction">The direction</param>
    /// <param name="initial">Whether this is the initial move, or one caused by other movements</param>
    bool AllowMoveInDirection(Vector3 direction, bool initial = false);
}

/// <summary>
/// A movement delegate for non-movable objects.
/// </summary>
public class NotMovable : MovementDelegate {
    public bool Animating { get; set; } = false;
    public ForegroundObject Object { get; }

    /// <summary>
    /// Constructs a not movable delegate for a given object
    /// </summary>
    /// <param name="obj">The foreground object</param>
    public NotMovable(ForegroundObject obj) { Object = obj; }

    public void MoveInDirection(Vector3 direction, bool initial = false) { }
    public bool AllowMoveInDirection(Vector3 direction, bool initial = false) {
        return false;
    }
}

/// <summary>
/// A movement delegate for movable objects.
/// </summary>
public class Movable : MovementDelegate {
    public bool Animating { get; set; } = false;
    public ForegroundObject Object { get; }

    /// <summary>
    /// Constructs a movable delegate for a given object
    /// </summary>
    /// <param name="obj">The foreground object</param>
    public Movable(ForegroundObject obj) { Object = obj; }

    public virtual void MoveInDirection(Vector3 direction, bool initial = false) {
        if (Object.Location == null) return;
        if (Object.Visited) return;

        Object.Visited = true;

        var loc = Object.Location;

        // Push
        var nextLoc = Object.Location.GetAdjacentLocation(direction);
        var pushObj = Object.Game.Foreground.Get(nextLoc);
        if (pushObj != null) {
            pushObj.Collision.PushInDirection(Object, direction);
        }

        // Interact Exit
        var oldBgObj = Object.Game.Background.Get(loc);
        if (oldBgObj != null) {
            oldBgObj.Interaction.Exit(Object);
        }

        // Move
        if (!Object.Dead && !Object.Finished) Object.Location = nextLoc;

        // Sticking
        if (Object.Sticking) {
            // Pull
            var pullLoc = loc.GetAdjacentLocation(-direction);
            var pullObj = Object.Game.Foreground.Get(pullLoc);
            if (pullObj != null) {
                pullObj.Collision.PullInDirection(Object, direction);
            }

            // Slide
            foreach (var dir in Location.GetPerpendicularDirections(direction)) {
                var slideLoc = loc.GetAdjacentLocation(dir);
                var slideObj = Object.Game.Foreground.Get(slideLoc);
                if (slideObj != null) {
                    slideObj.Collision.SlideInDirection(Object, direction);
                }
            }
        }

        // Interact Enter
        if (!Object.Dead && !Object.Finished) {
            var newBgObj = Object.Game.Background.Get(Object.Location);
            if (newBgObj != null) {
                newBgObj.Interaction.Enter(Object);
            }
        }

        // Reset

        if (initial) {
            Object.Game.FinishMove();
        }
    }

    public virtual bool AllowMoveInDirection(Vector3 direction, bool initial = false) {
        if (Object.Location == null) return false;
        if (Object.Visited) return true;

        Object.Visited = true;

        var allow = true;

        // Push

        var nextLoc = Object.Location.GetAdjacentLocation(direction);
        var obj = Object.Game.Foreground.Get(nextLoc);
        if (obj != null) {
            allow &= obj.Collision.AllowPushInDirection(Object, direction);
        }

        // Sticking

        if (Object.Sticking) {
            // Pull
            var pullLoc = Object.Location.GetAdjacentLocation(-direction);
            var pullObj = Object.Game.Foreground.Get(pullLoc);
            if (pullObj != null) {
                allow &= pullObj.Collision.AllowPullInDirection(Object, direction);
            }

            // Slide
            foreach (var dir in Location.GetPerpendicularDirections(direction)) {
                var slideLoc = Object.Location.GetAdjacentLocation(dir);
                var slideObj = Object.Game.Foreground.Get(slideLoc);
                if (slideObj != null) {
                    allow &= slideObj.Collision.AllowSlideInDirection(Object, direction);
                }
            }
        }

        if (initial) {
            Object.Game.CleanupBoard();
        }

        return allow;
    }
}

/// <summary>
/// A delegate built on top of MovementDelegate for animated movable objects.
/// </summary>
public class AnimatedMovable : Movable {
    private static float _timeToMove = 0.08f;
    private ObjectController _controller;

    /// <summary>
    /// Constructs an animated movable delegate for a given object and controller.
    /// </summary>
    /// <param name="obj">The object</param>
    /// <param name="controller">The controller</param>
    public AnimatedMovable(ForegroundObject obj, ObjectController controller) : base(obj) {
        _controller = controller;
    }

    public override void MoveInDirection(Vector3 direction, bool initial = false) {
        if (Object.Visited) return;

        Animating = true;

        base.MoveInDirection(direction, initial);
        var tween = _controller.transform.DOMove(_controller.transform.position + direction.normalized, _timeToMove);

        tween.OnComplete(() => { Animating = false; });
    }

    public override bool AllowMoveInDirection(Vector3 direction, bool initial = false) {
        return !Animating && base.AllowMoveInDirection(direction, initial);
    }
}
