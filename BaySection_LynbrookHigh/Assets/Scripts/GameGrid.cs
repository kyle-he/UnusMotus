using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Represents a grid of objects at locations.
/// </summary>
/// <typeparam name="T">The type of object</typeparam>
public class Grid<T> {
    private Dictionary<Location, T> _grid = new Dictionary<Location, T>();
    private Dictionary<T, Location> _lookup = new Dictionary<T, Location>();
    public List<T> Limbo { get; private set; } = new List<T>();

    /// <summary>
    /// Retrieve the object at a specified location.
    /// </summary>
    /// <param name="location">The location</param>
    /// <returns>The object at that location</returns>
    public T Get(Location location) {
        if (_grid.ContainsKey(location)) {
            return _grid[location];
        } else {
            return default(T);
        }
    }

    /// <summary>
    /// Retrieves the location of a specified object.
    /// </summary>
    /// <param name="obj">The object</param>
    /// <returns>The location of that object</returns>
    public Location GetLocation(T obj) {
        if (_lookup.ContainsKey(obj)) {
            return _lookup[obj];
        } else {
            return null;
        }
    }

    /// <summary>
    /// Retrieves all the objects on the board.
    /// </summary>
    /// <returns>The objects on the board</returns>
    public IEnumerable<T> GetAll() {
        return _grid.Values.Concat(Limbo);
    }

    /// <summary>
    /// Resets the limbo collection
    /// </summary>
    public void ResetLimbo() {
        Limbo.Clear();
    }

    /// <summary>
    /// Puts an object at a specified location, removing an existing object if found.
    /// </summary>
    /// <param name="location">The location</param>
    /// <param name="obj">The object</param>
    public void Put(Location location, T obj) {
        Remove(location);
        _grid[location] = obj;
        _lookup[obj] = location;
    }

    /// <summary>
    /// Removes the object at a specified location.
    /// </summary>
    /// <param name="location">The location</param>
    public void Remove(Location location) {
        var obj = Get(location);
        if (obj != null) {
            Limbo.Add(obj);
            _lookup.Remove(obj);
            _grid.Remove(location);
        }
    }

    /// <summary>
    /// Prints out the board to the debug log.
    /// </summary>
    internal void DebugLog() {
        var minRow = _grid.Keys.Select(loc => loc.Row).Min();
        var maxRow = _grid.Keys.Select(loc => loc.Row).Max();
        var minCol = _grid.Keys.Select(loc => loc.Col).Min();
        var maxCol = _grid.Keys.Select(loc => loc.Col).Max();

        var line = "";
        for (int c = maxCol; c >= minCol; c--) {
            for (int r = minRow; r <= maxRow; r++) {
                var obj = Get(new Location(r, c));
                if (obj == null) {
                    line += ".";
                } else {
                    line += obj;
                }
                line += "\t";
            }
            line += "\n";
        }

        Debug.Log(line);
    }
}
