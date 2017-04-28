using UnityEngine;
using System.Collections;

public class BalloonBlower : Machine {
    public GameObject balloon;
	public GameObject top;

    protected override void Start(){
		base.Start ();
		usable = true;
	}

    public override void use(GameObject player)
    {
		if (usable) {
			GameObject newBalloon = (GameObject)Instantiate (balloon, transform);
			newBalloon.transform.localPosition = new Vector3 (0, 0.7f, 0);
			usable = false;
			StartCoroutine (blow ());
		}
    }

	protected IEnumerator blow(){
		Vector3 pos = top.transform.position;
		float move = 0.3f;
		float pushPeriod = 1f;
		float recoverPeriod = 2f;
		float time = 0;
		while(time < pushPeriod){
			top.transform.position = pos + Vector3.down * move * time / pushPeriod;
			time += Time.deltaTime;
			yield return new WaitWhile(()=>isFreezed);
		}
		time = 0;
		while(time < recoverPeriod){
			top.transform.position = pos + Vector3.down * move * (1- time / recoverPeriod);
			time += Time.deltaTime;
			yield return new WaitWhile(()=>isFreezed);
		}
		top.transform.position = pos;
		usable = true;
	}

}
