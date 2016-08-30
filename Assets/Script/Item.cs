using UnityEngine;
using System.Collections;

public class Item : Entity {
    public string name;
    public bool pickable;
    int state;

    protected void Start()
    {
        base.Start();
    }

    public bool isPickable() {
        return pickable;
    }

    protected void FixedUpdate()
    {

        base.FixedUpdate();
    }

    protected override void main(){
        switch(state){
            case 0:
                idle();
                break;
            case 1:
                held();
                break;
            case 2:
                used();
                break;
        }
    }

    protected void idle()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, new Vector2(0, -1));
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.CompareTag("Floor"))
            {
                transform.position = Vector2.Lerp(transform.position, hit.point + new Vector2(0, 0.5f), 0.2f);
            }
        }
    }
    protected virtual void held()
    {
        //holding by player
    }

    protected virtual void used()
    {
        //after used
    }

    public virtual void pick(GameObject player)
    {
        //TODO: picked by player
        transform.SetParent(player.transform);
        GetComponent<SpriteRenderer>().enabled = false;
        state = 1;
    }

    public virtual void drop(GameObject player)
    {
        //TODO: droped by player
        transform.parent = null;
        GetComponent<SpriteRenderer>().enabled = true;
        state = 0;
    }

    public virtual void use(GameObject player)
    {
        //TODO: used by player
        GetComponent<SpriteRenderer>().enabled = true;
        pickable = false;
        state = 2;
    }
}
