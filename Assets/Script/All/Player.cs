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
	protected bool readyToJump;
    public Item itemOnHand;
    public GameObject nowStage;
    protected bool controlable;

    protected float animSpeedFront;
    protected float animSpeedBack;

    new void Start()
    {
        base.Start();
        itemOnHand = null;
        jumping = false;
		readyToJump = false;
        animSpeedFront = front.GetComponent<Animator> ().speed;
        animSpeedBack = back.GetComponent<Animator> ().speed;
        nowStage = GameObject.Find("Stage1_1");
    }

    public void setControlable(bool controlable)
    {
        this.controlable = controlable;
    }

    void Update()
    {
        if (!isFreezed) {
            if (!rb)
                return;
            Vector2 move = rb.velocity;

            if (controlable) {
                axisX = Input.GetAxis ("Horizontal");
                axisY = Input.GetAxis ("Vertical");

                if (Input.GetKeyDown (KeyCode.UpArrow)) {
                    Collider2D[] hits = overlapAreaAll ();
                    foreach (Collider2D hit in hits) {
                        StageEnter stageEnter = hit.gameObject.GetComponent<StageEnter> ();
                        if (stageEnter && stageEnter.canEnter [(face) ? 0 : 1]) {
                            hit.gameObject.GetComponent<StageEnter> ().enter (gameObject);
                            return;
                        }
					}
					if (onFloor && !jumping) {
						jumping = true;
						front.GetComponent<Animator> ().SetBool ("jumping", true);
						back.GetComponent<Animator> ().SetBool ("jumping", true);
						StartCoroutine (jump ());
					}
                }

                if (Input.GetKeyDown (KeyCode.X)) {
                    if (itemOnHand) {
                        itemOnHand.drop (gameObject);
                    } else {
                        //find item nearby
                        Collider2D[] hits = overlapAreaAll ();
                        foreach (Collider2D hit in hits) {
                            Item item = hit.gameObject.GetComponent<Item> ();
                            if (item != null && item.isPickable ()) {
                                item.pick (gameObject);
                                break;
                            }
                        }
                    }
                }
                if (Input.GetKeyDown (KeyCode.C)) {
                    if (itemOnHand) {
                        bool disappear = itemOnHand.use (gameObject);
                        if (disappear) {
                            dropItem ();
                        }
                    } else {
                        //find machine nearby
                        Collider2D[] hits = overlapAreaAll ();
                        foreach (Collider2D hit in hits) {
                            Machine machine = hit.gameObject.GetComponent<Machine> ();
                            if (machine != null) {
                                machine.use (gameObject);
                                break;
                            }
                        }
                    }
                }
            } else {
                axisX = 0;
            }
            if (axisX > 0) {
                move.x = walkSpeed;
                front.GetComponent<SpriteRenderer> ().flipX = false ^ (!face);
                back.GetComponent<SpriteRenderer> ().flipX = false ^ (!face);
            } else if (axisX < 0) {
                //flip also depends on front/back face
                move.x = -walkSpeed;
                front.GetComponent<SpriteRenderer> ().flipX = true ^ (!face);
                back.GetComponent<SpriteRenderer> ().flipX = true ^ (!face);
            } else {
                move.x = 0;
            }

			if (readyToJump) {
				controlable = true;
				readyToJump = false;
				move.y = jumpSpeed;
			}

            if (!onFloor && !jumping && move.y > 0) {
                move.y = 0;
            }
            rb.velocity = move;

            if (move.x != 0) {
                front.GetComponent<Animator> ().SetBool ("walking", true);
                back.GetComponent<Animator> ().SetBool ("walking", true);
            } else {
                front.GetComponent<Animator> ().SetBool ("walking", false);
                back.GetComponent<Animator> ().SetBool ("walking", false);
            }

            front.GetComponent<Animator> ().speed = animSpeedFront;
            back.GetComponent<Animator> ().speed = animSpeedBack;
        }
        else {
            if (front.GetComponent<Animator> ().speed != 0) {
                animSpeedFront = front.GetComponent<Animator> ().speed;
                animSpeedBack = back.GetComponent<Animator> ().speed;
                front.GetComponent<Animator> ().speed = 0;
                back.GetComponent<Animator> ().speed = 0;
            }
        }
    }

    public void dropItem()
    {
        itemOnHand = null;

        front.GetComponent<Animator> ().Play ("empty_idle");
        back.GetComponent<Animator> ().Play ("empty_idle");
    }

    public void pickItem(Item item) {
        itemOnHand = item;

        string tmpName = item.name;
        if (tmpName.IndexOf ("(Clone)") != -1) {
            tmpName = tmpName.Substring (0, tmpName.IndexOf ("(Clone)"));
        }
        front.GetComponent<Animator> ().Play (tmpName + "_idle");
        back.GetComponent<Animator> ().Play (tmpName + "_idle");
    }

    protected override void main()
    {
        
    }

	protected virtual IEnumerator jump(){
		controlable = false;
		yield return new WaitForSeconds (0.3f);
		readyToJump = true;
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag ("Floor")) {   
            onFloor = true;
			jumping = false;
			front.GetComponent<Animator> ().SetBool ("jumping", false);
			back.GetComponent<Animator> ().SetBool ("jumping", false);
        }
        else if (collision.gameObject.CompareTag ("Plane")) {
			onFloor = true;
			front.GetComponent<Animator> ().SetBool ("jumping", false);
			back.GetComponent<Animator> ().SetBool ("jumping", false);
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
