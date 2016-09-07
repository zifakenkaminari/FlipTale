﻿using UnityEngine;
using System.Collections;

public class Paper : Item {

    public Sprite paperCrumpled;
    public Sprite paperPlane;
    public float destroyPeriod;
    protected GameObject mapDesign;
    protected int paperState; //0: normal, 1: crumpled, 2: plane

    protected new void Start()
    {
        base.Start();
        paperState = 0; // normal
    }

    public override void drop(GameObject player)
    {
        base.drop(player);
        pickable = false;
        StartCoroutine(disappear());
    }

    public IEnumerator disappear() {
        float dropTime = Time.time;
        while(Time.time - dropTime < destroyPeriod){
            float rate = (Time.time - dropTime) / destroyPeriod;
            Color color = front.GetComponent<Renderer> ().material.color;
            color.a = 1 - rate;
            front.GetComponent<Renderer> ().material.color = color;

            color = back.GetComponent<Renderer> ().material.color;
            color.a = 1 - rate;
            back.GetComponent<Renderer> ().material.color = color;
            yield return null;
        }
        Destroy(gameObject);
    }

    public void magic(){
        paperState = 2;         //become plane
        front.GetComponent<SpriteRenderer>().sprite = paperPlane;
        back.GetComponent<SpriteRenderer>().sprite = paperPlane;
    }

    public override bool use(GameObject player)
    {
        if (paperState == 0) {          //normal
            if (mapDesign == null || mapDesign.GetComponent<Entity> ().face) {
                paperState = 1;         //become crumpled
                front.GetComponent<SpriteRenderer> ().sprite = paperCrumpled;
                back.GetComponent<SpriteRenderer> ().sprite = paperCrumpled;
            }
            else {
                paperState = 2;         //become plane
                front.GetComponent<SpriteRenderer> ().sprite = paperPlane;
                back.GetComponent<SpriteRenderer> ().sprite = paperPlane;
            }
            base.use (player);
        }
        else if (paperState == 2)
        {          //plane
            paperState = 3;
            transform.parent = player.transform.parent;
            StartCoroutine(fly());
        }
        return false;
    }

    protected IEnumerator fly()
    {
        if (face)
            setTransparent(ref front, 1);
        else
            setTransparent(ref back, 1);
        float b = 0.6f;
        Vector3 v = new Vector3(9f, 6f, 0f);
        Vector3 g = new Vector3(0, -1f, 0f);
        Quaternion rotation = transform.localRotation;
        Vector3 eular = rotation.eulerAngles;
        while(true){
            eular.z = Mathf.Atan(v.y/v.x)*180/Mathf.PI-30;
            rotation.eulerAngles = eular;
            transform.localRotation = rotation;
            transform.position += v * Time.deltaTime;
            v += g * Time.deltaTime;
            //v -= b * v * Time.deltaTime;
            v -= b * new Vector3(0, v.y, 0) * Time.deltaTime;
            yield return null;
        }

    }

    public int getPaperState() {
        return paperState;
    }
 
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "MapDesign")
        {
            mapDesign = collider.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (mapDesign && collider.gameObject == mapDesign)
        {
            mapDesign = null;
        }
    }
}
