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
	}
	

    public IEnumerator flip() {
        if (isFlipping)
            yield break;
        isFlipping = true;
        float timeNow = 0;
        while (timeNow < flipPeriod)
        {
            if (face)
                setTransparent(ref bgFront, Mathf.Cos(timeNow / flipPeriod * Mathf.PI / 2));
            else
                setTransparent(ref bgFront, 1 - Mathf.Cos(timeNow / flipPeriod * Mathf.PI / 2));
            timeNow += Time.deltaTime;
            yield return null;
        }
        if (face)
            setTransparent(ref bgFront, 0);
        else
            setTransparent(ref bgFront, 1);
        face = !face;
        isFlipping = false;
    }	


	private void setTransparent(ref GameObject bg, float a) {
		Color tmpColor = bg.GetComponent<SpriteRenderer> ().color;
		tmpColor.a = a;
		bg.GetComponent<SpriteRenderer> ().color = tmpColor;
		return;
	}
}
