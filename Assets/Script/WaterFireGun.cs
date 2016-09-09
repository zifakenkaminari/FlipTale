using UnityEngine;
using System.Collections;

public class WaterFireGun : Item {

    public Sprite frontFull;
    public Sprite backFull;
    public Sprite frontFullOnHand;
    public Sprite backFullOnHand;

    public override bool use (GameObject player)
    {
        Collider2D[] hits = overlapAreaAll ();
        foreach (Collider2D hit in hits) {
            if (hit.gameObject.name == "LakeIce" && face) {
                front.GetComponent<SpriteRenderer> ().sprite = frontFull;
                back.GetComponent<SpriteRenderer> ().sprite = backFull;
                frontOnHand = frontFullOnHand;
                backOnHand = backFullOnHand;
                player.GetComponent<Player>().pickItem(this);
            }
        }
        return false;
    }
}
