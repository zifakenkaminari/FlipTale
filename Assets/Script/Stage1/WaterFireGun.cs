using UnityEngine;
using System.Collections;

public class WaterFireGun : Item {

    public Sprite frontFull;
    public Sprite backFull;
    protected bool isFull;

    protected override void Start ()
    {
        isFull = false;
        base.Start ();
    }

    public override bool use (GameObject player)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 2f);
        foreach (Collider2D hit in hits) {
            if (hit.gameObject.name == "LakeIce" && face) {
                front.GetComponent<SpriteRenderer> ().sprite = frontFull;
                back.GetComponent<SpriteRenderer> ().sprite = backFull;
                name = "WaterFireGunFull";
                player.GetComponent<Player>().pickItem(this);
                isFull = true;
            }
			if (hit.gameObject.name == "HotAirBalloonBuilder" && !face && isFull) {
                drop (player);
				hit.GetComponent<HotAirBalloonBuilder> ().getItem (this);
                return true;
            }
        }
        return false;
    }
}
