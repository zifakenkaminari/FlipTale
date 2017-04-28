using UnityEngine;
using System.Collections;

public class Balloon : Entity {
	[SerializeField] protected AudioSource blowingSound;
	[SerializeField] protected AudioSource explodingSound;
	bool isExploding;
    float blowPeriod = 1f;

	// Use this for initialization
	protected override void Start () {
		base.Start();
		transform.localScale = Vector3.zero;
		isExploding = false;
        StartCoroutine(fly());
	}


    protected IEnumerator fly() { 
        float timeNow = 0;
        while (timeNow < blowPeriod)
        {
            transform.localScale = new Vector3(1, 1, 1) * timeNow/blowPeriod;
            timeNow += Time.deltaTime;
			yield return new WaitWhile(() => isFreezed);
        }
        transform.localScale = new Vector3(1, 1, 1);
        rb.velocity = new Vector2(2.5f, 0);
		//while(front.GetComponent<SpriteRenderer>().isVisible){
		while(!isExploding){
            if(face)
                rb.gravityScale = 4f/9.8f;
            else
                rb.gravityScale = -4f/9.8f;
			rb.AddForce(new Vector3(0, rb.velocity.y, 0)*-0.7f*rb.mass);
			yield return new WaitWhile(() => isFreezed);
        }
		rb.velocity = Vector2.zero;
		rb.gravityScale = 0;
		front.GetComponent<Animator> ().SetBool ("exploded", true);
		back.GetComponent<Animator> ().SetBool ("exploded", true);
		explodingSound.Play ();
		yield return new WaitUntil (() => 
			front.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Explode") &&
			front.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f
		);
		Destroy (gameObject);
    }

	public void explode(){
		isExploding = true;
	}
}
