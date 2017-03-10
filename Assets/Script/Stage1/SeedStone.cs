using UnityEngine;
using System.Collections;

public class SeedStone : Item
{
    public GameObject flowerTorch;
    public float onPotOffsetY;
    public float destroyPeriod;
    public Sprite potTorch;

    protected override void Start()
    {
        base.Start();
	}

    public override void drop(GameObject player)
    {
        base.drop(player);
        pickable = false;
        StartCoroutine(disappear());
    }

    public IEnumerator disappear() {
        float timeNow = 0;
        while(timeNow < destroyPeriod){
            setAlpha(1-timeNow / destroyPeriod);
			timeNow += Time.deltaTime;
			yield return new WaitWhile(() => isFreezed);
        }
        Destroy(gameObject);
    }

    public override bool use(GameObject player)
    {
        if (face)
        {
            Collider2D[] hits = overlapAreaAll();
            foreach (Collider2D hit in hits)
            {
                if (hit.gameObject.name == "Pot" && !FlowerTorch.isSpawned())
                {
                    Pot pot = hit.gameObject.GetComponent<Pot>();
                    pot.StartCoroutine(pot.bloom (player));
                    player.GetComponent<Player> ().dropItem ();
                    Destroy (gameObject);
                    return true;
                }
            }
        }
        return false;
    }

}
