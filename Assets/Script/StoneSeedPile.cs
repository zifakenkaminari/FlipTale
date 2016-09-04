using UnityEngine;
using System.Collections;

public class StoneSeedPile : Item {
    public GameObject stoneSeed;

    public override void pick(GameObject player) {
        GameObject newSeed = (GameObject)Instantiate(stoneSeed);
        newSeed.pick(player);
        player.GetComponent<Player>().itemOnHand = newSeed;
    }

}
