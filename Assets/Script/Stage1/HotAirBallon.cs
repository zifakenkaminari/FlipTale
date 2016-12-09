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
        base.Start();
        getItemCount = 0;
        getItems = new GameObject [3];
        setAlpha(0);
        flying = false;
    }



    protected override void main()
    {
        if (GameObject.Find("Manager").GetComponent<Manager>().GM_mode == true && Input.GetKeyDown(KeyCode.Q)) {
            setAlpha(1);
            getItemCount = 3;
        }
            
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
        /*
        Vector3 pos = item.transform.position;
        pos.x = transform.position.x;
        item.transform.position = pos;
        */
        item.GetComponent<Item> ().pickable = false;
        getItems [getItemCount] = item;
        getItemCount++;
        if (getItemCount == 3) {
            setAlpha(1);
            foreach(GameObject eachItem in getItems) {
                Destroy(eachItem);
            }
            //flipType = 0;
        }
    }

    public override void use(GameObject player)
    {
        if(getItemCount == 3 && !face) {
            player.AddComponent<FixedJoint2D>().connectedBody = rb;
            player.GetComponent<Player>().setAlpha(0);
            player.GetComponent<Player>().setControlable(false);
            player.GetComponent<Rigidbody2D>().mass = 0.001f;

            flying = true;
            front.GetComponent<SpriteRenderer> ().sprite = frontFlying;
            back.GetComponent<SpriteRenderer> ().sprite = backFlying;
        }
        base.use(player);
    }
}
