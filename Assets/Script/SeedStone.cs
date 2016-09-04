using UnityEngine;
using System.Collections;

public class SeedStone : Item
{
    public float onPotOffsetY;
    public GameObject flowerTorch;
    public float destroyPeriod;
    GameObject pot;
    bool isNearPot;

    protected new void Start()
    {
        base.Start();
        isNearPot = false;
	}

    public override void drop(GameObject player)
    {
        Debug.Log("drop");
        base.drop(player);
        pickable = false;
        StartCoroutine(disappear());
    }

    public IEnumerator disappear() {
        float dropTime = Time.time;
        while(Time.time - dropTime < destroyPeriod){
            float rate = (Time.time - dropTime) / destroyPeriod;
        	Color color = front.GetComponent<Renderer> ().material.color;
			color.a = 1 - rate;
			front.GetComponent<Renderer> ().material.color = color;

			color = back.GetComponent<Renderer> ().material.color;
			color.a = 1 - rate;
			back.GetComponent<Renderer> ().material.color = color;
            yield return null;
        }
        Destroy(gameObject);
    }

    public override bool use(GameObject player)
    {
        if (isNearPot && face)
        {
            GameObject newFlower = Instantiate(flowerTorch);
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
            isNearPot = true;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (pot && collider.gameObject == pot)
        {
            isNearPot = false;
            pot = null;
        }
    }
}
