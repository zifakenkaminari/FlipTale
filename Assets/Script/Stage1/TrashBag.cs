using UnityEngine;
using System.Collections;

public class TrashBag : Item {

	// Use this for initialization
	protected override void Start () {
        base.Start();
        pickable = false;
	}

    public override bool use (GameObject player)
    {
        Collider2D[] hits = overlapAreaAll ();
        foreach (Collider2D hit in hits) {
			if (hit.gameObject.name == "HotAirBalloonBuilder" && !face) {
                drop (player);
				hit.GetComponent<HotAirBalloonBuilder> ().getItem (this);
                return true;
            }
        }
        return false;
    }
}
