using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

	public void OnPointerEnter(PointerEventData eventData) {
		//Debug.Log("OnPointerEnter");
		if(eventData.pointerDrag == null)
			return;

		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if(d != null) {
			d.placeholderParent = this.transform;
		}
	}
	
	public void OnPointerExit(PointerEventData eventData) {
		//Debug.Log("OnPointerExit");
		if(eventData.pointerDrag == null)
			return;

		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if(d != null && d.placeholderParent==this.transform) {
			d.placeholderParent = d.parentToReturnTo;
		}
	}
	
	public void OnDrop(PointerEventData eventData) {
		//Debug.Log (eventData.pointerDrag.name + " was dropped on " + gameObject.name);
		GameHandler gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

		foreach (Transform child in gameObject.transform)
		{
			GameObject.Destroy(child.gameObject);
		}
		if (d.afterMana >= 0)
		{
			gameHandler.playerCardAmount--;
			//gameHandler.manaCounter = d.afterMana;
			gameHandler.sfxPlaceCard.Play();
			Destroy(d.gameObject);

			int atk = d.atkInt;
			int mana = d.afterMana;
			int defence = d.afterDefence;
			int ability = d.afterAbility;
			Debug.Log(ability);
			gameHandler.CardPlaced(mana, atk, defence, ability);
			
		}
		else if (d != null)
		{
			//d.parentToReturnTo = d.parentToReturnTo; //return to the original parent, not stick to the new parent(dropped zone)
		}


	}
}
