using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	
	public Transform parentToReturnTo = null;
	public Transform placeholderParent = null;

	GameObject placeholder = null;

	public CardDisplay cardInfo;
	private GameHandler gameHandler;
	public int manaInt;
	public int atkInt;
	public int defenceInt;
	public int afterMana;
	public int afterDefence;
	public int afterAbility;

	public void OnBeginDrag(PointerEventData eventData) {
		//Debug.Log ("OnBeginDrag");

		CardDisplay cardInfo = gameObject.GetComponent<CardDisplay>();
		GameHandler gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();

		manaInt = cardInfo.manaInt;
		atkInt = cardInfo.attackInt;
		defenceInt = cardInfo.healthInt;
		afterMana = gameHandler.manaCounter - manaInt;
		afterDefence = gameHandler.defenceCounter + defenceInt;
		//afterAbility = (int)gameHandler.generatedCard.myAbility;
		afterAbility = cardInfo.abilityValue;
		//Debug.Log(afterMana);
		//Debug.Log(afterDefence);

		placeholder = new GameObject();
		placeholder.transform.SetParent(this.transform.parent);
		LayoutElement le = placeholder.AddComponent<LayoutElement>();
		le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
		le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
		le.flexibleWidth = 0;
		le.flexibleHeight = 0;

		placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

		parentToReturnTo = this.transform.parent;
		placeholderParent = parentToReturnTo;
		this.transform.SetParent(this.transform.parent.parent);

		GetComponent<CanvasGroup>().blocksRaycasts = false;




	}
	
	public void OnDrag(PointerEventData eventData) {
		//Debug.Log ("OnDrag");
		

		this.transform.position = eventData.position;

		if(placeholder.transform.parent != placeholderParent)
			placeholder.transform.SetParent(placeholderParent);

		int newSiblingIndex = placeholderParent.childCount;

		for(int i=0; i < placeholderParent.childCount; i++) {
			if(this.transform.position.x < placeholderParent.GetChild(i).position.x) {

				newSiblingIndex = i;

				if(placeholder.transform.GetSiblingIndex() < newSiblingIndex)
					newSiblingIndex--;

				break;
			}
		}

		placeholder.transform.SetSiblingIndex(newSiblingIndex);
		

	}
	
	public void OnEndDrag(PointerEventData eventData) {
		//Debug.Log ("OnEndDrag");
		this.transform.SetParent( parentToReturnTo );
		this.transform.SetSiblingIndex( placeholder.transform.GetSiblingIndex() );
		GetComponent<CanvasGroup>().blocksRaycasts = true;

		Destroy(placeholder);

		
	}
	
	
	
}
