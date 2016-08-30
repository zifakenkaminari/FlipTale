using UnityEngine;
using System.Collections;

public class Player : Entity {
    public float walkSpeed;
    public float jumpSpeed;
    float axisX;
    float axisY;
    bool onFloor;
    public GameObject itemOnHand;
    public GameObject itemNearby;
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
                itemOnHand.GetComponent<Item>().drop(gameObject);
                itemOnHand = null;

            }
            else if (itemNearby)
            {
                itemOnHand = itemNearby;
                itemOnHand.GetComponent<Item>().pick(gameObject);
            }
        }

    }

    protected override void main()
    {
        Vector3 move = rigidbody.velocity;
        if (axisX > 0)
        {
            move.x = walkSpeed;
        }
        else if (axisX < 0)
        {
            move.x = -walkSpeed;
        }
        else
        {
            move.x = 0;
        }

        if (axisY > 0 && onFloor)
        {
            move.y = jumpSpeed;
        }
        rigidbody.velocity = move;
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
        GameObject item = collider.gameObject;
        if (item.GetComponent<Item>() && item.GetComponent<Item>().isPickable())
        {
            itemNearby = item;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        GameObject item = collider.gameObject;
        if (item == itemNearby)
        {
            itemNearby = null;
        }
    }
}
