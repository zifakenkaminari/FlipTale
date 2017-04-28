using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HintText : MonoBehaviour {
	[SerializeField]
	protected int flashPeriod;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Color color = GetComponent<Text> ().color;
		color = Color.Lerp(Color.white, Color.black, Mathf.PingPong (Time.time, flashPeriod)/flashPeriod*0.8f + 0.1f);
		GetComponent<Text> ().color = color;
	}
}
