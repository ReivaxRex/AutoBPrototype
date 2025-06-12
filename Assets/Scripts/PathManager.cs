using UnityEngine;

public class PathManager : MonoBehaviour
{
    public static PathManager Instance { get; private set; }

    [SerializeField] private Party _party;
    [SerializeField] private float _partyMovementSpeed = 5f;
    
    private Tile _currentTile;
    private Tile _nextTile;
    private Tile _pendingBranchTile;
    private Tile _targetTileAfterMovement;
    private Tile _previousTile;
    
    private Vector3 _movementDestination;
    
    [SerializeField] private bool _isMoving = false;
    [SerializeField] private bool _isWaitingAtBranch = false;
    [SerializeField] private bool _isMovementPaused = false;
    
    private Floor _currentFloor;

    public float PartyMovementSpeed
    {
        get => _partyMovementSpeed;
        set => _partyMovementSpeed = Mathf.Max(0, value);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        
        Instance = this;
    }

    public void Initialize(Floor startingFloor)
    {
        if (startingFloor == null || !startingFloor.IsValidFloor())
        {
            Debug.LogError("Invalid floor provided to PathManager");
            return;
        }

        _currentFloor = startingFloor;
        _currentTile = _currentFloor.EntryTile;
        
        if (_party != null && _currentTile != null)
        {
            _party.transform.position = _currentTile.PathNode.position;
            MoveToNextTile();
        }
    }

    private void Update()
    {
        if (_isMoving)
        {
            UpdateMovement();
        }
        else if (!_isWaitingAtBranch && !_isMovementPaused)
        {
            MoveToNextTile();
        }
    }

    private void UpdateMovement()
    {
        Vector3 direction = (_movementDestination - _party.transform.position).normalized;
    
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            
            Quaternion offsetRotation = Quaternion.Euler(0, 90, 0); // Adjust Y-value as needed
            targetRotation *= offsetRotation; // Apply the offset

            _party.transform.rotation = Quaternion.Slerp(_party.transform.rotation, targetRotation, Time.deltaTime * _partyMovementSpeed * 0.5f);
        }

        _party.transform.position = Vector3.MoveTowards(
            _party.transform.position,
            _movementDestination,
            _partyMovementSpeed * Time.deltaTime
        );

        if (Vector3.Distance(_party.transform.position, _movementDestination) < 0.01f)
        {
            CompleteMovement();
        }
    }

    private void CompleteMovement()
    {
        _previousTile = _currentTile;
        _currentTile = _targetTileAfterMovement;
        _currentTile.TriggerEvent();
        _isMoving = false;
        MoveToNextTile();
    }

    public void MoveToNextTile()
    {
        if (_isWaitingAtBranch || _isMoving || _isMovementPaused) return;

        if (_currentTile == null || _currentTile.NextTiles.Count == 0)
        {
            Debug.Log("End of path reached");
            return;
        }

        if (_currentTile.NextTiles.Count > 1)
        {
            StartBranching(_currentTile);
            return;
        }
       
        StartMovement(_currentTile.NextTiles[0]);
    }

    private void StartBranching(Tile branchTile)
    {
        _isWaitingAtBranch = true;
        _pendingBranchTile = branchTile;
    }

    private void StartMovement(Tile nextTile)
    {
        if (nextTile == null) return;
        
        _nextTile = nextTile;
        _movementDestination = nextTile.PathNode.position;
        _targetTileAfterMovement = nextTile;
        _isMoving = true;
    }
    

    public void SetBranchChoice(int choice)
    {
        if (!_isWaitingAtBranch || _pendingBranchTile == null) return;
        
        if (choice >= 0 && choice < _pendingBranchTile.NextTiles.Count)
        {
            StartMovement(_pendingBranchTile.NextTiles[choice]);
        }
        else
        {
            Debug.LogError($"Invalid branch choice: {choice}");
        }
        
        _isWaitingAtBranch = false;
        _pendingBranchTile = null;
    }

    public void ChangeFloor(Floor newFloor)
    {
        Debug.Log($"Changing floor: {newFloor}");
        _party.transform.position = newFloor.EntryTile.PathNode.position;
        _currentTile = newFloor.EntryTile;
        _nextTile = newFloor.EntryTile.NextTiles[0];
        MoveToNextTile();
    }

    public void TogglePauseMovement() => _isMovementPaused = !_isMovementPaused;
    
    public void EnablePauseMovement() => _isMovementPaused = true;
    public void DisablePauseMovement() => _isMovementPaused = false;
    
    public Tile GetCurrentTile() => _currentTile;
}