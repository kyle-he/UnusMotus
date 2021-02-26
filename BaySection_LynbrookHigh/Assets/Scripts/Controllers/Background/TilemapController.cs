using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour {
    void Start() {
        // Get tilemap
        var tilemap = GetComponent<Tilemap>();

        foreach (var position in tilemap.cellBounds.allPositionsWithin) {
            // Get tile at position
            var tile = tilemap.GetTile(position);
            if (tile == null) {
                continue;
            }

            Location loc = new Location(position);

            // Add corresponding object
            switch (tile.name) {
                case "Wall":
                    new Wall(loc);
                    break;
                case "Goal":
                    new Goal(loc);
                    break;
            }

            if (tile.name.Contains("LaserNode")) {
                new Wall(loc);
            }
        }
    }
}
