﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class CameraController : MonoBehaviour {
    public Player player;

    public float changePeriod;
    protected float startTime;
    protected bool isChanging;
    protected bool toChange;
    public GameObject endingBlank;

    void Start()
    {
        isChanging = false;
        setTransparent (ref endingBlank, 0);
    }
    void FixedUpdate ()
    {
        Vector3 now = transform.position;
        Vector3 pos = player.transform.position;
        Vector3 center = player.nowStage.transform.position;
        float viewLeft = player.nowStage.GetComponent<Stage>().viewLeft;
        float viewRight = player.nowStage.GetComponent<Stage>().viewRight;
        float viewUp = player.nowStage.GetComponent<Stage>().viewUp;
        float viewDown = player.nowStage.GetComponent<Stage>().viewDown;

        pos.x = Mathf.Clamp(pos.x, center.x - viewRight, center.x + viewLeft);
        pos.y = Mathf.Clamp(pos.y, center.y - viewDown, center.y + viewUp);

        now.x = Mathf.Lerp(now.x, pos.x, 0.1f);
        now.y = Mathf.Lerp(now.y, pos.y, 0.1f);
        transform.position = now;

        if (isChanging)
        {
            if (Time.time - startTime < changePeriod)
            {
                setTransparent (ref endingBlank, (Time.time - startTime) / changePeriod);
            }
            else
            {
                setTransparent (ref endingBlank, 1);
                isChanging = false;
                StartCoroutine (fadeOut ());
            }
        }
    }
    public void end()
    {
        startTime = Time.time;
        isChanging = true;
        toChange = false;
    }

    protected IEnumerator fadeOut(){
        float p = 2;
        float timeNow = 0;
        while(timeNow<p){
            Camera.main.GetComponent<AudioSource> ().volume = 1-timeNow / p;
            timeNow += Time.deltaTime;
            yield return null;
        }


        SceneManager.LoadScene ("Ending");
    }

    protected void setTransparent(ref GameObject bg, float a) {
        Color tmpColor = bg.GetComponent<SpriteRenderer> ().color;
        tmpColor.a = a;
        bg.GetComponent<SpriteRenderer> ().color = tmpColor;
        return;
    }

}