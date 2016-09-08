using UnityEngine;
using System.Collections;

public class FlowerTorch : Item
{
    protected override void Start()
    {
        base.Start();
        state = 3;
    }

    public override bool use(GameObject player)
    {
        if (!face)
        {
            Collider2D[] hits = overlapAreaAll();
            foreach (Collider2D hit in hits)
            {
                if (hit.gameObject.GetComponent<Cave>())
                {
                    hit.gameObject.GetComponent<Cave>().burn();
                    Destroy(gameObject);
                    return true;
                }
            }
        }
        return false;
    }

}
