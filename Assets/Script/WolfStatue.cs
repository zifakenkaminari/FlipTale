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
            Vector2 a = (Vector2)transform.position + new Vector2(5, 20);
            Vector2 b = (Vector2)transform.position + new Vector2(10, -20);
            Collider2D[] hits = Physics2D.OverlapAreaAll(a, b);
            foreach (Collider2D hit in hits)
            {
                if (hit.gameObject.CompareTag("Paper"))
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
        isRunning = true;
        rb.isKinematic = false;
        while (front.GetComponent<SpriteRenderer>().isVisible)
        {
            while (isFreezed) yield return null;
            rb.velocity += new Vector2(9, 0) * Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }



    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("TrashBag"))
        {
            collider.gameObject.GetComponent<TrashBag>().pickable = false;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("TrashBag"))
        {
            collider.gameObject.GetComponent<TrashBag>().pickable = true;
        }
    }
}
