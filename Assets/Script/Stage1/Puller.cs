using UnityEngine;
using System.Collections;

public class Puller : Machine {
    public GameObject totem;
    public Sprite frontPuller;
    public Sprite backPuller;
    public int state;
    bool isRotating;
    public float rotatePeriod;

    protected override void Start()
    {
        base.Start();
        isRotating = false;
        state = 5;
	}

    public override void use(GameObject player) { 
        //turn 90 degree
        if (state == 5) 
            return;
        else if (!isRotating && face)
        {
            state = (state + 1) % 4;
            isRotating = true;
            StartCoroutine(rotate());
        }
    }

    public void pulled() {
        state = 0;
        front.GetComponent<SpriteRenderer>().sprite = frontPuller;
        back.GetComponent<SpriteRenderer>().sprite = backPuller;
    }

    protected IEnumerator rotate() {
        float timeNow = 0;
        Vector3 euler = totem.transform.localEulerAngles;
        float angle = euler.z;
        while(timeNow<rotatePeriod){
            while (isFreezed) yield return null; 
            euler.z = angle + 90 * timeNow / rotatePeriod;
            totem.transform.localEulerAngles = euler;
            timeNow += Time.deltaTime;
            yield return null;
        }
        euler.z = angle + 90;
        totem.transform.localEulerAngles = euler;
        isRotating = false;
    }



}
