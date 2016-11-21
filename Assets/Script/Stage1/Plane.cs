using UnityEngine;
using System.Collections;

public class Plane : MonoBehaviour {
    public EdgeCollider2D trigger;
    public EdgeCollider2D floor;
    int touchCount;
    void OnTriggerEnter2D (Collider2D collider){
        if(collider.CompareTag("Player"))
            floor.isTrigger = true;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && !floor.IsTouching(collider))
            floor.isTrigger = false;
    }
}   
