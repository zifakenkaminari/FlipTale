using UnityEngine;
using System.Collections;

public class Spike : Entity {
    void OnTriggerStay2D(Collider2D collider) {
        if (!isFreezed && collider.gameObject.GetComponent<Balloon>()) {
            Destroy(collider.gameObject);
        }
    }
    public IEnumerator disappear()
    {
        float destroyPeriod = 1f;
        float timeNow = 0;
        while (timeNow < destroyPeriod)
        {
            while (isFreezed) yield return null;
            float rate = Mathf.Sin(timeNow / destroyPeriod*Mathf.PI/2);
            Color color = front.GetComponent<Renderer>().material.color;
            color.a = 1 - rate;
            front.GetComponent<Renderer>().material.color = color;

            color = back.GetComponent<Renderer>().material.color;
            color.a = 1 - rate;
            back.GetComponent<Renderer>().material.color = color;
            timeNow += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

}
