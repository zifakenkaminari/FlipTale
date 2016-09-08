using UnityEngine;
using System.Collections;

public class SeedStone : Item
{
    public float onPotOffsetY;
    public GameObject flowerTorch;
    public float destroyPeriod;
    GameObject pot;

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
        if (pot && face)
        {
            GameObject newFlower = (GameObject)Instantiate(flowerTorch, player.transform.parent);
            Vector3 pos = pot.transform.position;
            pos.y = pot.transform.position.y + onPotOffsetY;
            newFlower.transform.position = pos;
            Destroy(gameObject);
            return true;
        }
        return false;
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Pot"))
        {
            pot = collider.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (pot && collider.gameObject == pot)
        {
            pot = null;
        }
    }
}
