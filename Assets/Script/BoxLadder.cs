using UnityEngine;
using System.Collections;

public class BoxLadder : Item {

    public override bool isPickable()
    {
        if (!face)
            return false;
        else
            return base.isPickable();
    }
    protected override void held()
    {
        if (!face && state == 1)
        {
            GameObject player = transform.parent.gameObject;
            player.GetComponent<Player>().dropItem();
            drop(player);
        }
    }
    public override bool use(GameObject player)
    {

        Collider2D[] hits = overlapAreaAll();
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.GetComponent<Ladder>())
            {
                hit.gameObject.GetComponent<Ladder>().build();
                Destroy(gameObject);
                return true;
            }
        }
        return false;

    }
}
