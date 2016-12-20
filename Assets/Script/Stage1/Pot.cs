using UnityEngine;
using System.Collections;

public class Pot : Entity {

    public GameObject flowerTorch;
    public Sprite potTorch;
    protected Sprite pot_b;

    protected override void Start ()
    {
        base.Start ();
        pot_b = back.GetComponent<SpriteRenderer> ().sprite;
    }

    public void bloom(GameObject player) {
        GameObject newFlower = (GameObject)Instantiate(flowerTorch, player.transform.parent);
        newFlower.transform.position = transform.position;

        front.GetComponent<Animator> ().SetBool ("Bloom", true);
        back.GetComponent<SpriteRenderer> ().sprite = potTorch;
    }
    public void empty() {
        front.GetComponent<Animator> ().SetBool ("Bloom", false);
        back.GetComponent<SpriteRenderer> ().sprite = pot_b;
    }
}
