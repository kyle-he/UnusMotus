/// <summary>
/// Base class for controlling background objects via Unity.
/// </summary>
public abstract class BackgroundObjectController : ObjectController {

    /// <summary>
    /// The object that this controller is controlling.
    /// </summary>
    public BackgroundObject Object { get; protected set; }
}
