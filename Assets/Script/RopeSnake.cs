using UnityEngine;
using System.Collections;

public class RopeSnake : Item
{
    public GameObject puller;
    public GameObject player;

    protected new void Start()
    {
        base.Start();
        puller = null;
    }


    public override void pick(GameObject player) {
        this.player = player;
        base.pick(player);
    }

    public override bool isPickable()
    {
        if (!face)
            return false;
        else
            return base.isPickable();
    }

    public override IEnumerator flip()
    {
        yield return base.flip();
        if (!face) {
            if (player)
            {
                drop(player);
                Debug.Log("Drop");
                player.GetComponent<Player>().itemOnHand = null;
            }
        }
    }

    public override bool use(GameObject player)
    {
        if (puller && face)
        {
            puller.GetComponent<Puller>().state = 0;
            puller.GetComponent<Puller>().pulled();
            Destroy(gameObject);
            return true;
        }
        return false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Puller") && collider.gameObject.GetComponent<Puller>().state==5)
        {
            puller = collider.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Puller"))
        {
            puller = null;
        }
    }
}
