using System;
using System.Collections.Generic;
using UnityEngine;



public class Tile : MonoBehaviour
{
    public enum TileEvent
    {
        None,
        Start,
        End,
        Enemy,
        Chest,
        Merchant,
        Trap,
        Rest
        
    }

    [SerializeField] private List<Tile> _nextTiles = new List<Tile>();
    [SerializeField] private Transform _pathNode;
    
    public TileEvent tileEventTypeType;
    public event Action<Tile> OnEntered;

    public void TriggerEvent()
    {
        OnEntered?.Invoke(this);
        Debug.Log("Entered");
    }
    
    public IReadOnlyList<Tile> NextTiles => _nextTiles.AsReadOnly();
    public Transform PathNode => _pathNode;

    public TileEvent getTileEventType => tileEventTypeType;
    
    /* Save for Later (Tile Generation)
    public void AddNextTile(Tile tile)
    {
        if (tile != null && !_nextTiles.Contains(tile))
        {
            _nextTiles.Add(tile);
        }
    }

    public void RemoveNextTile(Tile tile)
    {
        if (tile != null && _nextTiles.Contains(tile))
        {
            _nextTiles.Remove(tile);
        }
    }

    

    public void SetTileEvent(TileEvent _eventType)
    {
        tileEventType = _eventType;
    }
    
    public bool IsConnectedTo(Tile tile) => tile != null && _nextTiles.Contains(tile);
    */
}