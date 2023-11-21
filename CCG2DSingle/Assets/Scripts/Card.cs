using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string cardName;
    [TextArea(5, 10)]
    public string cardDescription;
    public Sprite cardArt;
    public int cardManaCost;
    public int cardATK;
    public int cardHealth;

    private GameHandler gameHandler;

    public enum MyAbility { 
        None, 
        Heal20, 
        NextCardDoubleDamage, 
        TwoExtraCard, 
        DoubleDoubleAttack,
        AddMana10,
        EnemyMissAtk,
    }
    public MyAbility myAbility;

}
