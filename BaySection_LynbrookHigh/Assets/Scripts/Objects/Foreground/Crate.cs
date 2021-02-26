/// <summary>
/// A crate object that can be pushed around and activate buttons.
/// </summary>
public class Crate : ForegroundObject {
    private CrateController _controller;

    /// <summary>
    /// Constructs a crate object and adds it to the board.
    /// </summary>
    /// <param name="location">The initial location</param>
    /// <param name="controller">The controller</param>
    public Crate(Location location, CrateController controller) : base(location) {
        _controller = controller;
        Movement = new AnimatedMovable(this, controller);
        Collision = new Collidable(this);
    }
}
