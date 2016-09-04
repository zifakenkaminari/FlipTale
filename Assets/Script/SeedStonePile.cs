using UnityEngine;
using System.Collections;

public class SeedStonePile : Item
{
    public GameObject seedStone;

    public override void pick(GameObject player) {
        GameObject newSeed = (GameObject)Instantiate(seedStone);
        newSeed.GetComponent<Item>().pick(player);
        player.GetComponent<Player>().itemOnHand = newSeed.GetComponent<Item>();
    }

}
