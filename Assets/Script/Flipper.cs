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
            if (flipId < flipables.Length)
            {
                if (Time.time - flipTime > flipPeriod*flipId/flipables.Length)
                {
                    StartCoroutine(flipables[flipId].GetComponent<Entity>().flip());
                    flipId++;
                }
            }
            else if (Time.time -flipTime > flipables[flipId - 1].GetComponent<Entity>().flipPeriod + flipPeriod)
            {
                face = !face;
                isFlipping = false;
                foreach (GameObject flipable in flipables)
                {
                    flipable.GetComponent<Entity>().unlockMotion();
                }
            }
        }
    }

    public void flip()
    {
        if (isFlipping) return;
        isFlipping = true;
        flipTime = Time.time;
        flipId = 0;
        Array.Sort(flipables, delegate(GameObject a, GameObject b)
        {
            return a.transform.position.x.CompareTo(b.transform.position.x);
        });
        foreach (GameObject flipable in flipables) {
            flipable.GetComponent<Entity>().lockMotion();
        }
        background.GetComponent<Background>().flip();
    }
	
}
