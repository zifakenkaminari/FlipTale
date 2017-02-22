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
        Vector2 tmpOffset = GetComponent<BoxCollider2D>().offset;
        Vector2 tmpSize = GetComponent<BoxCollider2D>().size;
        tmpOffset.y = 0.3f;
        tmpSize.y = 4.3f;
        GetComponent<BoxCollider2D>().offset = tmpOffset;
        GetComponent<BoxCollider2D>().size = tmpSize;
    }

    protected IEnumerator rotate() {
        float timeNow = 0;

        Vector3 eulerTotem = totem.transform.localEulerAngles;
        float angleTotem = eulerTotem.z;

        Vector3 eulerPullerMask = pullerMask.transform.localEulerAngles;
        float anglePullerMask = eulerPullerMask.z;

        while(timeNow<rotatePeriod){

            eulerTotem.z = angleTotem + 90 * timeNow / rotatePeriod;
            totem.transform.localEulerAngles = eulerTotem;

            eulerPullerMask.z = anglePullerMask + 90 * timeNow / rotatePeriod;
            pullerMask.transform.localEulerAngles = eulerPullerMask;

			timeNow += Time.deltaTime;
			yield return new WaitWhile(() => isFreezed);
        }

        eulerTotem.z = angleTotem + 90;
        totem.transform.localEulerAngles = eulerTotem;

        eulerPullerMask.z = anglePullerMask + 90;
        pullerMask.transform.localEulerAngles = eulerPullerMask;

        isRotating = false;
    }        

}
