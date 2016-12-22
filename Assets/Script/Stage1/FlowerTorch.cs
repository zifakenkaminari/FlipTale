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
        setAlpha (0.0f);
    }
    public static bool isSpawned() {
        return spawned;
    }


    protected void OnDestroy() {
        spawned = false;
    }

    public override void pick (GameObject player)
    {
        base.pick (player);
        GameObject.Find ("Pot").GetComponent<Pot> ().empty ();
    }

    public override bool use(GameObject player)
    {
        Collider2D[] hits = overlapAreaAll ();
        if (!face) {
            foreach (Collider2D hit in hits) {
                if (hit.gameObject.GetComponent<CaveVine> ()) {
                    hit.gameObject.GetComponent<CaveVine> ().burn ();
                    Destroy (gameObject);
                    return true;
                }
            }
        }
        else {
            foreach (Collider2D hit in hits) {
                Tomb tomb = hit.gameObject.GetComponent<Tomb> ();
                if (tomb && !tomb.hasFlower) {
                    tomb.front.GetComponent<SpriteRenderer> ().sprite = tomb.frontWithFlower;
                    tomb.back.GetComponent<SpriteRenderer> ().sprite = tomb.backWithFlower;
                    tomb.hasFlower = true;
                    Destroy (gameObject);
                    return true;
                }
            }
        }
        return false;
    }

}
