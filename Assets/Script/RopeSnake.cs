﻿using UnityEngine;
using System.Collections;

public class RopeSnake : Item
{
    public override bool isPickable()
    {
        if (!face)
            return false;
        else
            return base.isPickable();
    }

    protected override void held()
    {
        if (!face && state==1)
        {
            GameObject player = transform.parent.gameObject;
            player.GetComponent<Player>().dropItem();
            drop(player);
        }
    }

    public override bool use(GameObject player)
    {
        if (face)
        {
            Collider2D[] hits = Physics2D.OverlapAreaAll(colliderTopLeft(), colliderBotRight());
            foreach (Collider2D hit in hits)
            {
                if (hit.gameObject.GetComponent<Puller>())
                {
                    hit.gameObject.GetComponent<Puller>().pulled();
                    Destroy(gameObject);
                    return true;
                }
            }
        }
        return false;
    }

}
