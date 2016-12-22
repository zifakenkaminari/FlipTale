using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class Flipper : MonoBehaviour {

    public GameObject[] flipables;
    public GameObject background;

    public bool face;           //not flipped at first
    public bool isFlipping;
    public float flipPeriod;

    // Use this for initialization
    void Start()
    {
        face = true;
        isFlipping = false;
        background.GetComponent<Background>().flipPeriod = flipPeriod;
    }

    public IEnumerator flip() {
        if (isFlipping)
            yield break;
        isFlipping = true;
        Entity[] entities = GetComponentsInChildren<Entity>();
        int flipableSize = entities.Length;
        flipables = new GameObject[flipableSize];
        for (int i = 0; i < flipableSize;i++)
        {
            flipables[i] = entities[i].gameObject;
        }
        Array.Sort(flipables, delegate(GameObject a, GameObject b){
            return a.transform.position.x.CompareTo(b.transform.position.x);
        });
        foreach (GameObject flipable in flipables) {
            flipable.GetComponent<Entity>().lockMotion();
        }
        StartCoroutine(background.GetComponent<Background>().flip());

        for (int i = 0; i < flipableSize - 1; i++)
        {
            StartCoroutine(flipables[i].GetComponent<Entity>().flip());
            yield return new WaitForSeconds(flipPeriod / flipableSize);
        }
        if (flipableSize > 0)
            yield return flipables[flipableSize-1].GetComponent<Entity>().flip();
        foreach (GameObject flipable in flipables) {
            flipable.GetComponent<Entity>().unlockMotion();
        }
        face = !face;
        isFlipping = false;
    }
        
}
