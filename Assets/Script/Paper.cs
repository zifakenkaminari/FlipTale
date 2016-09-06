using UnityEngine;
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
        else if(paperState == 2) {          //plane
        }
        return false;
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
