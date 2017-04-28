using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Cover : Entity {
	[SerializeField]	protected Sprite[] citySprites;


	protected void Update(){
		if (Input.GetKeyDown (KeyCode.Z)) {
			if (!isFlipping) {
				StartCoroutine(flip ());
			}
		}
		if (!face) {
			int idx = (int)(Time.time*2) % 6;
			Sprite sprite;
			switch (idx) {
			case 0:
				sprite = citySprites [0];
				break;
			case 1:
				sprite = citySprites [1];
				break;
			case 2:
				sprite = citySprites [2];
				break;
			case 3:
				sprite = citySprites [3];
				break;
			case 4:
				sprite = citySprites [2];
				break;
			case 5:
				sprite = citySprites [1];
				break;
			default:
				sprite = citySprites [0];
				break;
			}
			back.GetComponent<Image> ().sprite = sprite;
		}

		//back.GetComponent<Image> ().sprite;
	}

	public override void setFlipValue(float f)
	{
		//flip value: front = 1, back = 0
		flipValue = f;
		if (flipType == 0)
		{
			Vector3 scale = transform.localScale;
			scale.x = Mathf.Abs(Mathf.Cos((1-f)*Mathf.PI));
			transform.localScale = scale;
			if(f>0.5){
				setTransparent(ref front, alpha);
				setTransparent(ref back, 0);
			}
			else{
				setTransparent(ref front, 0);
				setTransparent(ref back, alpha);
			}
		}
		else if (flipType == 1)
		{
			float frontAlpha = f * alpha;
			float backAlpha = float.Equals(alpha*flipValue, 1f)?1f:alpha * (1f - f) / (1f - alpha * f);
			setTransparent(ref front, frontAlpha);
			setTransparent(ref back, backAlpha);
		}
	}

	protected override void setTransparent(ref GameObject bg, float a) {
		Color tmpColor = bg.GetComponent<Image>().color;
		tmpColor.a = a;
		bg.GetComponent<Image>().color = tmpColor;
	}



}
