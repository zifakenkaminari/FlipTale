using UnityEngine;
using System.Collections;

public class Bookcase : Item {

    public GameObject paper;

    public override void pick(GameObject player) {
        if (face) {
            GameObject newPaper = (GameObject)Instantiate (paper);
            newPaper.GetComponent<Item> ().pick (player);
        }
    }
}
