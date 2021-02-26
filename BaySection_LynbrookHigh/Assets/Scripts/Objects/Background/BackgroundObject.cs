/// <summary>
/// Base class for a background object in the Puzzle game.
/// </summary>
public abstract class BackgroundObject : Object {

    public override Location Location {
        get { return Game.Background.GetLocation(this); }
        set {
            if (Location != null) Game.Background.Remove(Location);
            if (value != null) Game.Background.Put(value, this);
        }
    }

    /// <summary>
    /// The interaction delegate attached to this object.
    /// </summary>
    public InteractionDelegate Interaction { get; protected set; }

    /// <summary>
    /// Constructs a background object at a location.
    /// </summary>
    /// <param name="loc">The initial location of the object.</param>
    public BackgroundObject(Location loc) : base(loc) { }

    /// <summary>
    /// Removes this object from the game.
    /// </summary>
    public void RemoveSelf() {
        Location = null;
    }
}
