using UnityEngine;

public class CombatState : IPartyState
{
    public void EnterState()
    {
        // Start Combat animation or any other initialization
        PathManager.Instance.EnablePauseMovement();
        Debug.Log("Entered Combat State");
    }
    public void UpdateState()
    {
        // Handle combat logic here
        if (PathManager.Instance.GetCurrentTile().EnemiesOnTile.Count == 0)
        {
            ExitState();
            PathManager.Instance.DisablePauseMovement();
            Debug.Log("Combat finished. All enemies defeated.");
        }
        
    }
    public void ExitState()
    {
        // Stop Combat animation or any other cleanup
        PathManager.Instance.DisablePauseMovement();
        Debug.Log("Exiting Combat State");
    }
}
