using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] private List<Tile> _tiles;
    [SerializeField] private Tile _entryTile;
    [SerializeField] private Tile _exitTile;

    public Tile EntryTile => _entryTile;
    public Tile ExitTile => _exitTile;
    public IReadOnlyList<Tile> Tiles => _tiles.AsReadOnly();

    private void Awake()
    {
        ValidateTiles();
    }

    private void ValidateTiles()
    {
        if (_tiles == null || _tiles.Count == 0)
        {
            Debug.LogError("No tiles assigned to floor!");
            return;
        }

        // Set entry tile if not manually assigned
        if (_entryTile == null && _tiles.Count > 0)
        {
            _entryTile = _tiles[0];
        }

        // Basic validation
        if (_entryTile == null)
        {
            Debug.LogError("No entry tile assigned!");
        }
    }

    public bool IsValidFloor() => _entryTile != null && _tiles.Count > 0;
}