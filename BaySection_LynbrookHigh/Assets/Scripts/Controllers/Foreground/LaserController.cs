using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// A controller for a laser object.
/// </summary>
public class LaserController : ObjectController {

    /// <summary>
    /// The number of buttons that must be activated for the laser to turn on.
    /// </summary>
    [SerializeField]
    private int _numButtons = 1;

    private List<Laser> _objects = new List<Laser>();
    private List<Location> _nodes = new List<Location>();
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Sprite _originalSprite;

    private TileBase _onTile;
    private TileBase _offTile;

    protected void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _originalSprite = _spriteRenderer.sprite;
        _offTile = Resources.Load<TileBase>("LaserNodeOff");
    }

    protected void Awake() {
        var initialLoc = GetInitialLocation();
        var length = (int)transform.localScale.y;

        if (transform.rotation.z % 180 == 0) {
            // Vertical
            _nodes.Add(new Location(initialLoc.Row, initialLoc.Col - length / 2 - 1));
            _nodes.Add(new Location(initialLoc.Row, initialLoc.Col + length - length / 2));
        } else {
            // Horizontal
            _nodes.Add(new Location(initialLoc.Row - length / 2 - 1, initialLoc.Col));
            _nodes.Add(new Location(initialLoc.Row + length - length / 2, initialLoc.Col));
        }

        for (var r = _nodes[0].Row; r <= _nodes[1].Row; r++) {
            for (var c = _nodes[0].Col; c <= _nodes[1].Col; c++) {
                var loc = new Location(r, c);
                if (_nodes.Contains(loc)) continue;
                _objects.Add(new Laser(loc, this));
            }
        }

        _onTile = Tilemap.GetTile(_nodes[0].ToPosition());
    }

    /// <summary>
    /// Open the laser (turn it off)
    /// </summary>
    public void Open() {
        _numButtons--;

        if (_numButtons == 0) {
            // We've activated all the buttons needed to open the laser.

            _animator.SetBool("isLaserActive", false);
            foreach (var obj in _objects) obj.Open();
            foreach (var loc in _nodes) Tilemap.SetTile(loc.ToPosition(), _offTile);
        }
    }

    /// <summary>
    /// Close the laser (turn it back on)
    /// </summary>
    public void Close() {
        _numButtons++;

        if (_numButtons > 0) {
            // There are no longer enough buttons pressed down.

            _animator.SetBool("isLaserActive", true);
            foreach (var obj in _objects) obj.Close();
            foreach (var loc in _nodes) Tilemap.SetTile(loc.ToPosition(), _onTile);
        }
    }
}
