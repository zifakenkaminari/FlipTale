using UnityEngine;
using System.Collections;

public class FlagAxe : Item {

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
                    StartCoroutine(hit.gameObject.GetComponent<TreeCut>().cut());
                    return false;
                }
            }
		}
        return false;
	}

}
