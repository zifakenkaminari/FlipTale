using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpButton : MonoBehaviour {
	bool isClicked;
	Color color1;
	Color color2;
	// Use this for initialization
	void Start () {
		color1 = new Color (1f, 1f, 0.7f);
		color2 = Color.white;
		isClicked = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isClicked) {
			GetComponent<Image> ().color = Color.Lerp (color1, color2, 0.5f + 0.5f * Mathf.Sin (Time.time * 2 * Mathf.PI * 3));
		}
	}

	public void onClick(){
		isClicked = true;
		GetComponent<Image> ().color = color2;
	}
}
