using UnityEngine;
using System.Collections;

public class Spike : Entity {
	
    void OnTriggerStay2D(Collider2D collider) {
        if (!isFreezed && collider.gameObject.GetComponent<Balloon>()) {
			collider.gameObject.GetComponent<Balloon>().explode ();
        }
    }

    public IEnumerator disappear()
    {
        float destroyPeriod = 0.2f;
        float timeNow = 0;
        while (timeNow < destroyPeriod)
        {
            float rate = Mathf.Sin(timeNow / destroyPeriod*Mathf.PI/2);
            setAlpha(1 - rate);
			timeNow += Time.deltaTime;
			yield return new WaitWhile(() => isFreezed);
        }
        Destroy(gameObject);
    }

}
