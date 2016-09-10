using UnityEngine;
using System.Collections;

public class FlowerTorch : Item
{
    static bool spawned = false;

    protected override void Start()
    {
        spawned = true;
        base.Start();
        state = 3;
    }
    public static bool isSpawned() {
        return spawned;
    }


    protected void OnDestroy() {
        spawned = false;
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
