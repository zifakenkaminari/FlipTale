using UnityEngine;
using System.Collections;

public class Item : Entity {
    public string name;
    public bool pickable;
    public Vector2 onFloorOffset;
    int state;

    Vector2 velocity;

    void Start()
    {
        velocity = Vector2.zero;
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
        RaycastHit2D[] hits = Physics2D.RaycastAll((Vector2)transform.position - onFloorOffset, new Vector2(0, -1), velocity.magnitude*Time.deltaTime);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.CompareTag("Floor"))
            {
                transform.position = hit.point + onFloorOffset;
                velocity = Vector2.zero;
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
