using UnityEngine;
using System.Collections;

public class CaveExit : Entity {
    public bool isOpen;
    float openPeriod = 2.1f;
    float shift = 3;

	protected override void Start () {
        base.Start();
        isOpen = false;
	}

    public IEnumerator open() {
        if (isOpen) yield break;
        isOpen = true;
        Vector2 pos = transform.position;
        float y = transform.position.y;
        float timeNow = 0;
        while(timeNow<openPeriod){
            while (isFreezed) yield return null;
            pos.y = y + shift * timeNow / openPeriod;
            transform.position = pos;
            timeNow += Time.deltaTime;
            yield return null;
        }
        pos.y = y + shift;
        transform.position = pos;
        
    }
}
