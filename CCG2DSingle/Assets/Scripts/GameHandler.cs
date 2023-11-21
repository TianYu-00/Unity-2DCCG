using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameHandler : MonoBehaviour
{
    //==============================================================Handles Player Stats
    public int playerCardAmount;
    public int manaCounter = 0;
    public int healthCounter = 100;
    public int defenceCounter = 0;

    //==============================================================Handles Panels
    public GameObject panelColorSelector;
    public GameObject panelPlayerHand;
    public GameObject panelEnemy;
    //==============================================================Handles
    public TextMeshProUGUI countDownText;
    public TextMeshProUGUI manaCounterTMP;
    public TextMeshProUGUI healthCounterTMP;
    public TextMeshProUGUI defenceCounterTMP;

    //==============================================================Handles Audio
    public AudioSource sfxPlaceCard;
    public AudioSource shuffleCard;
    //==============================================================Handles Countdown
    public Slider timerSlider;
    private float remainingTime;
    private float sliderTimerMax;
    private bool roundTimerEnd;
    public float roundTimer = 10f;

    //==============================================================Handles Button
    public Button endTurnButton;
    //==============================================================Handles Color Blocks
    public GameObject[] listOfColors;  
    private int colorBlockArrayindex;
    int colorBlockTimer = 3;
    bool colorBlockIsShowing = false;

    //==============================================================Handles Card
    [Header("CardHandler")]
    public int amountOfCardsToDeal = 5;
    public GameObject baseCard;
    public CardDisplay cardDisplay;
    public Card[] cardData;
    public Card generatedCard;
    private int cardDataArrayindex;
    private int abilityCounter;

    //==============================================================Handles Enemy
    public int level;
    [System.Serializable]
    public class Wave
    {
        public GameObject[] enemies;
    }
    public Wave[] waves;
    private int amountOfWaves;
    private int waveCount;
    private int enemyAmount;

    int attackMultiplier = 1;
    int enemyAttackMultiplayer = 1;
    bool nextCard;

    public AudioSource deathSound;

    //==============================================================Handles Win/Lose
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject winloseMainmenuButton;
    


    private void Awake()
    {
        

    }
    // Start is called before the first frame update
    void Start()
    {
        //sliderTimerMax = 3; //Start game timer
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        winloseMainmenuButton.SetActive(false);

        roundTimerEnd = false;

        amountOfWaves = waves.Length;
        //Debug.Log(amountOfWaves);
        waveCount = 1;
        if (waveCount == 1)
        {
            LoadEnemy();
        }
        cardData = Resources.LoadAll<Card>("SO_Characters") as Card[];
        //countDownText.gameObject.SetActive(true);
        countDownText.gameObject.SetActive(false); //set countdowntext to false
        panelPlayerHand.SetActive(true);
        panelColorSelector.SetActive(false);

        StartGameDrawCards();
        StartCoroutine(StartColorBlockCountDown());
        
    }

    // Update is called once per frame
    void Update()
    {
        timerSlider.value = CalculateTimerSliderValue();
        if (remainingTime <= 0)
        {
            
            remainingTime = 0;
            //EndTurnButtonClick();
            if (roundTimerEnd == true)
            {
                roundTimerEnd = false;
                StartCoroutine(StartColorBlockCountDown());
                EnemyAttack();
                //EndTurnButtonClick();
                //StopCoroutine(StartColorBlockCountDown());
                //StopCoroutine(ColorBlockTimer());


            }


        }
        else if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }



        manaCounterTMP.text = manaCounter.ToString();
        healthCounterTMP.text = healthCounter.ToString();
        defenceCounterTMP.text = defenceCounter.ToString();

        enemyAmount = panelEnemy.transform.childCount;
        if (enemyAmount == 0 && waveCount < amountOfWaves)
        {
            deathSound.Play();
            waveCount++;
            LoadEnemy();
            
        }
        else if (enemyAmount == 0 && waveCount == amountOfWaves)
        {
            //Debug.Log("Level complete");
            WinGame();

        }


        if (healthCounter > 100)
        {
            healthCounter = 100;
        }
        else if (healthCounter <= 0)
        {
            LoseGame();
        }

    }

    float CalculateTimerSliderValue()
    {
        return (remainingTime / sliderTimerMax);
    }   

    public void LoadEnemy()
    {

        GameObject[] listOfEnemies = waves[waveCount - 1].enemies;
        foreach (GameObject enemies in listOfEnemies)
        {
            Instantiate(enemies, panelEnemy.transform);

        }


    }


    IEnumerator StartColorBlockCountDown()
    {
        endTurnButton.interactable = false;
        sliderTimerMax = 3;
        remainingTime = sliderTimerMax;
        


        //countDownText.text = "3";
        yield return new WaitForSeconds(1);
        //countDownText.text = "2";
        yield return new WaitForSeconds(1);
        //countDownText.text = "1";
        yield return new WaitForSeconds(1);
        //countDownText.text = "";

        if (colorBlockIsShowing == false)
        {
            colorBlockIsShowing = true;
            //Debug.Log("space pressed");
            //Debug.Log("ColorBlockTimerStarted");
            StartCoroutine(ColorBlockTimer()); //Start color block end
            //Debug.Log("RandomColorBlockGenerating");
            for (int i = 0; i < 14; i++) //create color blocks
            {
                colorBlockArrayindex = Random.Range(0, listOfColors.Length);
                GameObject chosenColorBlock;
                chosenColorBlock = listOfColors[colorBlockArrayindex];
                Instantiate(chosenColorBlock, panelColorSelector.transform);
            }

            
            

        }
        

    }

    IEnumerator ColorBlockTimer()
    {
        panelColorSelector.SetActive(true);
        panelPlayerHand.SetActive(false);

        sliderTimerMax = colorBlockTimer;
        remainingTime = sliderTimerMax;

        yield return new WaitForSeconds(colorBlockTimer);
        foreach (Transform child in panelColorSelector.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        panelColorSelector.SetActive(false);
        panelPlayerHand.SetActive(true);
        colorBlockIsShowing = false;


        for (int i = 0; i < amountOfCardsToDeal; i++)
        {
            if (playerCardAmount < 9)
            {
                cardDataArrayindex = Random.Range(0, cardData.Length);
                generatedCard = cardData[cardDataArrayindex];
                cardDisplay.card = generatedCard;
                shuffleCard.Play();
                Instantiate(baseCard, panelPlayerHand.transform);
                playerCardAmount++;

            }

        }
        amountOfCardsToDeal = 1;





        sliderTimerMax = roundTimer; //round timer
        remainingTime = sliderTimerMax;
        roundTimerEnd = true;
        endTurnButton.interactable = true;

    }

    IEnumerator StartGameDrawCardsCountDown()
    {
        for (int i = 0; i < amountOfCardsToDeal; i++)
        {
            //Debug.Log("Card" + i);
            
            yield return new WaitForSeconds(0.5f);
            cardDataArrayindex = Random.Range(0, cardData.Length);
            generatedCard = cardData[cardDataArrayindex];
            cardDisplay.card = generatedCard;
            shuffleCard.Play();
            Instantiate(baseCard, panelPlayerHand.transform);
            
            playerCardAmount++;





        }
        amountOfCardsToDeal = 1;
    }
    




    void StartGameDrawCards()
    {

        StartCoroutine(StartGameDrawCardsCountDown());
    }

    public void EndTurnButtonClick()
    {

        //sliderTimerMax = 0;
        remainingTime = 0;
        roundTimerEnd = true;
        


    }


    public void EnemyAttack()
    {
        GameObject[] enemyAttackText = GameObject.FindGameObjectsWithTag("EnemyAttack"); // find tag
        foreach (GameObject enemyATKText in enemyAttackText) // loop through the tags
        {
            int oldHP;
            int newHP;
            int tempHP;
            string tempString2;
            //tempString2 = enemyATKText.GetComponent<TextMeshProUGUI>().text; //get Tmpro component
            tempString2 = enemyATKText.GetComponent<TextMeshProUGUI>().text;
            Debug.Log(tempString2);
            int tempEnemyAttack = (int.Parse(tempString2) * enemyAttackMultiplayer);
            newHP = healthCounter - tempEnemyAttack;
            //newHP = healthCounter - int.Parse(tempString2);
            Debug.Log("New HP: " + newHP);
            
            oldHP = healthCounter;
            //newHP = newHP - defenceCounter;
            tempHP = oldHP - newHP;
            //Debug.Log("Temp hp: " + tempHP);
            //Debug.Log(defenceCounter);
            //healthCounter = newHP - defenceCounter;

            // X
            if (defenceCounter <= 0)
            {
                healthCounter = newHP;
            }
            else if (defenceCounter > 0)
            {
                int newDefenceCounter;

                newDefenceCounter = defenceCounter - tempEnemyAttack;
                if (newDefenceCounter >= 0)
                {
                    defenceCounter = newDefenceCounter;
                }
                else
                {
                    healthCounter = newDefenceCounter + healthCounter;
                    defenceCounter = 0;
                    
                }
            }

            // X

            //healthCounter = newHP + defenceCounter;
            //if (defenceCounter <= tempHP)
            //{
            //    defenceCounter = 0;
            //}
            //else
            //{
            //    defenceCounter = defenceCounter - tempHP;
            //}



            //healthCounterTMP.text = newHP.ToString();

        }
        enemyAttackMultiplayer = 1;
    }

    public void CardPlaced(int manaInt,int atkInt, int defenceInt, int abilityInt)
    {
        
        
        //Debug.Log("Function CardPlaced has been called");
        manaCounter = manaInt;
        defenceCounter = defenceInt;
        abilityCounter = abilityInt;
        int tempAttack = atkInt * attackMultiplier;
        
        GameObject[] enemyHealthText = GameObject.FindGameObjectsWithTag("EnemyHealth"); // find tag
        foreach (GameObject enemyHPText in enemyHealthText) // loop through the tags
        {
            int enemyHealth;
            string tempString;
            tempString = enemyHPText.GetComponent<TextMeshProUGUI>().text; //get Tmpro component 
            //Debug.Log(tempString);
            enemyHealth = int.Parse(tempString) - tempAttack; //turn string into int minus the card attack 
            enemyHPText.GetComponent<TextMeshProUGUI>().text = enemyHealth.ToString(); //turn string back to int
            if (enemyHealth <= 0)
            {
                //What happens when enemy health is smaller than or equals to 0
                Destroy(GameObject.FindGameObjectWithTag("Enemy"));
                //Destroy(enemyHPText.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject); //destroy enemy
            }
            EnemyDisplay.FindObjectOfType<EnemyDisplay>().TakeDamageFunction();


        }

        Debug.Log(abilityCounter);
        attackMultiplier = 1;
        CardAbility(abilityCounter);


    }

    public void WinGame()
    {
        winPanel.SetActive(true);
        winloseMainmenuButton.SetActive(true);
        Time.timeScale = 0;
    }

    public void LoseGame()
    {
        losePanel.SetActive(true);
        winloseMainmenuButton.SetActive(true);
        Time.timeScale = 0;
    }





    public void CardAbility(int abilityCounter)
    {
        int abilityValue = abilityCounter;
        if (abilityValue == (int)Card.MyAbility.None)
        {
            Debug.Log("Ability: None"); //None Ability
        }//End of 
        else if (abilityValue == (int)Card.MyAbility.Heal20)
        {
            Debug.Log("Ability: Heal 20 HP"); 
            healthCounter = healthCounter + 20;
        }//End of 
        else if (abilityValue == (int)Card.MyAbility.NextCardDoubleDamage)
        {
            Debug.Log("Ability: Next card double damage"); 
            attackMultiplier = 2;

        }//End of 
        else if (abilityValue == (int)Card.MyAbility.TwoExtraCard)
        {
            Debug.Log("Ability: Pickup 1 Extra Card");
            for (int i = 0; i < 2; i++)
            {
                if (playerCardAmount < 9)
                {
                    cardDataArrayindex = Random.Range(0, cardData.Length);
                    generatedCard = cardData[cardDataArrayindex];
                    cardDisplay.card = generatedCard;
                    shuffleCard.Play();
                    Instantiate(baseCard, panelPlayerHand.transform);
                    playerCardAmount++;

                }

            }

        } //End of 
        else if (abilityValue == (int)Card.MyAbility.DoubleDoubleAttack)
        {
            Debug.Log("Ability: Next card double damage also next enemy attack deal double damage");
            attackMultiplier = 2;
            enemyAttackMultiplayer = 2;

        }//End of 
        else if (abilityValue == (int)Card.MyAbility.AddMana10)
        {
            Debug.Log("Ability: +10 Mana");
            manaCounter += 10;

        }//End of 
        else if (abilityValue == (int)Card.MyAbility.EnemyMissAtk)
        {
            Debug.Log("Ability: +10 Mana");
            enemyAttackMultiplayer = 0;

        }//End of 

    }


}
