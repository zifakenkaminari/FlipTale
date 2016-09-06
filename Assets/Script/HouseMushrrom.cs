using UnityEngine;
using System.Collections;

public class HouseMushrrom : StageTrigger {

    public override void OnTriggerEnter2D (Collider2D collider)
    {
        if(GetComponent<Entity>().face){
            base.OnTriggerEnter2D (collider);
        }
    } 
}
