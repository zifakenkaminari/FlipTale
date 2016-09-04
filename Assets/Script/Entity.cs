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

    public Vector3 saveVelocity;

    protected virtual void Start () {
        rb = GetComponent<Rigidbody2D>();
        setTransparent (ref back, 0);
        face = true;
        isFlipping = false;
        isFreezed = false;
        flipTime = -10000f;
    }

    protected void FixedUpdate () {
        if (!isFreezed) { 
            main();
        }
    }

    public void quickFlip() {
        face = !face;
    }

    public virtual IEnumerator flip()
    {
        flipTime = Time.time;
        isFlipping = true;
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
                    setTransparent(ref back, 1 - Mathf.Cos((Time.time - flipTime) / flipPeriod * Mathf.PI / 2) );

                    // -- uniformly change background --
                    //setTransparent(ref bgFront, 1 - (Time.time - flipTime) / flipPeriod);
                    //setTransparent(ref bgBack, (Time.time - flipTime) / flipPeriod);
                }
                else
                {
                    // -- change background slow to fast --
                    setTransparent(ref front, 1 - Mathf.Cos((Time.time - flipTime) / flipPeriod * Mathf.PI / 2) );
                    setTransparent(ref back, Mathf.Cos((Time.time - flipTime) / flipPeriod * Mathf.PI / 2) );

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
            saveVelocity = rb.velocity;
            rb.isKinematic = true;
        }
        isFreezed = true;
    }
    public void unlockMotion()
    {
        if (rb)
        {
            rb.isKinematic = false;
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
}

