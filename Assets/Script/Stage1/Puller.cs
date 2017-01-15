using UnityEngine;
using System.Collections;

public class Puller : Machine {
    public GameObject totem;
    public GameObject pullerMask;
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

        Vector3 eulerTotem = totem.transform.localEulerAngles;
        float angleTotem = eulerTotem.z;

        Vector3 eulerPullerMask = pullerMask.transform.localEulerAngles;
        float anglePullerMask = eulerPullerMask.z;

        while(timeNow<rotatePeriod){
            while (isFreezed) yield return null; 

            eulerTotem.z = angleTotem + 90 * timeNow / rotatePeriod;
            totem.transform.localEulerAngles = eulerTotem;

            eulerPullerMask.z = anglePullerMask - 90 * timeNow / rotatePeriod;
            pullerMask.transform.localEulerAngles = eulerPullerMask;

            timeNow += Time.deltaTime;
            yield return null;
        }

        eulerTotem.z = angleTotem + 90;
        totem.transform.localEulerAngles = eulerTotem;

        eulerPullerMask.z = anglePullerMask - 90;
        pullerMask.transform.localEulerAngles = eulerPullerMask;

        isRotating = false;
    }        

}
