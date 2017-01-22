using UnityEngine;
using System.Collections;

public class Player : Entity {
    public Sprite frontNormal;
    public Sprite backNormal;
    public float walkSpeed;
    public float jumpSpeed;
    protected float axisX;
    protected float axisY;
    protected bool onFloor;
    [SerializeField]
    protected bool jumping;
    public Item itemOnHand;
    public GameObject nowStage;
    protected bool controlable;

    new void Start()
    {
        base.Start();
        controlable = true;
        itemOnHand = null;
        jumping = false;
        nowStage = GameObject.Find("Stage1_1");
    }

    public void setControlable(bool controlable)
    {
        this.controlable = controlable;
    }

    void Update()
    {
        if (!isFreezed)
        {
            if (!rb) return;
            Vector2 move = rb.velocity;
            if (controlable)
            {
                axisX = Input.GetAxis("Horizontal");
                axisY = Input.GetAxis("Vertical");

                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    Collider2D[] hits = overlapAreaAll();
                    foreach (Collider2D hit in hits)
                    {
                        StageEnter stageEnter = hit.gameObject.GetComponent<StageEnter>();
                        if (stageEnter && stageEnter.canEnter[(face) ? 0 : 1])
                        {
                            hit.gameObject.GetComponent<StageEnter>().enter(gameObject);
                            axisY = 0;
                            break;
                        }
                    }
                }
                else
                {
                    axisY = 0;
                }
                if (Input.GetKeyDown(KeyCode.X))
                {
                    if (itemOnHand)
                    {
                        itemOnHand.drop(gameObject);
                    }
                    else
                    {
                        //find item nearby
                        Collider2D[] hits = overlapAreaAll();
                        foreach (Collider2D hit in hits)
                        {
                            Item item = hit.gameObject.GetComponent<Item>();
                            if (item != null && item.isPickable())
                            {
                                item.pick(gameObject);
                                break;
                            }
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.C))
                {
                    if (itemOnHand)
                    {
                        bool disappear = itemOnHand.use(gameObject);
                        if (disappear)
                        {
                            dropItem();
                        }
                    }
                    else
                    {
                        //find machine nearby
                        Collider2D[] hits = overlapAreaAll();
                        foreach (Collider2D hit in hits)
                        {
                            Machine machine = hit.gameObject.GetComponent<Machine>();
                            if (machine != null)
                            {
                                machine.use(gameObject);
                                break;
                            }
                        }
                    }
                }
            }
            else {
                axisX = 0;
                axisY = 0;
            }
            if (axisX > 0)
            {
                move.x = walkSpeed;
                front.GetComponent<SpriteRenderer>().flipX = false ^ (!face);
                back.GetComponent<SpriteRenderer>().flipX = false ^ (!face);
            }
            else if (axisX < 0)
            {
                //flip also depends on front/back face
                move.x = -walkSpeed;
                front.GetComponent<SpriteRenderer>().flipX = true ^ (!face);
                back.GetComponent<SpriteRenderer>().flipX = true ^ (!face);
            }
            else
            {
                move.x = 0;
            }

            if (axisY > 0 && onFloor)
            {
                move.y = jumpSpeed;
                jumping = true;
            }

            if (!onFloor && !jumping && move.y > 0) {
                move.y = 0;
            }
            rb.velocity = move;
        }
    }

    public void dropItem()
    {
        itemOnHand = null;
        front.GetComponent<SpriteRenderer>().sprite = frontNormal;
        back.GetComponent<SpriteRenderer>().sprite = backNormal;
    }

    public void pickItem(Item item) {
        itemOnHand = item;
        front.GetComponent<SpriteRenderer>().sprite = item.frontOnHand;
        back.GetComponent<SpriteRenderer>().sprite = item.backOnHand;
    }

    protected override void main()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag ("Floor")) {   
            onFloor = true;
            jumping = false;
        }
        else if (collision.gameObject.CompareTag ("Plane")) {
            onFloor = true;
            collision.gameObject.tag = "Floor";
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

    public virtual void teleport(Vector3 position) {
        transform.position = position;
        if (itemOnHand != null)
            itemOnHand.transform.position = position;
    }

}
