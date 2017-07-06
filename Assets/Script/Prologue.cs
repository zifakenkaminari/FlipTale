using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Prologue : MonoBehaviour {

	/*
	public enum ActionType{
		ACTION_PLAY,
		ACTION_FADE
	}
	protected class Action{
		public ActionType type;		//0: image, 1: crossfade 
		public T arg;		//0: image, 1: nextcolor
		public float duration;
		public bool autoplay;	//if set to true, this action will be executed automatically
		public Action(ActionType type, T arg, float duration, bool autoplay){
			this.type = type;
			this.arg = arg;
			this.duration = duration;
			this.autoplay = autoplay;
		}
	}

	Queue q;
	*/
    protected int idx;
    protected int countChangeBackground;
    public float changePeriod;
    protected float startTime;
    protected bool isChanging;
    protected bool toChange;
	// Use this for initialization

	protected Image[] texts;

	void Start () {
		texts = GetComponentsInChildren<Image> ();
		foreach (Image text in texts) {
			text.gameObject.SetActive(false);
		}
		Debug.Log (texts.Length);
        idx = 0;
        countChangeBackground = 6;
        isChanging = false;
		/*
		q = new Queue();
		q.Enqueue(new Action<GameObject>(ActionType.ACTION_PLAY, texts[0], 0, true));
		q.Enqueue(new Action<GameObject>(ActionType.ACTION_PLAY, texts[1], 0, false));
		q.Enqueue(new Action<GameObject>(ActionType.ACTION_PLAY, texts[2], 0, false));
		q.Enqueue(new Action<GameObject>(ActionType.ACTION_PLAY, texts[3], 0, false));
		q.Enqueue(new Action<GameObject>(ActionType.ACTION_PLAY, texts[4], 0, false));
		q.Enqueue(new Action<GameObject>(ActionType.ACTION_PLAY, texts[5], 0, false));
		q.Enqueue(new Action<Color>(ActionType.ACTION_FADE, Color.white, 1.5f, false));
		q.Enqueue(new Action<GameObject>(ActionType.ACTION_PLAY, texts[6], 0, true));
		q.Enqueue(new Action<GameObject>(ActionType.ACTION_PLAY, texts[7], 0, false));
		q.Enqueue(new Action<GameObject>(ActionType.ACTION_PLAY, texts[8], 0, false));
		q.Enqueue(new Action<GameObject>(ActionType.ACTION_PLAY, texts[9], 0, false));
		StartCoroutine (actionLoop ());
		*/
	}
	/*
	IEnumerator actionLoop(){
		while (q.Peek () != null) {
			Action<> action = q.Dequeue ();
			if (action.autoplay) {
				yield return new WaitUntil (() => Input.GetKeyDown (KeyCode.Z));
			}
			switch (action.type) {
			case ActionType.ACTION_PLAY:
				GameObject obj = (GameObject)action.arg;
				obj.SetActive (true);
				//((GameObject)action.arg).SetActive (true);
				yield return new WaitForSeconds (action.duration);
				break;
			case ActionType.ACTION_FADE:
				yield return Mask.main.changeMaskColor ((Color)action.arg, action.duration);
				break;
			}
		}
	}
	*/
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Z) && !isChanging) {
			if (idx < texts.Length) {
				if (toChange) {
					startTime = Time.time;
					isChanging = true;
					toChange = false;
				} else { 
					texts [idx].gameObject.SetActive (true);
					idx++;
					if (idx == countChangeBackground) {
						toChange = true;
					}
				}                 
			} else {
				StartCoroutine (fadeOut ());
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
				texts[idx].gameObject.SetActive(true);
				idx++;
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
