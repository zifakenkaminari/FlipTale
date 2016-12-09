using UnityEngine;
using System.Collections;

public class Plane : MonoBehaviour {
    public EdgeCollider2D trigger;
    public EdgeCollider2D floor;


    void OnTriggerEnter2D (Collider2D collider){
        Physics2D.IgnoreCollision(collider, floor);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        Physics2D.IgnoreCollision(collider, floor, false);
    }
}   
