using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButtonScript : MonoBehaviour
{
    public GameHandler gameHandler;
    GameObject blockPlaceHolder = null;
    // Start is called before the first frame update
    void Start()
    {
        //GameHandler gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>(); //Find this object from the scene and use the "GameHandler" script
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BlockPlaceHolder()
    {
        //===========================================================================Color Block PLACE HOLDER
        blockPlaceHolder = new GameObject();
        blockPlaceHolder.transform.SetParent(this.transform.parent);
        LayoutElement layoutElement = blockPlaceHolder.AddComponent<LayoutElement>();
        layoutElement.preferredWidth = 100;
        layoutElement.preferredHeight = 100;
        layoutElement.flexibleWidth = 0;
        layoutElement.flexibleHeight = 0;
        blockPlaceHolder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
        //===========================================================================
    }

    public void BlueClicked()
    {
        //Debug.Log("ClickedBlue");
        GameHandler gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>(); //Find this object from the scene and use the "GameHandler" script
        gameHandler.manaCounter++;
        Destroy(gameObject);
        BlockPlaceHolder();
    }

    public void RedClicked()
    {
        ////Debug.Log("ClickedRed");
        GameHandler gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        gameHandler.healthCounter++;
        Destroy(gameObject);
        BlockPlaceHolder();


    }

    public void BlackClicked()
    {
        //Debug.Log("ClickedBlack");
        GameHandler gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        gameHandler.healthCounter--;
        Destroy(gameObject);
        BlockPlaceHolder();
    }

    public void WhiteClicked()
    {
        ////Debug.Log("ClickedWhite");
        GameHandler gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        gameHandler.defenceCounter++;
        Destroy(gameObject);
        BlockPlaceHolder();
    }
}
