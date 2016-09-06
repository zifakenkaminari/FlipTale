using UnityEngine;
using System.Collections;

public class RopeSnake : Item
{
    public GameObject totem;
    public GameObject player;

    protected new void Start()
    {
        base.Start();
        totem = null;
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
        Debug.Log("End coroutine");
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
        if (totem && face)
        {
            totem.GetComponent<Totem>().state = 0;
            Destroy(gameObject);
            return true;
        }
        return false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Totem") && collider.gameObject.GetComponent<Totem>().state==5)
        {
            totem = collider.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Totem"))
        {
            totem = null;
        }
    }
}
