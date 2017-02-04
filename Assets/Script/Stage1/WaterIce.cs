using UnityEngine;
using System.Collections;

public class WaterIce : Entity {
	[SerializeField]
	protected float speed;
	protected float slowStart;

	protected override void main() { 
		if (face) {
			Vector2 offset = front.GetComponent<Renderer> ().material.mainTextureOffset;
			offset.x = Mathf.Repeat (offset.x + Time.deltaTime * speed, 1f); 
			front.GetComponent<Renderer> ().material.mainTextureOffset = offset;
			back.GetComponent<Renderer> ().material.mainTextureOffset = offset;
		}
	}


	protected override void setTransparent(ref GameObject bg, float a) {
		
		Color tmpColor = bg.GetComponent<MeshRenderer>().material.color;
		tmpColor.a = a;
		bg.GetComponent<MeshRenderer>().material.color = tmpColor;
		return;
	}



}
