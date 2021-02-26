using System.Collections.Generic;

/// <summary>
/// A button that activates and deactivates a laser.
/// </summary>
public class Button : BackgroundObject {

    /// <summary>
    /// The controller attatched to this button.
    /// </summary>
    public ButtonController Controller { get; private set; }

    /// <summary>
    /// The laser controller that this button controls.
    /// </summary>
    public List<LaserController> LaserControllers { get; private set; }

    /// <summary>
    /// Constructs a button and adds it to the board.
    /// </summary>
    /// <param name="location">The initial location.</param>
    /// <param name="controller">The controller for the button.</param>
    /// <param name="laserController">The laser controller the button is attached to.</param>
    public Button(Location location, ButtonController controller, List<LaserController> laserControllers) : base(location) {
        LaserControllers = laserControllers;
        Controller = controller;
        Interaction = new ButtonInteraction(this);
    }
}
