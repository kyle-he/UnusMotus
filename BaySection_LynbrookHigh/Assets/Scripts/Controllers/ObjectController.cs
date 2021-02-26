using System;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Base class for an object controller (controlling objects through Unity)
/// </summary>
public abstract class ObjectController : MonoBehaviour {

    /// <summary>
    /// The background tilemap for the current level.
    /// </summary>
    public Tilemap Tilemap { get; protected set; }

    /// <summary>
    /// Retrieves the initial location of the object.
    /// </summary>
    /// <returns>The initial location of the object.</returns>
    public Location GetInitialLocation() {
        Tilemap = FindObjectOfType<Tilemap>();
        Vector3Int position = Tilemap.WorldToCell(transform.position);
        return new Location(position);
    }

    /// <summary>
    /// Destroys the object.
    /// </summary>
    public void DestroySelf() {
        Destroy(gameObject);
    }
}
