using System.IO;
using UnityEngine;

public class MovementState : IPartyState
{
    private PartyController _partyController;

    public MovementState(PartyController partyController)
    {
        _partyController = partyController;
    }
    public void EnterState()
    {
        // Start Movement animation or any other initialization

        Debug.Log("Entered Movement State");
    }

    public void UpdateState()
    {
        PathManager.Instance.ExecuteMovement();
    }
    
    public void ExitState()
    {
        // Stop Movement animation or any other cleanup
        Debug.Log("Exiting Movement State");
    }


}
