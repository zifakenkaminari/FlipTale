using UnityEngine;
using System.Collections;

public class Basket : Item {

    public override bool use (GameObject player)
    {
        Collider2D[] hits = overlapAreaAll ();
        foreach (Collider2D hit in hits) {
            if (hit.gameObject.name == "HotAirBallon") {
                drop (player);
                hit.GetComponent<HotAirBallon> ().getItem (gameObject);
                return true;
            }
        }
        return false;
    }
}
