using UnityEngine;
using System.Collections;

public class FlagAxe : Item {

    public Sprite FlagAxeUsed;

    public override bool use(GameObject player)
    {
        if (!face)
        {
            Collider2D[] hits = overlapAreaAll();
            foreach (Collider2D hit in hits)
            {
                if (hit.gameObject.GetComponent<TreeCut>())
                {
                    if (hit.gameObject.GetComponent<TreeCut>().isCut) 
                        return false;
                    if (hit.gameObject.GetComponent<TreeCut> ().cut ()) {
                        back.GetComponent<SpriteRenderer> ().sprite = FlagAxeUsed;
                        isUsed [1] = true;
                        player.GetComponent<Player>().back.GetComponent<Animator> ().Play (name + "_used_idle");
                    }
                    return false;
                }
            }
		}
        return false;
	}

}
