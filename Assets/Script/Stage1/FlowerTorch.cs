using UnityEngine;
using System.Collections;

public class FlowerTorch : Item
{
    static bool spawned = false;

    protected override void Start()
    {
		base.Start();
		spawned = true;
        state = 3;
        setAlpha (0.0f);
		Debug.Log ("alpha?");
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
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        if (!face) {
            foreach (Collider2D hit in hits) {
                if (hit.gameObject.GetComponent<CaveThorn> ()) {
                    hit.gameObject.GetComponent<CaveThorn> ().burn ();
                    destroy (player);
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
                    destroy (player);
                    return true;
                }
            }
        }
        return false;
    }

}
