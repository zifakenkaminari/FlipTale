using UnityEngine;
using System.Collections;

public class Player : Entity {
    public float walkSpeed;
    public float jumpSpeed;
    float axisX;
    float axisY;
    bool onFloor;
    public Item itemOnHand;
    public Item itemNearby;
    public GameObject nowStage;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        itemOnHand = null;
        itemNearby = null;
        //nowStage = GameObject.Find("Stage1");
    }

    void Update()
    {
        axisX = Input.GetAxis("Horizontal");
        axisY = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (itemOnHand)
            {
                itemOnHand.drop(gameObject);
                itemOnHand = null;

            }
            else if (itemNearby && itemNearby.isPickable())
            {
                itemOnHand = itemNearby;
                itemOnHand.pick(gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (itemOnHand)
            {
                itemOnHand.use(gameObject);
            }
        }
    }

    protected override void main()
    {
        Vector2 move = GetComponent<Rigidbody2D>().velocity;
        if (axisX > 0)
        {
            move.x = walkSpeed;
			GetComponent<SpriteRenderer> ().flipX = false ^ (!face);
        }
        else if (axisX < 0)
        {
            move.x = -walkSpeed;
			//flip also depends on front/back face
			GetComponent<SpriteRenderer> ().flipX = true ^ (!face);
        }
        else
        {
            move.x = 0;
        }

        if (axisY > 0 && onFloor)
        {
            move.y = jumpSpeed;
        }
        GetComponent<Rigidbody2D>().velocity = move;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            onFloor = true;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            onFloor = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            onFloor = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Item item = collider.gameObject.GetComponent<Item>();
        if (item)
        {
            itemNearby = item;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        Item item = collider.gameObject.GetComponent<Item>();
        if (item == itemNearby)
        {
            itemNearby = null;
        }
    }
}
