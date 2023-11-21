using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyDisplay : MonoBehaviour
{
    public Enemy enemy;
    public Image enemyArtworkImage;
    public TextMeshProUGUI enemyNameText;
    public TextMeshProUGUI enemyHealthText;
    public TextMeshProUGUI enemyAttackText;
    public Animator takeDamageAnimation;

    private void Start()
    {
        enemyNameText.text = enemy.enemyName;
        enemyHealthText.text = enemy.enemyHealth;
        enemyAttackText.text = enemy.enemyAttack;
        enemyArtworkImage.sprite = enemy.enemyArt;
        
    }

    public void TakeDamageFunction()
    {
        takeDamageAnimation.Play("TakeDamage");
        
    }
}
