using UnityEngine;
using System.Collections;

public class HotAirBalloon : Machine {

    //protected int getItemCount;
    //protected GameObject[] getItems;
    protected bool flying;
    public float upForce;
    public float downForce;
    public Sprite frontFlying;
    public Sprite backFlying;

    protected override void Start ()
    {
        base.Start();
        //getItemCount = 0;
        //getItems = new GameObject [3];
        //setAlpha(0);
        flying = false;
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

    public override void use(GameObject player)
	{
		player.AddComponent<FixedJoint2D> ().connectedBody = rb;
		player.GetComponent<Player> ().setAlpha (0);
		player.GetComponent<Player> ().setControlable (false);
		player.GetComponent<Collider2D> ().enabled = false;
		player.GetComponent<Rigidbody2D> ().mass = 0.001f;
		flying = true;
		setSprite (frontFlying, backFlying);
		base.use (player);
	}
}
