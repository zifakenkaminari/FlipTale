using UnityEngine;
using System.Collections;

public class CaveExit : Entity {
    public bool isOpen;
    float openPeriod = 2.1f;
    float shift = 5;

	protected override void Start () {
        isOpen = false;
	}


    public IEnumerator open() {
        if (isOpen) yield break;
        isOpen = true;
        Vector2 pos = transform.position;
        float y = transform.position.y;
        float beginTime = Time.time;
        while(Time.time-beginTime<openPeriod){
            pos.y = y + shift*(Time.time - beginTime) / openPeriod;
            transform.position = pos;
            yield return null;
        }
        pos.y = y + shift;
        transform.position = pos;
        
    }
}
