using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {
    public Sprite frontSprite;
    public Sprite backSprite;
    public bool face;           //not flipped at first
    public bool isFlipping;
    public float flipTime;
    public float flipPeriod;
    new protected Rigidbody2D rigidbody;

    public Vector3 saveVelocity;

	protected void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        face = true;
        isFlipping = false;
        flipTime = -10000f;
	}
	
	protected void FixedUpdate () {
        if (isFlipping)
        {
            flipping();
        }
        else {
            main();
        }
	}

    public void flip() {
        if (isFlipping) return;
        isFlipping = true;
        flipTime = Time.time;
        saveVelocity = rigidbody.velocity;
        rigidbody.isKinematic = true;
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
            face = !face;
            isFlipping = false;
            rigidbody.isKinematic = false;
            rigidbody.velocity = saveVelocity;
        }

        transform.localScale = scale; 
    }

    protected virtual void main() { 
        //TODO: add main
    }
}

