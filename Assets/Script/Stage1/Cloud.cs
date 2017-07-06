using UnityEngine;
using System.Collections;

public class Cloud : Entity {
	[SerializeField]	protected GameObject plane; 

    // Use this for initialization
    protected override void Start()
    {
		base.Start();
		plane.SetActive (false);
		setFlipValue (1f);
    }

    public override IEnumerator flip()
    {
		plane.SetActive (face);
        yield return base.flip();
    }

	public override void setFlipValue(float f)
	{
		//flip value: front = 1, back = 0
		flipValue = f;
		alpha = 0.75f + (1f-f)*0.25f;
		float frontAlpha = f * alpha;
		float backAlpha = float.Equals(alpha*flipValue, 1f)?1f:alpha * (1f - f) / (1f - alpha * f);
		setTransparent(ref front, frontAlpha);
		setTransparent(ref back, backAlpha);
	}

	public override void setAlpha(float alpha)
	{
		//do nothing
	}
		
}
