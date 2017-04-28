using UnityEngine;
using System.Collections;

public class Bookcase : Item {

    public GameObject paper;

    public override void pick(GameObject player) {
        if (face) {
			GetComponent<AudioSource> ().Play ();
            GameObject newPaper = (GameObject)Instantiate (paper, transform.parent);
            newPaper.GetComponent<Item> ().pick (player);
        }
    }
}
