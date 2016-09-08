using UnityEngine;
using System.Collections;

public class WaterFireGun : Item {

    public Sprite frontFull;
    public Sprite backFull;

    public override bool use (GameObject player)
    {
        Collider2D[] hits = overlapAreaAll ();
        foreach (Collider2D hit in hits) {
            if (hit.gameObject.name == "LakeIce" && face) {
                front.GetComponent<SpriteRenderer> ().sprite = frontFull;
                back.GetComponent<SpriteRenderer> ().sprite = backFull;
            }
        }
        return false;
    }
}
