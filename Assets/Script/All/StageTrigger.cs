﻿using UnityEngine;
using System.Collections;

public class StageTrigger : MonoBehaviour {

    public GameObject stage0;
    public GameObject stage1;
    [SerializeField]
    [HideInInspector]
    protected int direction;         // 0: horizontal, 1: vertical

    // Use this for initialization
    void Start () {
    }

    //change stage
    public void OnTriggerExit2D(Collider2D collider)
    {
        Player player = collider.GetComponent<Player>();
        if (player)
        {
            GameObject nxtStage = null;
            if (direction == 0)
            {                                   // horizontal
                if (player.transform.position.x > this.transform.position.x)
                {
                    nxtStage = (stage0.transform.position.x > stage1.transform.position.x) ? stage0 : stage1;
                }
                else
                {
                    nxtStage = (stage0.transform.position.x < stage1.transform.position.x) ? stage0 : stage1;
                }
            }
            else if (direction == 1)
            {                              // vertical
                if (player.transform.position.y > this.transform.position.y)
                {
                    nxtStage = (stage0.transform.position.y > stage1.transform.position.y) ? stage0 : stage1;
                }
                else
                {
                    nxtStage = (stage0.transform.position.y < stage1.transform.position.y) ? stage0 : stage1;
                }
            }
            if (player.nowStage != nxtStage)
            {
                StartCoroutine(switchScene(player, nxtStage));
            }
        }
        
    }

    public IEnumerator switchScene(Player player, GameObject nxtStage)
    {
        player.lockMotion();

        Color maskColor = Camera.main.GetComponent<CameraController>().endingBlank.GetComponent<SpriteRenderer>().color;
        float time = 0;
        float TIME = 0.2f;
        maskColor = Color.black;
        while(time<TIME){
            maskColor.a = time / TIME;
            Camera.main.GetComponent<CameraController>().endingBlank.GetComponent<SpriteRenderer>().color = maskColor;
            time += Time.deltaTime;
            yield return null;
        }
        maskColor.a = 1;
        Camera.main.GetComponent<CameraController>().endingBlank.GetComponent<SpriteRenderer>().color = maskColor;


        player.nowStage = nxtStage;
        player.transform.parent = player.nowStage.transform;
        
        time = 0;
        while (time < TIME)
        {
            maskColor.a = 1 - time / TIME;
            Camera.main.GetComponent<CameraController>().endingBlank.GetComponent<SpriteRenderer>().color = maskColor;
            time += Time.deltaTime;
            yield return null;
        } maskColor.a = 0;
        Camera.main.GetComponent<CameraController>().endingBlank.GetComponent<SpriteRenderer>().color = maskColor;
       

        player.unlockMotion();
    }

    // 0: horizontal, 1: vertical
    public void setDirection(int dir) {
        this.direction = dir;
    }
    public int getDirection() {
        return this.direction;
    }
}
