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
        float destroyPeriod = 0.2f;
        float timeNow = 0;
        while (timeNow < destroyPeriod)
        {
            while (isFreezed) yield return null;
            float rate = Mathf.Sin(timeNow / destroyPeriod*Mathf.PI/2);
            setAlpha(1 - rate);
            timeNow += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

}
