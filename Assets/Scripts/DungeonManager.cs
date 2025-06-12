using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager Instance { get; private set; }

    [SerializeField] private List<Floor> _floors;
    private Floor _currentFloor;
    [SerializeField] private int _currentFloorIndex = 0;
    
    [SerializeField] private CombatManager _combatManager;
    // [SerializeField] private TreasureManager _treasureManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        if (_floors == null || _floors.Count == 0)
        {
            Debug.LogError("No floors assigned to DungeonManager!");
            return;
        }

        _currentFloor = _floors[_currentFloorIndex]; //
    }

    private void Start()
    {
        if (PathManager.Instance != null)
        {
            PathManager.Instance.Initialize(_currentFloor); //
        }
        else
        {
            Debug.LogError("PathManager instance not found!");
        }
    }

    
    private void OnEnable()
    {
        if (_currentFloor == null) return;

        // Subscribe to all tile events on the current floor
        foreach (var tile in _currentFloor.Tiles) //
        {
            tile.OnEntered += HandleTileEntered;
        }
    }

    
    private void OnDisable()
    {
        if (_currentFloor == null) return;

        // Unsubscribe from all tile events to prevent errors
        foreach (var tile in _currentFloor.Tiles) 
        {
            tile.OnEntered -= HandleTileEntered;
        }
    }

    private void HandleTileEntered(Tile tile)
    {
        Debug.Log($"Entered a tile of type: {tile.tileEventTypeType}");
        PathManager.Instance.EnablePauseMovement();

        switch (tile.tileEventTypeType)
        {
            case Tile.TileEvent.Enemy:
                // _combatManager.StartCombat(tile);
                Debug.Log("Starting Combat!");
                break;
            case Tile.TileEvent.Chest:
                // _treasureManager.OpenChest(tile);
                Debug.Log("Opening a Chest!");
                break;
            case Tile.TileEvent.Merchant:
                Debug.Log("Opening a Merchant!");
                break;
            case Tile.TileEvent.Trap:
                Debug.Log("Stepped on a Trap!");
                break;
            case Tile.TileEvent.Rest:
                Debug.Log("Entered Rest Area");
                break;
            case Tile.TileEvent.None:
                PathManager.Instance.DisablePauseMovement();
                break;
            case Tile.TileEvent.Start:
                Debug.Log("Starting Dungeon!");
                break;
            case Tile.TileEvent.End:
                Debug.Log("Floor Complete!");
                _currentFloorIndex++;
                if (TryChangeFloor(_currentFloorIndex))
                {
                    PathManager.Instance.ChangeFloor(_floors[_currentFloorIndex]);
                }
                break;
        }
    }


    public Floor GetCurrentFloor() => _currentFloor;

    public bool TrySetCurrentFloor(Floor floor)
    {
        if (floor == null) return false;
        
        _currentFloor = floor;
        return true;
    }
    
    public bool TryChangeFloor(int floorIndex)
    {
        if (floorIndex < 0 || floorIndex >= _floors.Count)
            return false;
            
        // update the event subscriptions
        // You'd call OnDisable() to clear old subscriptions, then change the floor, then call OnEnable() to create new ones.
        
        OnDisable(); // Unsubscribe from the old floor's tiles
        _currentFloor = _floors[floorIndex];
        _currentFloorIndex = floorIndex;
        OnEnable(); // Subscribe to the new floor's tiles
        
        return true;
    }
}