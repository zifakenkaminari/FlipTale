using UnityEngine;
using System.Collections;


public class Cave : Entity {
    public Sprite frontCaveBurned;
    public Sprite backCaveBurned;
    public void burn() {
        front.GetComponent<SpriteRenderer>().sprite = frontCaveBurned;
        back.GetComponent<SpriteRenderer>().sprite = backCaveBurned;
        GetComponent<Collider2D>().enabled = false;
    }
}
