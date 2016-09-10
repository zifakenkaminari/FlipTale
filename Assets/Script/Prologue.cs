using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Prologue : MonoBehaviour {

    protected int idx;
    protected int countChangeBackground;
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
                StartCoroutine (fadeOut());
            }
        }
	}

    void FixedUpdate(){
        if (isChanging) {
            if (Time.time - startTime < changePeriod) {
                Camera.main.backgroundColor = Color.white * (Time.time - startTime) / changePeriod;
            }
            else {
                Camera.main.backgroundColor = Color.white;
                isChanging = false;
            }
        }
    }

    protected IEnumerator fadeOut(){
        float p = 2;
        float timeNow = 0;
        while(timeNow<p){
            Camera.main.GetComponent<AudioSource> ().volume = 1-timeNow / p;
            timeNow += Time.deltaTime;
            yield return null;
        }


        SceneManager.LoadScene ("Stage1");
        
    }
}
