﻿using UnityEngine;
using System.Collections;

public class Puller : Entity {
    GameObject player;
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

    public void pulled() {
        front.GetComponent<SpriteRenderer>().sprite = frontPuller;
        back.GetComponent<SpriteRenderer>().sprite = backPuller;
    }

    protected IEnumerator rotate() {
        float timeNow = 0;
        Quaternion rotation = totem.transform.rotation;
        Vector3 euler = rotation.eulerAngles;
        float angle = euler.z;
        while(timeNow<rotatePeriod){
            while (isFreezed) yield return null; 
            euler.z = angle + 90 * timeNow / rotatePeriod;
            rotation.eulerAngles = euler;
            totem.transform.rotation = rotation;
            timeNow += Time.deltaTime;
            yield return null;
        }
        euler.z = angle + 90;
        rotation.eulerAngles = euler;
        totem.transform.rotation = rotation;
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