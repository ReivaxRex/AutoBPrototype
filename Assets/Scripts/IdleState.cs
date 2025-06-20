using System.IO;
using UnityEngine;

public class IdleState : IPartyState
{
    private PartyController _partyController;

    public IdleState(PartyController partyController)
    {
        _partyController = partyController;
    }

    public void EnterState()
    {
        // Start Idle animation or any other initialization
        PathManager.Instance.EnablePauseMovement();
        Debug.Log("Entered Idle State");
    }

    public void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            //_partyController.SetState();
        }
    }

    public void ExitState()
    {
        // Stop Idle animation or any other cleanup
        PathManager.Instance.DisablePauseMovement();
        Debug.Log("Exiting Idle State");
    }
}
