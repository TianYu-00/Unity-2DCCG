using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public Card card;
    public Image artworkImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI attackText;
    public int attackInt;
    public TextMeshProUGUI healthText;
    public int healthInt;
    public TextMeshProUGUI manaText;
    public int manaInt;
    public int abilityValue;
    

    void Start()
    {
        //Debug.Log(card.name);
        nameText.text = card.cardName;
        descriptionText.text = card.cardDescription;
        artworkImage.sprite = card.cardArt;
        manaText.text = card.cardManaCost.ToString();
        attackText.text = card.cardATK.ToString();
        healthText.text = card.cardHealth.ToString();
        abilityValue = (int)card.myAbility;

        //=======================================================To be used for drag game mechanic
        int.TryParse(attackText.text, out attackInt);
        int.TryParse(healthText.text, out healthInt);
        int.TryParse(manaText.text, out manaInt);
        

    }

    







}
