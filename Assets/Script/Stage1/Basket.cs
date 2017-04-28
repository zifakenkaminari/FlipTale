using UnityEngine;
using System.Collections;

public class Basket : Item {

    public override bool use (GameObject player)
	{
		if (!face) {
			Collider2D[] hits = overlapAreaAll ();
			foreach (Collider2D hit in hits) {
				if (hit.gameObject.name == "HotAirBalloonBuilder") {
					drop (player);
					hit.GetComponent<HotAirBalloonBuilder> ().getItem (this);
					return true;
				}
			}
		}
        return false;
    }
}
