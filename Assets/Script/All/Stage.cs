using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class Stage : MonoBehaviour {

    public Entity[] flipables;
    public GameObject background;

    public bool face;           //not flipped at first
    public bool isFlipping;
    public float flipPeriod;

    public float viewLeft;
    public float viewRight;
    public float viewUp;
    public float viewDown;

    // Use this for initialization
    void Start()
    {
        face = true;
        isFlipping = false;
        background.GetComponent<Background>().flipPeriod = flipPeriod;
    }

    void Update() {

        flipables = GetComponentsInChildren<Entity>();
        //int flipableSize = flipables.Length;
        Array.Sort(flipables, (a, b) =>
            a.transform.position.x.CompareTo(b.transform.position.x)
        );
    }

    public void lockMotion() {
        foreach (Entity flipable in flipables)
        {
            flipable.lockMotion();
        }
    }

    public void unlockMotion()
    {
        foreach (Entity flipable in flipables)
        {
            flipable.unlockMotion();
        }
    }

    public IEnumerator flip() {
        if (isFlipping)
            yield break;
        isFlipping = true;

        //lock
        foreach (Entity flipable in flipables) {
            flipable.lockMotion();
        }
        StartCoroutine(background.GetComponent<Background>().flip());
        //flip one by one
        int flipableSize = flipables.Length;
        for (int i = 0; i < flipableSize - 1; i++)
        {
            StartCoroutine(flipables[i].flip());
            yield return new WaitForSeconds(flipPeriod / flipableSize);
        }
        //last one
        if (flipableSize > 0)
            yield return flipables[flipableSize-1].flip();
        //unlock
        foreach (Entity flipable in flipables) {
            flipable.unlockMotion();
        }
        face = !face;
        isFlipping = false;
    }
        
}
