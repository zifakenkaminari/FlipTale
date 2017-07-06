using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask : MonoBehaviour {
	public static Mask main;
	void Start(){
		main = this;
	}
	public IEnumerator changeMaskColor(Color colorBefore, Color colorAfter, float period){
		float time = 0;
		while(time<period){
			GetComponent<SpriteRenderer> ().color = Color.Lerp (colorBefore, colorAfter, time/period);
			time += Time.deltaTime;
			yield return null;
		}
		GetComponent<SpriteRenderer> ().color = colorAfter;

	}

	public IEnumerator changeMaskColor(Color colorAfter, float period){
		float time = 0;
		Color colorBefore = GetComponent<SpriteRenderer> ().color;
		while(time<period){
			GetComponent<SpriteRenderer> ().color = Color.Lerp (colorBefore, colorAfter, time/period);
			time += Time.deltaTime;
			yield return null;
		}
		GetComponent<SpriteRenderer> ().color = colorAfter;

	}

}
