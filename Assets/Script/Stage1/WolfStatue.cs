using UnityEngine;
using System.Collections;

public class WolfStatue : Entity {
    bool isRunning;

    protected override void Start() {
        base.Start();
        isRunning = false;
    }

    protected override void main()
    {
        if (!isRunning)
        {
            Vector2 a = (Vector2)transform.position + new Vector2(5, 7.23f);
            Vector2 b = (Vector2)transform.position + new Vector2(10, 0);
            Collider2D[] hits = Physics2D.OverlapAreaAll(a, b);
            foreach (Collider2D hit in hits)
            {
                if (hit.gameObject.GetComponent<Paper>())
                {
                    if (hit.gameObject.GetComponent<Paper>().getPaperState() == 3 && face)
                    {
                        StartCoroutine(run());
                    }
                    break;
                }
            }
        }

    }

    protected IEnumerator run() {
        if (isRunning) yield break;
		front.GetComponent<SpriteRenderer> ().flipX = true;
		back.GetComponent<SpriteRenderer> ().flipX = true;
        GameObject trashBag = GameObject.Find("TrashBag");
        trashBag.GetComponent<Item>().pickable = true;
        isRunning = true;
        rb.isKinematic = false;
		Collider2D moonMoonBound = GameObject.Find("StageBoundary_5b").GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), moonMoonBound);
        while (front.GetComponent<SpriteRenderer>().isVisible)
        {
            while (isFreezed) yield return null;
            rb.velocity += new Vector2(9, 0) * Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
