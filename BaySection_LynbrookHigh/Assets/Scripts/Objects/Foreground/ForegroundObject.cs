public abstract class ForegroundObject : Object {

    public override Location Location {
        get { return Game.Foreground.GetLocation(this); }
        set {
            if (Location != null) Game.Foreground.Remove(Location);
            if (value != null) Game.Foreground.Put(value, this);
        }
    }

    /// <summary>
    /// The movement delegate attached to this object.
    /// </summary>
    public MovementDelegate Movement { get; protected set; }

    /// <summary>
    /// The collision delegate attached to this object.
    /// </summary>
    public CollisionDelegate Collision { get; protected set; }

    /// <summary>
    /// A variable to keep track of game state
    /// </summary>
    internal bool Visited { get; set; }

    /// <summary>
    /// Whether the object is currently sticking.
    /// </summary>
    public bool Sticking { get; set; }

    /// <summary>
    /// Whether the object has finished and exited the scene.
    /// </summary>
    public bool Finished { get; private set; }

    /// <summary>
    /// Whether the object is dead.
    /// </summary>
    public bool Dead { get; private set; }

    /// <summary>
    /// Constructs a foreground object at a location.
    /// </summary>
    /// <param name="loc">The initial location</param>
    public ForegroundObject(Location loc) : base(loc) { }

    /// <summary>
    /// Removes the object from the board.
    /// </summary>
    public void RemoveSelf() {
        Location = null;
    }

    /// <summary>
    /// Schedules death of the object at the end of this move.
    /// </summary>
    public void KillAfterMove() {
        Dead = true;
    }

    /// <summary>
    /// Schedules finishing of the object at the end of this move.
    /// </summary>
    public void FinishAfterMove() {
        Finished = true;
    }

    /// <summary>
    /// Recursively propagates the sticking attribute to neighbors.
    /// </summary>
    public void PropagateSticking() {
        if (Location == null) return;
        if (Visited) return;
        Visited = true;

        foreach (var loc in Location.AdjacentLocations) {
            var obj = Game.Foreground.Get(loc);
            if (!(obj is Player || obj is Crate)) continue;
            obj.Sticking = Sticking;
            obj.PropagateSticking();
        }
    }

    /// <summary>
    /// Recursively propagates and schedules deletion to sticking neighbors.
    /// </summary>
    public void PropagateDelete() {
        if (Location == null) return;
        if (Visited) return;
        Visited = true;

        foreach (var loc in Location.AdjacentLocations) {
            var obj = Game.Foreground.Get(loc);
            if (!(obj is Player)) continue;
            if (!obj.Sticking) continue;
            obj.Finished = Finished;
            obj.PropagateDelete();
        }
    }
}
