using UnityEngine;
using System.Collections;


public class CaveThorn : Entity {
    public Sprite frontCaveBurned;
    public Sprite backCaveBurned;

	public IEnumerator burn() {
        front.GetComponent<SpriteRenderer>().sprite = frontCaveBurned;
		back.GetComponent<Animator> ().SetBool ("ignite", true);
		//back.GetComponent<SpriteRenderer>().sprite = backCaveBurned;
        GetComponent<Collider2D>().enabled = false;
		Manager.main.setPlayerControlable(false);
		yield return new WaitForSeconds(1.0f);
		Manager.main.setPlayerControlable(true);
    }


}
