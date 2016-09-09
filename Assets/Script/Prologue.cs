using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Prologue : MonoBehaviour {

    protected int idx;
    protected int countChangeBackground;
    public GameObject mainCamera;
    public float changePeriod;
    protected float startTime;
    protected bool isChanging;
    protected bool toChange;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild (i).gameObject.SetActive(false);
        }
        idx = 0;
        countChangeBackground = 6;
        isChanging = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isChanging && Input.GetKeyDown (KeyCode.Z)) {
            if (idx < transform.childCount)
            {
                if (toChange) {
                    startTime = Time.time;
                    isChanging = true;
                    toChange = false;
                }
                else { 
                    transform.GetChild(idx).gameObject.SetActive(true);
                    idx++;
                    if (idx == countChangeBackground) {
                        toChange = true;
                    }
                }                 
            }
            else {
                SceneManager.LoadScene ("Stage1");
            }
        }
	}

    void FixedUpdate(){
        if (isChanging) {
            if (Time.time - startTime < changePeriod) {
                mainCamera.GetComponent<Camera> ().backgroundColor = Color.white * (Time.time - startTime) / changePeriod;
            }
            else {
                mainCamera.GetComponent<Camera> ().backgroundColor = Color.white;
                isChanging = false;
            }
        }
    }
}
