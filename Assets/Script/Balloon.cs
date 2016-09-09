using UnityEngine;
using System.Collections;

public class Balloon : Entity {

    float blowPeriod = 1f;

	// Use this for initialization
	protected override void Start () {
        transform.localScale = Vector3.zero;
        base.Start();
        StartCoroutine(fly());
	}

    protected IEnumerator fly() { 
        float timeNow = 0;
        while (timeNow < blowPeriod)
        {
            while (isFreezed) yield return null;
            transform.localScale = new Vector3(1, 1, 1) * timeNow/blowPeriod;
            timeNow += Time.deltaTime;
            yield return null;
        }
        transform.localScale = new Vector3(1, 1, 1);

        float b = 0.7f;
        Vector3 v = new Vector3(2.5f, 0f, 0f);
        Vector3 g = new Vector3(0f, 0f, 0f);
        while (true)
        {
            while(isFreezed)yield return null;
            if (face) 
                g.y = -4;
            else    
                g.y = 4;
            while (isFreezed) yield return null;
            transform.position += v * Time.deltaTime;
            v += g * Time.deltaTime;
            v -= b * new Vector3(0, v.y, 0) * Time.deltaTime;
            yield return null;
        }
    }

}
