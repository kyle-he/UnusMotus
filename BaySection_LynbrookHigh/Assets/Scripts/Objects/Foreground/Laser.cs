/// <summary>
/// A single tile of a laser that kills objects when they move into its path.
/// </summary>
public class Laser : ForegroundObject {
    private LaserController _controller;
    private Location _location;

    /// <summary>
    /// Constructs a laser object and adds it to the board.
    /// </summary>
    /// <param name="location">The initial location</param>
    /// <param name="controller">The controller attached to the laser</param>
    public Laser(Location location, LaserController controller) : base(location) {
        _location = location;
        _controller = controller;
        Movement = new NotMovable(this);
        Collision = new LaserCollidable(this);
    }

    /// <summary>
    /// Opens the laser (turns it off).
    /// </summary>
    public void Open() {
        Location = null;
    }

    /// <summary>
    /// Closes the laser (turns it on).
    /// </summary>
    public void Close() {
        var obj = Game.Foreground.Get(_location);
        if (obj != null && obj != this) {
            obj.KillAfterMove();
        }
        Location = _location;
    }
}
