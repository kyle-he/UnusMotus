/// <summary>
/// A wall that the objects cannot be pushed across.
/// </summary>
public class Wall : ForegroundObject {

    /// <summary>
    /// Constructs a wall object and adds it to the board.
    /// </summary>
    /// <param name="location">The initial location</param>
    public Wall(Location location) : base(location) {
        Movement = new NotMovable(this);
        Collision = new NotCollidable(this);
    }
}
