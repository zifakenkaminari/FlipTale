using UnityEngine;
using System.Collections;

public class WaterFireGun : Item {

    public Sprite frontFull;
    public Sprite backFull;
    public Sprite frontFullOnHand;
    public Sprite backFullOnHand;
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
                frontOnHand = frontFullOnHand;
                backOnHand = backFullOnHand;
                player.GetComponent<Player>().pickItem(this);
                isFull = true;
            }
            if (hit.gameObject.name == "HotAirBallon" && !face && isFull) {
                drop (player);
                hit.GetComponent<HotAirBallon> ().getItem (gameObject);
                return true;
            }
        }
        return false;
    }
}
