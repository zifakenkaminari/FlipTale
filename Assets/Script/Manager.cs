using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

	public string loadScene;
	public string menuScene;
	public GameObject[] stages;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartGame () {
		print ("StartGame");
		SceneManager.LoadScene (loadScene);
	}


	public void ExitGame () {
		print ("ExitGame");
		Application.Quit();
	}

	public void BackMenu() {
		print ("BackMenu");
		SceneManager.LoadScene (menuScene);
	}
		
	public void flip() {
		foreach (GameObject stage in stages) {
			stage.GetComponentInChildren<Flipper> ().flip ();
		}
	}
}
