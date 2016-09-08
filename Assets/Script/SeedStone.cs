using UnityEngine;
using System.Collections;

public class SeedStone : Item
{
    public GameObject flowerTorch;
    public float onPotOffsetY;
    public float destroyPeriod;

    protected new void Start()
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
            while (isFreezed) yield return null;
            float rate = timeNow / destroyPeriod;
        	Color color = front.GetComponent<Renderer> ().material.color;
			color.a = 1 - rate;
			front.GetComponent<Renderer> ().material.color = color;

			color = back.GetComponent<Renderer> ().material.color;
			color.a = 1 - rate;
			back.GetComponent<Renderer> ().material.color = color;
            timeNow += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

    public override bool use(GameObject player)
    {
        if (face)
        {
            Collider2D[] hits = Physics2D.OverlapAreaAll(colliderTopLeft(), colliderBotRight());
            foreach (Collider2D hit in hits)
            {
                if (hit.gameObject.CompareTag("Pot"))
                {
                    GameObject pot = hit.gameObject;
                    GameObject newFlower = (GameObject)Instantiate(flowerTorch, player.transform.parent);
                    Vector3 pos = pot.transform.position;
                    pos.y = pot.transform.position.y + onPotOffsetY;
                    newFlower.transform.position = pos;
                    Destroy(gameObject);
                    return true;
                }
            }
        }
        return false;
    }

}
