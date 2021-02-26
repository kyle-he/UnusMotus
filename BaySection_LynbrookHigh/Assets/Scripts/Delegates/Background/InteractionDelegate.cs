/// <summary>
/// A delegate that defines the interaction mechanics of a background object.
/// Interaction refers to when a foreground object moves on top of a background object.
/// </summary>
public interface InteractionDelegate {

    /// <summary>
    /// A method that is called whenever a foreground object moves onto a background object.
    /// </summary>
    /// <param name="otherObj">The foreground object</param>
    void Enter(ForegroundObject otherObj);

    /// <summary>
    /// A method that is called whenever a foreground object moves off a background object.
    /// </summary>
    /// <param name="otherObj">The foreground object</param>
    void Exit(ForegroundObject otherObj);
}

/// <summary>
/// A delegate that does nothing on interactions. Should be used by default
/// for most background objects.
/// </summary>
public class NoInteraction : InteractionDelegate {
    public void Enter(ForegroundObject otherObj) { }
    public void Exit(ForegroundObject otherObj) { }
}

/// <summary>
/// A delegate that deletes (exits) objects when they enter the goal.
/// </summary>
public class GoalInteraction : InteractionDelegate {

    /// <summary>
    /// The goal object that this delegate is attached to.
    /// </summary>
    public Goal Object { get; }

    /// <summary>
    /// Constructs a goal interaction delegate
    /// </summary>
    /// <param name="obj">The goal to control</param>
    public GoalInteraction(Goal obj) { Object = obj; }

    public void Enter(ForegroundObject otherObj) {
        if (otherObj is Player) otherObj.FinishAfterMove();
    }

    public void Exit(ForegroundObject otherObj) { }
}

/// <summary>
/// A delegate that controls the mechanics of a button activating
/// and deactivating a laser.
/// </summary>
public class ButtonInteraction : InteractionDelegate {

    /// <summary>
    /// The button object that this delegate is attached to.
    /// </summary>
    public Button Object { get; }

    /// <summary>
    /// Constructs a button interaction delegate
    /// </summary>
    /// <param name="obj">The button to control</param>
    public ButtonInteraction(Button obj) { Object = obj; }

    public void Enter(ForegroundObject otherObj) {
        // Open laser + disable button glow
        foreach (var c in Object.LaserControllers) {
            c.Open();
        }
        Object.Controller.PressDown();
    }

    public void Exit(ForegroundObject otherObj) {
        if (Object.Controller.HoldDown) {
            // Close laser + enable button glow
            foreach (var c in Object.LaserControllers) {
                c.Close();
            }
            Object.Controller.PressUp();
        }
    }
}