using UnityEngine;
using System.Collections;

public class WaterIce : Entity {
	[SerializeField]	protected float speed;
	protected float slowStart;
	protected float time;

	protected override void Start(){
		base.Start();
	}

	protected override void main() { 
		if (face) {
			Vector2 offset = new Vector2(Mathf.Repeat (time * speed, 1f), 0f);
			front.GetComponent<Renderer> ().material.mainTextureOffset = offset;
			back.GetComponent<Renderer> ().material.mainTextureOffset = offset;
			Vector3 pos = transform.position;
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
