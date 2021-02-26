/// <summary>
/// A goal object.
/// </summary>
public class Goal : BackgroundObject {

    /// <summary>
    /// Construts a goal object and adds it to the board.
    /// </summary>
    /// <param name="location">The initial location</param>
    public Goal(Location location) : base(location) {
        Interaction = new GoalInteraction(this);
    }
}
