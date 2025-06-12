using UnityEngine;

public class PartyInput
{
    private readonly PartyController _controller;

    public PartyInput(PartyController controller)
    {
        _controller = controller;
    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _controller.HandleDebugInput();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) _controller.SetBranchChoice(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) _controller.SetBranchChoice(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) _controller.SetBranchChoice(2);
        
        if (Input.GetKeyDown(KeyCode.P)) _controller.TogglePauseMovement();
    }
    
}