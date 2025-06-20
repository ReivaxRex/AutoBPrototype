using UnityEngine;

[CreateAssetMenu(fileName = "ClassData", menuName = "Scriptable Objects/ClassData")]
public class ClassData : ScriptableObject
{
    public enum CharacterType
    {
        Characer,
        Enemy,
        NPC
    }
    
    public string className;
    public string description;
    // public Sprite icon;
    public int baseHealth;
    public int baseMana;
    public int baseAttack;
    public int baseDefense;
    // public int magicAttack;
    // public int magicDefense;
    public float baseAutoAttackDelay;

    #region Stats
    /*
    public int strength;
    public int dexterity;
    public int agility;
    public int intelligence;
    public int faith;
    public int vitality;
    public int endurance;
    public int luck;   
    */  
    #endregion


}
