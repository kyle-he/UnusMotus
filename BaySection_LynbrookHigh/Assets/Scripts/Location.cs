using UnityEngine;

/// <summary>
/// Represents a location on the board.
/// </summary>
public class Location {

    /// <summary>
    /// The north direction.
    /// </summary>
    public static Vector3 North = new Vector3(0, 1, 0);

    /// <summary>
    /// The south direction.
    /// </summary>
    public static Vector3 South = new Vector3(0, -1, 0);

    /// <summary>
    /// The east direction.
    /// </summary>
    public static Vector3 East = new Vector3(1, 0, 0);

    /// <summary>
    /// The west direction.
    /// </summary>
    public static Vector3 West = new Vector3(-1, 0, 0);

    /// <summary>
    /// The row component of this location.
    /// </summary>
    public int Row { get; }

    /// <summary>
    /// The column component of this location.
    /// </summary>
    public int Col { get; }

    /// <summary>
    /// Constructs a location with a specified row and column component.
    /// </summary>
    /// <param name="row">The row component</param>
    /// <param name="col">The column component</param>
    public Location(int row, int col) {
        Row = row;
        Col = col;
    }

    /// <summary>
    /// Constructs a location based on a certain cell position.
    /// </summary>
    /// <param name="cellPosition">The cell position</param>
    public Location(Vector3Int cellPosition) : this(cellPosition.x, cellPosition.y) { }

    /// <summary>
    /// All locations adjacent to this location.
    /// </summary>
    public Location[] AdjacentLocations {
        get {
            return new Location[] {
                GetAdjacentLocation(North),
                GetAdjacentLocation(East),
                GetAdjacentLocation(South),
                GetAdjacentLocation(West)
            };
        }
    }

    /// <summary>
    /// Retrieves all directions perpendicular to the given direction.
    /// </summary>
    /// <param name="direction">The direction</param>
    public static Vector3[] GetPerpendicularDirections(Vector3 direction) {
        return new Vector3[] {
            Vector3.Cross(direction, Vector3.forward),
            Vector3.Cross(direction, Vector3.back),
        };
    }

    /// <summary>
    /// Converts the location to a Unity tilemap position.
    /// </summary>
    /// <returns>The Unity tilemap position</returns>
    public Vector3Int ToPosition() {
        return new Vector3Int(Row, Col, 0);
    }

    /// <summary>
    /// Retrieves the adjacent location in a given direction.
    /// </summary>
    /// <param name="direction">The direction</param>
    /// <returns>The adjacent location</returns>
    public Location GetAdjacentLocation(Vector3 direction) {
        Vector3Int delta = Vector3Int.RoundToInt(direction.normalized);
        return new Location(Row + delta.x, Col + delta.y);
    }

    public override bool Equals(object obj) {
        if (obj is Location locObj) {
            return locObj.Row == this.Row && locObj.Col == this.Col;
        }
        return false;
    }

    public override int GetHashCode() {
        return Row * 1000 + Col;
    }
}
