using UnityEngine;
using System.Collections;


public class Cave : Entity {
    public Sprite caveBurned;
    public void burn() {
        front.GetComponent<SpriteRenderer>().sprite = caveBurned;
        back.GetComponent<SpriteRenderer>().sprite = caveBurned;
        GetComponent<Collider2D>().enabled = false;
    }
}
