using UnityEngine;

/// <summary>
/// A delegate that defines the collision mechanics of a foreground object.
/// Collision refers to when a foreground object pushes, pulls, or slides on another object.
/// </summary>
public interface CollisionDelegate {

    /// <summary>
    /// The foreground object that this delegate is attached to.
    /// </summary>
    ForegroundObject Object { get; }

    /// <summary>
    /// A method that is called whenever a foreground object pushes another object.
    /// </summary>
    /// <param name="otherObj">The foreground object</param>
    void PushInDirection(ForegroundObject otherObj, Vector3 direction);

    /// <summary>
    /// A method that defines whenever a foreground object can push another object.
    /// </summary>
    /// <param name="otherObj">The foreground object</param>
    bool AllowPushInDirection(ForegroundObject otherObj, Vector3 direction);

    /// <summary>
    /// A method that is called whenever a foreground object pulls another object.
    /// </summary>
    void PullInDirection(ForegroundObject otherObj, Vector3 direction);

    /// <summary>
    /// A method that defines whenever a foreground object can pull another object.
    /// </summary>
    /// <param name="otherObj">The foreground object</param>
    bool AllowPullInDirection(ForegroundObject otherObj, Vector3 direction);

    /// <summary>
    /// A method that is called whenever a foreground object slides on another object.
    /// </summary>
    void SlideInDirection(ForegroundObject otherObj, Vector3 direction);

    /// <summary>
    /// A method that defines whenever a foreground object can slide on another object.
    /// </summary>
    /// <param name="otherObj">The foreground object</param>
    bool AllowSlideInDirection(ForegroundObject otherObj, Vector3 direction);
}

/// <summary>
/// A collision delegate for objects that can neither be pushed nor pulled.
/// </summary>
public class NotCollidable : CollisionDelegate {

    public ForegroundObject Object { get; }

    /// <summary>
    /// Constructs a not collidable delegate for a given object.
    /// </summary>
    /// <param name="obj">The foreground object</param>
    public NotCollidable(ForegroundObject obj) { Object = obj; }

    public void PushInDirection(ForegroundObject otherObj, Vector3 direction) { }
    public bool AllowPushInDirection(ForegroundObject otherObj, Vector3 direction) {
        return false;
    }

    public void PullInDirection(ForegroundObject otherObj, Vector3 direction) { }
    public bool AllowPullInDirection(ForegroundObject otherObj, Vector3 direction) {
        return true;
    }

    public void SlideInDirection(ForegroundObject otherObj, Vector3 direction) { }
    public bool AllowSlideInDirection(ForegroundObject otherObj, Vector3 direction) {
        return true;
    }
}

/// <summary>
/// A collision delegate for objects that can be pushed and pulled.
/// </summary>
public class Collidable : CollisionDelegate {

    public ForegroundObject Object { get; }

    /// <summary>
    /// Constructs a collidable delegate for a given object.
    /// </summary>
    /// <param name="obj">The foreground object</param>
    public Collidable(ForegroundObject obj) { Object = obj; }

    public void PushInDirection(ForegroundObject otherObj, Vector3 direction) {
        Object.Movement.MoveInDirection(direction);
    }

    public bool AllowPushInDirection(ForegroundObject otherObj, Vector3 direction) {
        return Object.Movement.AllowMoveInDirection(direction);
    }

    public void PullInDirection(ForegroundObject otherObj, Vector3 direction) {
        Object.Movement.MoveInDirection(direction);
    }

    public bool AllowPullInDirection(ForegroundObject otherObj, Vector3 direction) {
        return Object.Movement.AllowMoveInDirection(direction);
    }

    public void SlideInDirection(ForegroundObject otherObj, Vector3 direction) {
        Object.Movement.MoveInDirection(direction);
    }

    public bool AllowSlideInDirection(ForegroundObject otherObj, Vector3 direction) {
        return Object.Movement.AllowMoveInDirection(direction);
    }
}

/// <summary>
/// A collision delegate to remove objects on collide.
/// </summary>
public class LaserCollidable : CollisionDelegate {

    public ForegroundObject Object { get; }

    /// <summary>
    /// Constructs a laser collidable delegate for a given object.
    /// </summary>
    /// <param name="obj">The foreground object</param>
    public LaserCollidable(ForegroundObject obj) { Object = obj; }

    public void PushInDirection(ForegroundObject otherObj, Vector3 direction) {
        otherObj.KillAfterMove();
    }

    public bool AllowPushInDirection(ForegroundObject otherObj, Vector3 direction) {
        return true;
    }

    public void PullInDirection(ForegroundObject otherObj, Vector3 direction) { }
    public bool AllowPullInDirection(ForegroundObject otherObj, Vector3 direction) {
        return true;
    }

    public void SlideInDirection(ForegroundObject otherObj, Vector3 direction) { }
    public bool AllowSlideInDirection(ForegroundObject otherObj, Vector3 direction) {
        return true;
    }
}
