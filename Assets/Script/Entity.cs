using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {
    public Sprite frontSprite;
    public Sprite backSprite;
    public bool face;           //not flipped at first
    public bool isFlipping;
    public bool isFreezed;
    float flipTime;
    public float flipPeriod;
    public Rigidbody2D rb;

    public Vector3 saveVelocity;

	protected virtual void Start () {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().sprite = frontSprite;
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

    public IEnumerator flip()
    {
        flipTime = Time.time;
        Vector3 scale;
        while (Time.time - flipTime < flipPeriod)
        {
            scale = transform.localScale;
            if (face)
            {
                scale.x = Mathf.Cos((Time.time - flipTime) / flipPeriod * Mathf.PI);
            }
            else
            {
                scale.x = -Mathf.Cos((Time.time - flipTime) / flipPeriod * Mathf.PI);
            }
            if (Time.time - flipTime > flipPeriod / 2)
            {
                if (!face && GetComponent<SpriteRenderer>().sprite == backSprite)
                {
                    GetComponent<SpriteRenderer>().sprite = frontSprite;
                }
                if (face && GetComponent<SpriteRenderer>().sprite == frontSprite)
                {
                    GetComponent<SpriteRenderer>().sprite = backSprite;
                }
            }
            transform.localScale = scale;
            yield return null;
        }
        scale = transform.localScale;
        if (face)
        {
            scale.x = -1;
        }
        else
        {
            scale.x = 1;
        }
        face = !face;
        transform.localScale = scale;
        yield return null;
    }

    protected void flipping()
    {
        Vector3 scale = transform.localScale;
        if (Time.time - flipTime < flipPeriod)
        {
            if (face)
            {
                scale.x = Mathf.Cos((Time.time - flipTime) / flipPeriod * Mathf.PI);
            }
            else
            {
                scale.x = -Mathf.Cos((Time.time - flipTime) / flipPeriod * Mathf.PI);
            }
            if (Time.time - flipTime > flipPeriod / 2)
            {
                if (!face && GetComponent<SpriteRenderer>().sprite == backSprite)
                {
                    GetComponent<SpriteRenderer>().sprite = frontSprite;
                }
                if (face && GetComponent<SpriteRenderer>().sprite == frontSprite)
                {
                    GetComponent<SpriteRenderer>().sprite = backSprite;
                }
            }
        }
        else
        {
            if (face)
            {
                scale.x = -1;
            }
            else
            {
                scale.x = 1;
            }
            face = !face;
            isFlipping = false;
        }

        transform.localScale = scale; 
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
}

