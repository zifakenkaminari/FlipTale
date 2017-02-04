using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {
    public int flipType;
    [HideInInspector]
    public GameObject front;
    [HideInInspector]
    public GameObject back;
    public bool face;           //not flipped at first
    protected bool isFlipping;
    protected bool isFreezed;
    protected float flipTime;
    protected float flipPeriod = 1;
    protected float alpha;
    protected float flipValue;

    protected Rigidbody2D rb;
    protected Vector3 saveVelocity;
    protected bool saveKinematic;

    protected virtual void Start () {
        front = transform.FindChild ("front").gameObject;
        back = transform.FindChild ("back").gameObject;

        rb = GetComponent<Rigidbody2D>();
        isFlipping = false;
        isFreezed = false;
        flipTime = -10000f;
        //if has an Entity or Stage parent, set flip side to the same as its parent
        //if is spawn by parent, set flip side to the same as its parent
        if (transform.parent && transform.parent.gameObject.GetComponent<Entity> ())
            face = transform.parent.gameObject.GetComponent<Entity> ().face;
        else if (transform.parent && transform.parent.gameObject.GetComponent<Stage> ()) 
            face = transform.parent.gameObject.GetComponent<Stage> ().face;
        else if (GetComponent<FixedJoint2D>())
            face = GetComponent<FixedJoint2D>().connectedBody.GetComponent<Entity>().face;
        else 
            face = true;
        setFlipValue(face?1:0);
        setAlpha(1);

    }

    protected void FixedUpdate () {
        if (!isFreezed) { 
            main();
        }
    }

    public virtual IEnumerator flip()
    {
        if (isFlipping)
            yield break;
        isFlipping = true;
        flipTime = Time.time;

        if(face){
            while (Time.time - flipTime < flipPeriod)
            {
                setFlipValue(1 - (Time.time - flipTime) / flipPeriod);
                yield return null;
            }
            setFlipValue(0);
            face = false;
        }
        else
        {
            while (Time.time - flipTime < flipPeriod)
            {
                setFlipValue((Time.time - flipTime) / flipPeriod);
                yield return null;
            }
            setFlipValue(1);
            face = true;
        }
        isFlipping = false;
    }

    protected virtual void main() { 
        //TODO: add main
    }

    public void lockMotion()
    {
        if (rb)
        {
            saveKinematic = rb.isKinematic;
            saveVelocity = rb.velocity;
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }
        isFreezed = true;
    }
    public void unlockMotion()
    {
        if (rb)
        {
            rb.isKinematic = saveKinematic;
            rb.velocity = saveVelocity;
        }
        isFreezed = false;
    }

    protected virtual void setTransparent(ref GameObject bg, float a) {
        Color tmpColor = bg.GetComponent<SpriteRenderer> ().color;
        tmpColor.a = a;
        bg.GetComponent<SpriteRenderer> ().color = tmpColor;
        return;
    }

    public void setAlpha(float alpha) {
        this.alpha = alpha;
        if (flipType == 0)
        {
            if (flipValue > 0.5)
            {
                setTransparent(ref front, alpha);
                setTransparent(ref back, 0);
            }
            else
            {
                setTransparent(ref front, 0);
                setTransparent(ref back, alpha);
            }
        }
        else if (flipType == 1)
        {
            float frontAlpha = flipValue * alpha;
            float backAlpha = alpha * (1 - flipValue) / (1 - alpha * flipValue);
            setTransparent(ref front, frontAlpha);
            setTransparent(ref back, backAlpha);
        }
    }

    public void setFlipValue(float f)
    {
        //flip value: front = 1, back = 0
        flipValue = f;
        if (flipType == 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Cos((1-f)*Mathf.PI);
            transform.localScale = scale;
            if(f>0.5){
                setTransparent(ref front, alpha);
                setTransparent(ref back, 0);
            }
            else{
                setTransparent(ref front, 0);
                setTransparent(ref back, alpha);
            }
        }
        else if (flipType == 1)
        {
            float frontAlpha = f * alpha;
            float backAlpha = alpha * (1 - f) / (1 - alpha * f);
            setTransparent(ref front, frontAlpha);
            setTransparent(ref back, backAlpha);
        }
    }

    public Collider2D[] overlapAreaAll() {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        if (collider == null) return null;
        Vector2 topLeft = (Vector2)transform.position + collider.offset - new Vector2(collider.size.x / 2, collider.size.y / 2);
        Vector2 botRight = (Vector2)transform.position + collider.offset + new Vector2(collider.size.x / 2, collider.size.y / 2);
        return Physics2D.OverlapAreaAll(topLeft, botRight);
    }

	public void setSprite(Sprite frontSprite, Sprite backSprite){
		front.GetComponent<SpriteRenderer> ().sprite = frontSprite;
		back.GetComponent<SpriteRenderer> ().sprite = backSprite;
	}

}

