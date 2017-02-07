using UnityEngine;
using System.Collections;

public class WaterIce : Entity {
	[SerializeField]	protected float speed;
	[SerializeField]	protected float amp;
	protected float slowStart;
	protected float initY;
	protected float time;

	protected override void Start(){
		base.Start();
		initY = transform.position.y;
	}

	protected override void main() { 
		if (face) {
			Vector2 offset = front.GetComponent<Renderer> ().material.mainTextureOffset;
			offset.x = Mathf.Repeat (offset.x + Time.deltaTime * speed, 1f); 
			front.GetComponent<Renderer> ().material.mainTextureOffset = offset;
			back.GetComponent<Renderer> ().material.mainTextureOffset = offset;
			Vector3 pos = transform.position;
			pos.y = initY - amp*(0.5f + 0.5f * Mathf.Cos (time));
			transform.position = pos;
			time += Time.deltaTime;
		}
	}


	protected override void setTransparent(ref GameObject bg, float a) {
		
		Color tmpColor = bg.GetComponent<MeshRenderer>().material.color;
		tmpColor.a = a;
		bg.GetComponent<MeshRenderer>().material.color = tmpColor;
		return;
	}



}
