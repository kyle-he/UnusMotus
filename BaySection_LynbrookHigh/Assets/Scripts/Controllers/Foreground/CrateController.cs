/// <summary>
/// A controller for a crate object.
/// </summary>
public class CrateController : BasePlayerController {
    protected void Awake() {
        var loc = GetInitialLocation();
        Object = new Crate(loc, this);
    }
}
