using UnityEngine;
using System.Collections;

public class Balloon : Entity {

	// Use this for initialization
	protected override void Start () {
        base.Start();
        StartCoroutine(fly());
	}

    protected IEnumerator fly() { 
        float b = 0.7f;
        Vector3 v = new Vector3(3f, 0f, 0f);
        Vector3 g = new Vector3(0f, 0f, 0f);
        while (true)
        {
            while(isFreezed)yield return null;
            if (face) 
                g.y = -6;
            else    
                g.y = 6;
            while (isFreezed) yield return null;
            transform.position += v * Time.deltaTime;
            v += g * Time.deltaTime;
            v -= b * new Vector3(0, v.y, 0) * Time.deltaTime;
            yield return null;
        }
    }

}
