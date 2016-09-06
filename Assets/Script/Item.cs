using UnityEngine;
using System.Collections;

public class Item : Entity {
    new public string name;
    public Sprite frontOnHand;
    public Sprite backOnHand;
    public bool pickable;
    public Vector2 onFloorOffset;
    protected int state;

    Vector2 velocity;

    new protected virtual void Start()
    {
        base.Start();
        velocity = Vector2.zero;
        if (state == 1)
        {
            setTransparent(ref front, 0);
            setTransparent(ref back, 0);
        }
    }

    public bool isPickable() {
        return pickable;
    }

    new protected void FixedUpdate()
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
        RaycastHit2D[] hits = Physics2D.RaycastAll((Vector2)transform.position - onFloorOffset, new Vector2(0, -1), velocity.magnitude*Time.deltaTime);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.CompareTag("Floor"))
            {

                transform.position = hit.point + onFloorOffset;
                velocity = Vector2.zero;
                pickable = true;
                return;
            }
        }

        transform.Translate(velocity * Time.fixedDeltaTime);
        velocity += Physics2D.gravity*Time.fixedDeltaTime;
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
        velocity = Vector2.zero;
        transform.SetParent(player.transform);
        transform.localPosition = Vector3.zero;
        setTransparent (ref front, 0);
        setTransparent (ref back, 0);
        state = 1;
    }

    public virtual void drop(GameObject player)
    {
        //TODO: droped by player
        pickable = false;
        transform.parent = player.transform.parent;
        if (face)
            setTransparent (ref front, 1);
        else
            setTransparent (ref back, 1);
        state = 0;
    }

    public virtual bool use(GameObject player)
    {
        //TODO: used by player
        //GetComponent<SpriteRenderer>().enabled = true;
        pickable = false;
        state = 2;
        return false;
    }
}
