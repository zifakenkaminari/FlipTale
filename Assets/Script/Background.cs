using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

	public GameObject bgFront;
	public GameObject bgBack;

	protected bool face;
	protected bool isFlipping;
	protected float flipTime;
	public float flipPeriod;

	// Use this for initialization
	void Start () {
		face = true;
		isFlipping = false;
		//setTransparent (ref bgBack, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (isFlipping)
		{
			flipping();
		}

	}
		
	public void flip() {
		if (isFlipping) return;
		isFlipping = true;
		flipTime = Time.time;
	}
	protected void flipping()
	{
		if (Time.time - flipTime < flipPeriod)
		{
			if (face)
			{
				// -- change background slow to fast --
				setTransparent(ref bgFront, Mathf.Cos((Time.time - flipTime) / flipPeriod * Mathf.PI / 2) );
				//setTransparent(ref bgBack, 1 - Mathf.Cos((Time.time - flipTime) / flipPeriod * Mathf.PI / 2) );

				// -- uniformly change background --
				//setTransparent(ref bgFront, 1 - (Time.time - flipTime) / flipPeriod);
				//setTransparent(ref bgBack, (Time.time - flipTime) / flipPeriod);
			}
			else
			{
				// -- change background slow to fast --
				setTransparent(ref bgFront, 1 - Mathf.Cos((Time.time - flipTime) / flipPeriod * Mathf.PI / 2) );
				//setTransparent(ref bgBack, Mathf.Cos((Time.time - flipTime) / flipPeriod * Mathf.PI / 2) );

				// -- uniformly change background --
				//setTransparent(ref bgFront, (Time.time - flipTime) / flipPeriod);
				//setTransparent(ref bgBack, 1 - (Time.time - flipTime) / flipPeriod);
			}
		}
		else
		{
			face = !face;
			isFlipping = false;
		}
	}

	private void setTransparent(ref GameObject bg, float a) {
		Color tmpColor = bg.GetComponent<SpriteRenderer> ().color;
		tmpColor.a = a;
		bg.GetComponent<SpriteRenderer> ().color = tmpColor;
		return;
	}
}
