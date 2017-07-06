using UnityEngine;
using System.Collections;

public class Paper : Item {
	[SerializeField] protected AudioSource makeCrumbleSound;
	[SerializeField] protected AudioSource makePlaneSound;
    public Sprite paperCrumpled;
    public Sprite paperPlane;
    public float destroyPeriod;
    protected int paperState; //0: normal, 1: crumpled, 2: plane
	protected bool usedFlag; 
    protected bool isDisappearing;

    protected new void Start()
    {
        base.Start();
        paperState = 0; // normal
        usedFlag = false;
        isDisappearing = false;
    }

	public bool hasUsed(){
        return usedFlag;
	}

	public void setUsed(bool used){
        this.usedFlag = used;
	}

    public void magic() {
        paperState = 2;
        front.GetComponent<SpriteRenderer> ().sprite = paperPlane;
        back.GetComponent<SpriteRenderer> ().sprite = paperPlane;
    }


    public override void drop(GameObject player)
    {
        base.drop(player);
        pickable = false;
    }

    public IEnumerator disappear() {
        float timeNow = 0;
        while(timeNow < destroyPeriod){
            setAlpha(1 - timeNow / destroyPeriod);
			timeNow += Time.deltaTime;
			yield return new WaitWhile(() => isFreezed);
        }
        Destroy(gameObject);
    }

    public override bool use(GameObject player)
    {
        if (paperState == 0) {          
            //normal
			if(!face){
				Collider2D[] hits = overlapAreaAll();
				foreach (Collider2D hit in hits) {
					if (hit.gameObject.name == "MapDesign")
					{
						//become plane
						paperState = 2;     
						makePlaneSound.Play ();
						front.GetComponent<SpriteRenderer> ().sprite = paperPlane;
						back.GetComponent<SpriteRenderer> ().sprite = paperPlane;
                        name = "PaperPlane";
						player.GetComponent<Player>().pickItem(this);
						base.use (player);

						return false;
					}
				}
			}
			//become trash
			paperState = 1;
			makeCrumbleSound.Play ();
			front.GetComponent<SpriteRenderer> ().sprite = paperCrumpled;
			back.GetComponent<SpriteRenderer> ().sprite = paperCrumpled;
            name = "PaperTrash";
			player.GetComponent<Player>().pickItem(this);
            base.use (player);

        }
        else if (paperState == 1)
        {
            //throw paper trash
            paperState = 4;
            drop(player);
            player.GetComponent<Player>().dropItem();
            Vector3 scale = transform.localScale;
            scale.x = (player.GetComponent<Player>().front.GetComponent<SpriteRenderer>().flipX ^ player.GetComponent<Player>().face) ? 1 : -1;
            transform.localScale = scale;
            StartCoroutine(thrown(player));
        }
        else if (paperState == 2)
        {          
            //plane
            paperState = 3;
            drop(player);
            player.GetComponent<Player>().dropItem();
            Vector3 scale = transform.localScale;
            scale.x = (player.GetComponent<Player>().front.GetComponent<SpriteRenderer>().flipX ^ player.GetComponent<Player>().face)?1:-1;
            transform.localScale = scale;
            GetComponent<BoxCollider2D> ().enabled = false;
            StartCoroutine(fly(player));
        }
        return false;
    }


    protected IEnumerator thrown(GameObject player)
    {
        Vector3 scale = transform.localScale;
        setAlpha(1);
        rb.velocity = new Vector2(5f * scale.x, 6f);
        rb.angularVelocity = scale.x;
        yield return null;

    }

    protected IEnumerator fly(GameObject player)
    {
        Vector3 scale = transform.localScale;
        setAlpha(1);
        rb.gravityScale = 0.5f;
        rb.velocity = new Vector2(7f * scale.x, 5f);
        
        Vector3 eular = transform.localEulerAngles;
        while (front.GetComponent<SpriteRenderer>().isVisible)
        {
            if (isDisappearing) {
                yield break;
            }
			if (!float.Equals (rb.velocity.x, 0f)) {
				eular.z = Mathf.Atan(rb.velocity.y/ rb.velocity.x)*Mathf.Rad2Deg - 15 * scale.x;
				transform.localEulerAngles = eular;
			}
			rb.AddForce(new Vector2(0, rb.velocity.y) * rb.mass * -1.5f);
			yield return new WaitWhile(() => isFreezed);
        }
        Destroy(gameObject);
        yield break;

    }

    public int getPaperState() {
        return paperState;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") && !isDisappearing)
        {
            isDisappearing = true;
            gameObject.GetComponent<Rigidbody2D> ().velocity.Set(0, 0);
            StartCoroutine(disappear());
        }
    }
}
