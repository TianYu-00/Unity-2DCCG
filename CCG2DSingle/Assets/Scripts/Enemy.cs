using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public Sprite enemyArt;
    public string enemyName;
    public string enemyHealth;
    public string enemyAttack;

    
}
