using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {
    public int flipType;
    public GameObject front;
    public GameObject back;
    public bool face;           //not flipped at first
    public bool isFlipping;
    public bool isFreezed;
    float flipTime;
    public float flipPeriod;
    protected Rigidbody2D rb;

    protected Vector3 saveVelocity;
    protected bool saveKinematic;

    protected virtual void Start () {
        rb = GetComponent<Rigidbody2D>();
        isFlipping = false;
        isFreezed = false;
        flipTime = -10000f;
        if (transform.parent && transform.parent.gameObject.GetComponent<Entity>())
        {
            face = transform.parent.gameObject.GetComponent<Entity>().face;
        }
        else {
            face = true;
        }
    
        if (face)
        {
            setTransparent(ref back, 0);
        }
        else
        {
            setTransparent(ref front, 0);
        }
        if(flipType==1)setTransparent(ref back, 1);
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
        Vector3 scale;
        if (flipType == 0) {
            while (Time.time - flipTime < flipPeriod) {
                scale = transform.localScale;
                if (face) {
                    scale.x = Mathf.Cos ((Time.time - flipTime) / flipPeriod * Mathf.PI);
                } else {
                    scale.x = -Mathf.Cos ((Time.time - flipTime) / flipPeriod * Mathf.PI);
                }
                if (Time.time - flipTime > flipPeriod / 2) {
                
                    if (!face && back.GetComponent<SpriteRenderer> ().color.a == 1) {
                        setTransparent (ref front, 1);
                        setTransparent (ref back, 0);
                    }
                    if (face && front.GetComponent<SpriteRenderer> ().color.a == 1) {
                        setTransparent (ref front, 0);
                        setTransparent (ref back, 1);
                    }

                }
                transform.localScale = scale;
                yield return null;
            }
            scale = transform.localScale;
            if (face) {
                scale.x = -1;
            } else {
                scale.x = 1;
            }
            face = !face;
            transform.localScale = scale;
        }
        else if (flipType == 1) {
            while (Time.time - flipTime < flipPeriod)
            {
                if (face)
                {
                    // -- change background slow to fast --
                    setTransparent(ref front, Mathf.Cos((Time.time - flipTime) / flipPeriod * Mathf.PI / 2) );
                    //setTransparent(ref back, 1 - Mathf.Cos((Time.time - flipTime) / flipPeriod * Mathf.PI / 2) );

                    // -- uniformly change background --
                    //setTransparent(ref bgFront, 1 - (Time.time - flipTime) / flipPeriod);
                    //setTransparent(ref bgBack, (Time.time - flipTime) / flipPeriod);
                }
                else
                {
                    // -- change background slow to fast --
                    setTransparent(ref front, 1 - Mathf.Cos((Time.time - flipTime) / flipPeriod * Mathf.PI / 2) );
                    //setTransparent(ref back, Mathf.Cos((Time.time - flipTime) / flipPeriod * Mathf.PI / 2) );

                    // -- uniformly change background --
                    //setTransparent(ref bgFront, (Time.time - flipTime) / flipPeriod);
                    //setTransparent(ref bgBack, 1 - (Time.time - flipTime) / flipPeriod);
                }
                yield return null;
            }
            face = !face;
        }
        isFlipping = false;
        yield return null;
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

    protected void setTransparent(ref GameObject bg, float a) {
        Color tmpColor = bg.GetComponent<SpriteRenderer> ().color;
        tmpColor.a = a;
        bg.GetComponent<SpriteRenderer> ().color = tmpColor;
        return;
    }

    public Collider2D[] overlapAreaAll() {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        if (collider == null) return null;
        Vector2 topLeft = (Vector2)transform.position + collider.offset - new Vector2(collider.size.x / 2, collider.size.y / 2);
        Vector2 botRight = (Vector2)transform.position + collider.offset + new Vector2(collider.size.x / 2, collider.size.y / 2);
        return Physics2D.OverlapAreaAll(topLeft, botRight);
    }

}

