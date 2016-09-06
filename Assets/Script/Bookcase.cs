using UnityEngine;
using System.Collections;

public class Bookcase : Item {

    public GameObject paper;

    public override void pick(GameObject player) {
        GameObject newPaper = (GameObject)Instantiate(paper);
        newPaper.GetComponent<Item>().pick(player);
        player.GetComponent<Player>().itemOnHand = newPaper.GetComponent<Item>();
    }
}
