using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class Stage : MonoBehaviour {

    public Entity[] flipables;
	public Background[] backgrounds;

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
		backgrounds = GetComponentsInChildren<Background> ();
		foreach (Background bg in backgrounds) {
			bg.flipPeriod = flipPeriod;
		}
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

		float startTime = Time.time;

		flipables = GetComponentsInChildren<Entity> ();
		int flipableSize = flipables.Length;
		Array.Sort (flipables, (a, b) =>
			a.transform.position.x.CompareTo (b.transform.position.x)
		);

		//lock
		foreach (Entity flipable in flipables) {
			flipable.lockMotion ();
		}
		foreach (Background bg in backgrounds) {
			StartCoroutine (bg.flip ());
		}
		foreach (Entity entity in flipables) {
			if (entity.isFlipWithBackground ()) {
				StartCoroutine (entity.flip ());
			}
		}
		//flip one by one
		for (int i = 0; i < flipableSize - 1; i++) {
			if (!flipables [i].isFlipWithBackground ()){
				StartCoroutine (flipables [i].flip ());
			}
			yield return new WaitForSeconds (flipPeriod / flipableSize);
		}
        //last one
        if (flipableSize > 0)
            yield return flipables[flipableSize-1].flip();
		yield return new WaitUntil (() => Time.time > startTime + flipPeriod);
        //unlock
        foreach (Entity flipable in flipables) {
            flipable.unlockMotion();
        }
        face = !face;
        isFlipping = false;
    }
        
}
