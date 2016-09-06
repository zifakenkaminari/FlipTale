using UnityEngine;
using System.Collections;

public class FlowerTorch : Item
{
    GameObject cave;

    protected override void Start()
    {
        base.Start();
        state = 3;
    }



    public override bool use(GameObject player)
    {
        if (cave && !face)
        {
            cave.GetComponent<Cave>().burn();
            Destroy(gameObject);
            return true;
        }
        return false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Cave"))
        {
            cave = collider.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (cave && collider.gameObject == cave)
        {
            cave = null;
        }
    }
}
