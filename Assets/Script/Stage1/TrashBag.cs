using UnityEngine;
using System.Collections;

public class TrashBag : Item {

	// Use this for initialization
	protected override void Start () {
        base.Start();
        //pickable = false;
	}

    public override bool use (GameObject player)
    {
        Collider2D[] hits = overlapAreaAll ();
        foreach (Collider2D hit in hits) {
            if (hit.gameObject.name == "HotAirBallon" && !face) {
                drop (player);
                hit.GetComponent<HotAirBallon> ().getItem (gameObject);
                return true;
            }
        }
        return false;
    }
}
