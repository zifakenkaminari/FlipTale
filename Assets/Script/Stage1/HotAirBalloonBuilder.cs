using UnityEngine;
using System.Collections;

public class HotAirBalloonBuilder : MonoBehaviour {

	//3 components
	[SerializeField]
	protected Entity[] workStaionComponents;
	[SerializeField]
	protected Sprite[] workStationFrontSprites;
	[SerializeField]
	protected Sprite[] workStationBackSprites;
	//3 items
	protected int itemCount;

	[SerializeField]
	protected HotAirBalloon hotAirBalloon;

	protected void Start () {
		itemCount = 0;
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Q) && Manager.main.GM_mode) {

			hotAirBalloon.gameObject.SetActive(true);
		}
	}

	public void getItem(Item item) {
		if (item.name == "TrashBag") {
			workStaionComponents [0].setSprite (workStationFrontSprites[0], workStationBackSprites[0]);
		} else if(item.name == "WaterFireGun"){
			workStaionComponents [1].setSprite (workStationFrontSprites[1], workStationBackSprites[1]);
		}
		else if(item.name == "Basket"){
			workStaionComponents [2].setSprite (workStationFrontSprites[2], workStationBackSprites[2]);
		}
		Destroy (item.gameObject);
		itemCount++;
		if (itemCount == 3) {
			//spawn HotAirBalloon
			hotAirBalloon.gameObject.SetActive(true);
		}
	}

		
}
