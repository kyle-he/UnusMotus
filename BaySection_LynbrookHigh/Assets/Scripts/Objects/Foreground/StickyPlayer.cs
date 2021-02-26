/// <summary>
/// A sticky player object that can be moved around.
/// </summary>
public class StickyPlayer : Player {
    /// <summary>
    /// Constructs a sticky player object and adds it to the board.
    /// </summary>
    /// <param name="location">The initial locataion</param>
    /// <param name="controller">The controller attached to the object</param>
    public StickyPlayer(Location location, StickyPlayerController controller) : base(location, controller) { }
}

