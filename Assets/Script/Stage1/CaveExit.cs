﻿using UnityEngine;
using System.Collections;

public class CaveExit : MonoBehaviour {
    public bool isOpen;
    protected float openPeriod = 2.1f;
    protected float shiftLightX = 4.05f;
    protected float shiftWallY = 3.7f;

	protected void Start () {
        isOpen = false;
	}

    public IEnumerator open() {
        if (isOpen) yield break;
        isOpen = true;
        Vector2 pos = transform.position;
        Vector2 posWall = GetComponent<BoxCollider2D> ().offset;
        float lightX = pos.x;
        float wallX = posWall.x;
        float wallY = posWall.y;
        float timeNow = 0;
        while(timeNow < openPeriod) {
            while (GetComponentInParent<Flipper>().isFlipping) yield return null;
            pos.x = lightX - shiftLightX * timeNow / openPeriod;
            posWall.x = wallX + shiftLightX * timeNow / openPeriod;
            posWall.y = wallY + shiftWallY * timeNow / openPeriod;
            transform.position = pos;
            GetComponent<BoxCollider2D> ().offset = posWall;
            timeNow += Time.deltaTime;
            yield return null;
        }
        pos.x = lightX - shiftLightX;
        posWall.x = wallX + shiftLightX;
        posWall.y = wallY + shiftWallY;
        transform.position = pos;
        GetComponent<BoxCollider2D> ().offset = posWall;
        // GetComponent<BoxCollider2D> ().enabled = false;
    }
}
