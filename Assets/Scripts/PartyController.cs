using UnityEngine;

public class PartyController : MonoBehaviour
{
    [SerializeField] private CharacterClass[] _partyMembers;
    //[SerializeField] private PartyState _currentState = PartyState.Idle;
    [SerializeField] private  IPartyState _currentState;
    [SerializeField] private IdleState _idleState;
    [SerializeField] private MovementState _movementState;
    [SerializeField] private CombatState _combatState;

    public static PartyController Instance { get; private set; }
    private PartyController _controller;
    private PartyInput _input;
    private float _autoAttackTimer = 0f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        _controller = GetComponent<PartyController>();
        _input = new PartyInput(_controller);
    }

private void Update()
{
    _input.HandleInput();

    if (_currentState == PartyState.Combat)
    {
        // Check if there are any enemies left to fight
        if (PathManager.Instance.GetCurrentTile().EnemiesOnTile.Count == 0)
        {
            //SetState(PartyState.Idle); // Or Moving
            PathManager.Instance.DisablePauseMovement();
            Debug.Log("Combat finished. All enemies defeated.");
            return;
        }

        _autoAttackTimer += Time.deltaTime;

        if (_autoAttackTimer >= _partyMembers[0].AutoAttackDelay)
        {
            foreach (var partyMember in _partyMembers)
            {
                if (partyMember.IsAlive() && PathManager.Instance.GetCurrentTile().EnemiesOnTile.Count > 0)
                {
                    // Always attack the first enemy in the list
                    CharacterClass target = PathManager.Instance.GetCurrentTile().EnemiesOnTile[0];
                    partyMember.AutoAttack(target);

                        // If the target died, remove it
                        /*
                    if (!target.IsAlive())
                        {
                            PathManager.Instance.GetCurrentTile().EnemiesOnTile.Remove(target);
                        }*/
                }
            }
            _autoAttackTimer = 0f; // Reset timer AFTER all party members have attacked
        }
    }
}

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
    

    public void SetState(PartyState state)
    {

    }

    public void SetBranchChoice(int choice) => PathManager.Instance?.SetBranchChoice(choice);
    public void TogglePauseMovement() => PathManager.Instance?.TogglePauseMovement();
}