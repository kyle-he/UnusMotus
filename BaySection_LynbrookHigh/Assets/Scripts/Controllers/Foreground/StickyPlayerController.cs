using UnityEngine;

/// <summary>
/// A controller for sticky player cells.
/// </summary>
public class StickyPlayerController : BasePlayerController {
    private void Awake() {
        var loc = GetInitialLocation();
        Object = new StickyPlayer(loc, this);
    }

    protected override void Update() {
        if (Input.GetButtonDown("Jump")) {
            Object.Sticking = !Object.Sticking;
            Object.Game.CleanupBoard();
        }

        base.Update();
    }
}
