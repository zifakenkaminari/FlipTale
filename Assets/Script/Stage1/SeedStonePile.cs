using UnityEngine;
using System.Collections;

public class SeedStonePile : Item
{
    public GameObject seedStone;

    protected override void Start(){
        base.Start();
    }

    public override void pick(GameObject player) {
        GameObject newSeed = (GameObject)Instantiate(seedStone, transform.parent);
        newSeed.GetComponent<Item>().pick(player);
    }

}
