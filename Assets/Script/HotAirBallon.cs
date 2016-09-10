using UnityEngine;
using System.Collections;

public class HotAirBallon : Machine {

    protected int getItemCount;
    protected GameObject[] getItems;
    protected bool flying;
    public float upForce;
    public float downForce;
    public Sprite frontFlying;
    public Sprite backFlying;

    protected override void Start ()
    {
        getItemCount = 0;
        getItems = new GameObject [3];
        front.SetActive (false);
        back.SetActive (false);
        flying = false;
        base.Start ();
    }

    protected override void main()
    {
        if(flying) {
            if (!face) {
                rb.AddForce(new Vector2(0, upForce * rb.mass));
            }
            else {
                rb.AddForce(new Vector2(0, -downForce * rb.mass));
            }
            if (!back.GetComponent<SpriteRenderer>().isVisible) {
                flying = false;
                Camera.main.GetComponent<CameraController>().end();
            }
        }
    }

    public void getItem(GameObject item) {
        item.transform.parent = transform;
        item.GetComponent<Item> ().pickable = false;
        getItems [getItemCount] = item;
        getItemCount++;
        if (getItemCount == 3) {
            front.SetActive (true);
            back.SetActive (true);
            foreach(GameObject eachItem in getItems) {
                eachItem.SetActive (false);
            }
        }
    }

    public override void use(GameObject player)
    {
        if(getItemCount == 3 && !face) {
            player.transform.parent = transform;
            setTransparent(ref player.GetComponent<Player>().front, 0);
            setTransparent(ref player.GetComponent<Player>().back, 0);
            Destroy(player.GetComponent<Rigidbody2D>());

            flying = true;
            front.GetComponent<SpriteRenderer> ().sprite = frontFlying;
            back.GetComponent<SpriteRenderer> ().sprite = backFlying;
        }
        base.use(player);
    }
}
