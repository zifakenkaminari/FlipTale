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
	[SerializeField]
	protected Sprite[] workStationOriginSprites;
	//3 items
	protected int itemCount;

	[SerializeField]
	protected HotAirBalloon hotAirBalloon;

	protected void Start () {
		itemCount = 0;
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Q) && Manager.main.GM_mode) {
			StartCoroutine(buildHotAirBalloon ());
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
			StartCoroutine(buildHotAirBalloon());
		}
	}

	protected IEnumerator buildHotAirBalloon(){

		Player player = GameObject.Find ("Player").GetComponent<Player> ();

		Manager.main.setFlippable(false);
		Manager.main.setPlayerControlable(false);

		yield return new WaitForSeconds (1.5f);
		yield return Camera.main.GetComponent<CameraController>().changeMaskColor(new Color(0, 0, 0, 0), Color.black, 1.5f);
		yield return new WaitForSeconds (1f);
		hotAirBalloon.gameObject.SetActive(true);
		Vector3 pos = player.transform.position;
		pos.x = hotAirBalloon.transform.position.x + 2f;
		player.transform.position = pos;

		workStaionComponents [0].setSprite (workStationOriginSprites[0], workStationOriginSprites[0]);
		workStaionComponents [1].setSprite (workStationOriginSprites[1], workStationOriginSprites[1]);
		workStaionComponents [2].setSprite (workStationOriginSprites[2], workStationOriginSprites[2]);
		yield return Camera.main.GetComponent<CameraController>().changeMaskColor(Color.black, new Color(0, 0, 0, 0), 1.5f);

		Manager.main.setPlayerControlable(true);
		Manager.main.setFlippable(true);
	}
		
}
