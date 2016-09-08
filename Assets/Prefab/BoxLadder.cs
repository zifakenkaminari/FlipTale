using UnityEngine;
using System.Collections;

public class BoxLadder : Item {

    protected override void use(GameObject player)
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
