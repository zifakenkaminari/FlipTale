using UnityEngine;
using System.Collections;

public class Totem : Entity {
    GameObject player;
    public int state;
    bool isRotating;
    public float rotatePeriod;

    protected override void Start()
    {
        base.Start();
        isRotating = false;
        state = 5;
	}

    protected void Update() {
        if (state == 5) { 
        }
        else if (player && Input.GetKeyDown(KeyCode.C)) {
            use(player);
        }
    }

    public void use(GameObject player) { 
        //turn 90 degree
        if (!isRotating && face)
        {
            state = (state + 1) % 4;
            isRotating = true;
            StartCoroutine(rotate());
        }
    }

    protected IEnumerator rotate() {
        float beginTime = Time.time;
        Quaternion rotation = transform.rotation;
        Vector3 euler = rotation.eulerAngles;
        float angle = euler.z;
        while(Time.time-beginTime<rotatePeriod){
            euler.z = angle + 90 * (Time.time - beginTime) / rotatePeriod;
            rotation.eulerAngles = euler;
            transform.rotation = rotation;
            yield return null;
        }
        euler.z = angle + 90;
        rotation.eulerAngles = euler;
        transform.rotation = rotation;
        isRotating = false;
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            player = collider.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (player && collider.gameObject == player)
        {
            player = null;
        }
    }

}
