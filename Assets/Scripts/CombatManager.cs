using UnityEngine;

public class CombatManager : MonoBehaviour
{
     CombatState _combatState;
    public static CombatManager Instance { get; private set; }

    public enum CombatState
    {
        NotInCombat,
        Started,
        Occuring,
        Victory,
    
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
    
    Floor _currentFloor;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentFloor = DungeonManager.Instance.GetCurrentFloor();

        foreach (Tile t in _currentFloor.Tiles)
        {
            t.OnEntered += Tile_OnEntered;
        }
    }

    private void Tile_OnEntered(Tile obj)
    {
        if (obj.getTileEventType == Tile.TileEvent.Enemy)
        {
            PathManager.Instance.EnablePauseMovement();
            StartCombat();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartCombat()
    {

        //PartyController.Instance.SetState(PartyController.PartyState.Combat);
        
        Debug.Log("CombatManager.StartCombat");
    }
}
