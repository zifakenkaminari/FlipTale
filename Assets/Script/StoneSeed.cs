using UnityEngine;
using System.Collections;

public class StoneSeed : Item
{
    public float onPotOffsetY;
    public GameObject flowerTorch;
    public float destroyPeriod;
    GameObject pot;
    bool isNearPot;

	protected new void Start () {
        isNearPot = false;
	}

    public new void drop(GameObject player) {
        base.drop(player);
        pickable = false;
    }
	
	// Update is called once per frame
	void Update () {

    }

    public override void use(GameObject player)
    {
        if (isNearPot && !face)
        {
            GameObject newFlower = Instantiate(flowerTorch);
            Vector3 pos = pot.transform.position;
            pos.y = pot.transform.position.y + onPotOffsetY;
            newFlower.transform.position = pos;
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Pot"))
        {
            isNearPot = true;
            pot = collider.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject == pot)
        {
            isNearPot = false;
            pot = null;
        }
    }
}
