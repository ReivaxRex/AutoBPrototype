using UnityEngine;

public class PartyController : MonoBehaviour
{
    public void HandleDebugInput()
    {
        var currentTile = PathManager.Instance?.GetCurrentTile();
        if (currentTile == null) return;
        
        if (currentTile.NextTiles.Count == 0 || currentTile.NextTiles[0] == null)
        {
            Debug.Log("No Next Tile");
        }
        else
        {
            Debug.Log($"Current Tile: {currentTile.name}");
            Debug.Log($"Next Tile: {currentTile.NextTiles[0].name}");
        }
    }

    public void SetBranchChoice(int choice) => PathManager.Instance?.SetBranchChoice(choice);
    public void TogglePauseMovement() => PathManager.Instance?.TogglePauseMovement();
}