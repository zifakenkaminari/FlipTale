using UnityEngine;
using System.Collections;

public class Paper : Item {

    public Sprite paperCrumpled;
    public Sprite paperPlane;
    public float destroyPeriod;

    protected new void Start()
    {
        base.Start();
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
        if (state != 2) {
            front.GetComponent<SpriteRenderer> ().sprite = paperCrumpled;
            back.GetComponent<SpriteRenderer> ().sprite = paperCrumpled;
        }
        return false;
    }
        
}
