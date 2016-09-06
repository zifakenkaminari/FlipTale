using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class Flipper : MonoBehaviour {

    public GameObject[] flipables;
    public GameObject background;

    public bool face;           //not flipped at first
    public bool isFlipping;
    public float flipTime;
    public float flipPeriod;
    public int flipId;

    // Use this for initialization
    void Start()
    {
        face = true;
        isFlipping = false;
        flipTime = -10000f;
        background.GetComponent<Background>().flipPeriod = flipPeriod;
    }

    void FixedUpdate() {
        if (isFlipping)
        {
            if (flipId < flipables.Length) {
                while (Time.time - flipTime > flipPeriod * flipId / flipables.Length) {
                    StartCoroutine (flipables [flipId].GetComponent<Entity> ().flip ());
                    flipId++;
                }
            }
            else if (flipables.Length > 0) {
                if (Time.time - flipTime > flipables [flipId - 1].GetComponent<Entity> ().flipPeriod + flipPeriod) {
                    face = !face;
                    isFlipping = false;
                    foreach (GameObject flipable in flipables) {
                        flipable.GetComponent<Entity> ().unlockMotion ();
                    }
                } 
            }
            else if (Time.time - flipTime > flipPeriod) {
                face = !face;
                isFlipping = false;
            }
        }
    }

    public void flip()
    {
        if (isFlipping) return;
        isFlipping = true;
        flipTime = Time.time;
        flipId = 0;
        int flipableSize = 0, idx = 0;
        for (int i = 0; i < transform.childCount; i++) {
            if (transform.GetChild (i).GetComponent<Entity> ()) {
                flipableSize++;
            }
        }
        flipables = new GameObject[flipableSize];
        for (int i = 0; i < transform.childCount; i++) {
            if (transform.GetChild (i).GetComponent<Entity> ()) {
                flipables[idx] = transform.GetChild(i).gameObject;
                idx++;
            }
        }
        Array.Sort(flipables, delegate(GameObject a, GameObject b){
            return a.transform.position.x.CompareTo(b.transform.position.x);
        });
        foreach (GameObject flipable in flipables) {
            flipable.GetComponent<Entity>().lockMotion();
        }
        background.GetComponent<Background>().flip();
    }

}
