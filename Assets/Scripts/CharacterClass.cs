using System.IO;
using Mono.Cecil;
using UnityEngine;

public abstract class CharacterClass : MonoBehaviour, IDamageable<int>
{
    [SerializeField] private ClassData _classData;
    //[SerializeField] private float _autoAttackDelay = 1f;
    public ClassData ClassData
    {
        get => _classData;
        set => _classData = value;
    }

    public string ClassName => _classData?.className ?? "Unknown Class";
    public string Description => _classData?.description ?? "No description available.";

    private int _currentHealth;
    private int _currentMana;
    private int _currentAttack;

    private int _currentDefense;

    private float _currentAutoAttackDelay;

    public int Health
    {
        get => _currentHealth;
        set { if (_classData != null) _currentHealth = value; }
    }

    public int Mana
    {
        get => _currentMana;
        set { if (_classData != null) _currentMana = value; }
    }
     public int Attack
    {
        get => _currentAttack;
        set { if (_classData != null) _currentAttack = value; }
    }
     public int Defense
    {
        get => _currentDefense;
        set { if (_classData != null) _currentDefense = value; }
    }
     public float AutoAttackDelay
    {
        get => _currentAutoAttackDelay;
        set { if (_classData != null) _currentAutoAttackDelay = value; }
    }
    
    public virtual void Initialize()
    {
        if (_classData == null)
        {
            Debug.LogError("ClassData is not assigned for " + ClassName);
            return;
        }

        // Initialize class-specific properties or behaviors here
        Debug.Log($"Initializing {ClassName} with Health: {Health}, Mana: {Mana}, Attack: {Attack}, Defense: {Defense}");
        _currentHealth = _classData.baseHealth;
        _currentMana = _classData.baseMana;
        _currentAttack = _classData.baseAttack;
        _currentDefense = _classData.baseDefense;
        _currentAutoAttackDelay = _classData.baseAutoAttackDelay;

        Debug.Log($"Class {ClassName} initialized successfully.");

    }
    public virtual void Damage(int damage)
    {
        Health -= damage; 
        Debug.Log($"{ClassName} took {damage} damage. Current health: {Health}");

    if (!IsAlive())
    {
        Die();
    }
}

    public virtual bool IsAlive()
    {
        return Health > 0;
    }
    
public virtual void Die()
    {
        Debug.Log($"{ClassName} has died.");

        
        // Notify the PathManager or any other relevant systems
        // Remove this enemy from the current tile's list
        Tile currentTile = PathManager.Instance.GetCurrentTile();
        if (currentTile != null && currentTile.EnemiesOnTile.Contains(this))
        {
            currentTile.EnemiesOnTile.Remove(this);
        }

        // Optional: Disable the GameObject
         gameObject.SetActive(false);
    }

    public virtual void AutoAttack(CharacterClass target)
    {
          if (target == null)
        {
            Debug.LogWarning($"{gameObject.name} cannot auto-attack because the target is null.");
            return;
        }

        target = target ?? ChooseTarget();
        if (!target.IsAlive())
        {
            Debug.LogWarning($"{gameObject.name} cannot auto-attack {target} because it's target is dead.");
            Debug.Log("Choosing a new target.");
            target = ChooseTarget();
            return; 
        }

        Debug.Log($"{gameObject.name} is auto-attacking {target.gameObject.name} for {Attack} damage.");
        target.Damage(Attack);
    
    }

    // Add a parameter to specify the target
    // If no target is specified, choose a random target from the current tile
    public CharacterClass ChooseTarget()
    {
        int targetIndex = Random.Range(0, PathManager.Instance.GetCurrentTile().EnemiesOnTile.Count);
        if (PathManager.Instance.GetCurrentTile().EnemiesOnTile.Count == 0)
        {
            Debug.LogWarning("No enemies on the current tile to choose from.");
            return null;
        }
        var target = PathManager.Instance.GetCurrentTile().EnemiesOnTile[targetIndex];
        if (target == null)
        {
            Debug.LogWarning("Chosen target is null.");
            return null;
        }
        Debug.Log($"{gameObject.name} has chosen {target.gameObject.name} as the target.");


        return target;
    }


    

}
