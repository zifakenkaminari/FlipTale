using UnityEngine;
using System.Collections;

public class Item : Entity {
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
            setAlpha(0);
        }
    }

    public virtual bool isPickable() {
        if (state == 0 && velocity != Vector2.zero) return false;
        return pickable;
    }

    new protected void FixedUpdate()
    {
        base.FixedUpdate();
    }


    public override IEnumerator flip()
    {
        if (state == 1)
        {
            if (flipType == 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = face ? -1 : 1;
                transform.localScale = scale;
            }
            if (face)
            {
                setFlipValue(0);
                face = false;
            }
            else
            {
                setFlipValue(1);
                face = true;
            }
            yield break;
        }
        else
        {
            yield return base.flip();
        }
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

    protected virtual void idle()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll((Vector2)transform.position - onFloorOffset, GetComponent<Collider2D>().bounds.size, 0f, -Vector2.up, velocity.magnitude * Time.fixedDeltaTime);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.CompareTag("Floor"))
            {
                transform.position = hit.centroid + onFloorOffset;
                velocity = Vector2.zero;
                return;
            }
        }
        transform.Translate(velocity * Time.fixedDeltaTime);
        velocity += Physics2D.gravity * Time.fixedDeltaTime;

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

        setAlpha(0);
        player.GetComponent<Player>().pickItem(this);
        state = 1;
    }

    public virtual void drop(GameObject player)
    {
        //TODO: droped by player
        transform.parent = player.transform.parent;
        if (flipType == 1) {
            Vector3 scale = transform.localScale;
            scale.x = 1;
            transform.localScale = scale;
        }
        setAlpha(1);
        player.GetComponent<Player>().dropItem();
        state = 0;
    }

    public virtual bool use(GameObject player)
    {
        //TODO: used by player
        pickable = false;
        state = 2;
        return false;
    }
}
