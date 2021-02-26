/// <summary>
/// The base class for all objects on the board.
/// </summary>
public abstract class Object {

    /// <summary>
    /// The game the object is in.
    /// </summary>
    public GameManager Game { get; }

    /// <summary>
    /// The location of the object.
    /// </summary>
    public virtual Location Location { get; set; }

    /// <summary>
    /// Constructs an object and adds it to the board.
    /// </summary>
    /// <param name="loc">The initial location</param>
    public Object(Location loc) {
        Game = GameManager.Instance;
        Location = loc;
    }
}
