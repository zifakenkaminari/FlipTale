using UnityEngine;
using System.Collections;

public class Item : Entity {
    public Sprite frontOnHand;
    public Sprite backOnHand;
    public bool pickable;
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
            /*
            if (flipType == 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = face ? -1 : 1;
                transform.localScale = scale;
            }
            */
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
        gameObject.AddComponent<FixedJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
        GetComponent<Collider2D>().enabled = false;
        transform.position = player.transform.position;
        if (face != player.GetComponent<Entity>().face) {
            face = player.GetComponent<Entity>().face;
            setFlipValue(face ? 1 : 0); 
        }
        setAlpha(0);
        player.GetComponent<Player>().pickItem(this);
        state = 1;
    }

    public virtual void drop(GameObject player)
    {
        //TODO: droped by player
        /*
        transform.parent = player.transform.parent;
        if (flipType == 1) {
            Vector3 scale = transform.localScale;
            scale.x = 1;
            transform.localScale = scale;
        }
        */
        Destroy(GetComponent<FixedJoint2D>());
        GetComponent<Collider2D>().enabled = true;
        rb.velocity = Vector2.zero;
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

    public virtual void destroy(GameObject player) {
        drop (player);
        Destroy (gameObject);
    }
}
