using UnityEngine;
using System.Collections;

public class Plane : MonoBehaviour {
    public EdgeCollider2D trigger;
    public EdgeCollider2D collider;
    void OnTriggerEnter2D (Collider2D collider){
        this.collider.enabled = false;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        this.collider.enabled = true;
    }
}   
