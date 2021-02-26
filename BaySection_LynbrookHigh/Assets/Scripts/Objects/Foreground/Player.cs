/// <summary>
/// A normal (left/right, up/down) player object that can be moved around.
/// </summary>
public class Player : ForegroundObject {
    /// <summary>
    /// Constructs a player object and adds it to the board.
    /// </summary>
    /// <param name="location">The initial location</param>
    /// <param name="controller">The controller controlling the player</param>
    public Player(Location location, ObjectController controller) : base(location) {
        Movement = new AnimatedMovable(this, controller);
        Collision = new Collidable(this);
    }
}
