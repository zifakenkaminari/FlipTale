using UnityEngine;
using System.Collections;

public class WolfStatue : Entity {
	bool isRunning;
	bool isLooking;

    protected override void Start() {
        base.Start();
		isRunning = false;
		isLooking = false;
    }


	public override IEnumerator flip(){
		if (face) {
			front.GetComponent<Animator> ().speed = 0;
			back.GetComponent<Animator> ().speed = 0;
		}
		yield return base.flip ();
		if(face){
			front.GetComponent<Animator> ().speed = 1;
			back.GetComponent<Animator> ().speed = 1;
		}
	}

    protected override void main()
    {
		if (!face)
			return;
		if (!isRunning && !isLooking)
        {
            Vector2 a = (Vector2)transform.position + new Vector2(-2, 10f);
            Vector2 b = (Vector2)transform.position + new Vector2(2, 0);
            Collider2D[] hits = Physics2D.OverlapAreaAll(a, b);
            foreach (Collider2D hit in hits)
			{
				Paper paper = hit.gameObject.GetComponent<Paper> ();
				if (paper != null)
                {
					if (paper.hasUsed ())
						continue;
					int state = paper.getPaperState();
                    if (state == 4 && !isLooking)
					{
						isLooking = true;
						paper.setUsed (true);
                        StartCoroutine(look());
					}
					if (state == 3 && !isRunning)
					{
						isRunning = true;
						paper.setUsed (true);
						StartCoroutine(run());
					}
                    break;
                }
            }
        }

    }
	protected IEnumerator look() {
		front.GetComponent<Animator> ().SetBool ("seeCrumble", true);
		back.GetComponent<Animator> ().SetBool ("seeCrumble", true);
		yield return new WaitUntil (()=>
			front.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f &&
			front.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Look")

		);
		front.GetComponent<Animator> ().SetBool ("seeCrumble", false);
		back.GetComponent<Animator> ().SetBool ("seeCrumble", false);
		isLooking = false;
	}

    protected IEnumerator run() {
		yield return new WaitForSeconds (0.2f);
		front.GetComponent<SpriteRenderer> ().flipX = true;
		back.GetComponent<SpriteRenderer> ().flipX = true;
		front.GetComponent<Animator> ().SetBool ("seePlane", true);
		back.GetComponent<Animator> ().SetBool ("seePlane", true);
        GameObject trashBag = GameObject.Find("TrashBag");
        trashBag.GetComponent<Item>().pickable = true;
        rb.isKinematic = false;
		Collider2D moonMoonBound = GameObject.Find("StageBoundary_5b").GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), moonMoonBound);
        while (front.GetComponent<SpriteRenderer>().isVisible)
        {
			if (face)
				rb.AddForce (new Vector2 (9, 0) * rb.mass);
			else
				rb.velocity = Vector2.zero;
			yield return new WaitWhile(() => isFreezed);
        }
        Destroy(gameObject);
    }


}
