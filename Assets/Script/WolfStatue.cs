using UnityEngine;
using System.Collections;

public class WolfStatue : Entity {


    protected override void Start() {
        base.Start();
    }



    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("TrashBag"))
        {
            collider.gameObject.GetComponent<TrashBag>().pickable = false;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("TrashBag"))
        {
            collider.gameObject.GetComponent<TrashBag>().pickable = true;
        }
    }
}
