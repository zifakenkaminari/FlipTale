using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class CameraController : MonoBehaviour {
    public Player player;

    public float changePeriod;
    protected float startTime;
    protected bool isChanging;
	[SerializeField] protected Mask mask;

    void Start()
    {
        isChanging = false;
		StartCoroutine (begin ());

    }
    void Update(){
        
    }
    void FixedUpdate ()
    {
	/*	
        Vector3 now = transform.position;
        Vector3 pos = player.transform.position;
        Vector3 center = player.nowStage.transform.position;
        float viewLeft = player.nowStage.GetComponent<Stage>().viewLeft;
        float viewRight = player.nowStage.GetComponent<Stage>().viewRight;
        float viewUp = player.nowStage.GetComponent<Stage>().viewUp;
        float viewDown = player.nowStage.GetComponent<Stage>().viewDown;

        
        pos.x = Mathf.Clamp(pos.x, center.x - viewLeft, center.x + viewRight);
        pos.y = Mathf.Clamp(pos.y, center.y - viewDown, center.y + viewUp);
        
		//Lerp
        //now.x = Mathf.Lerp(now.x, pos.x, 0.1f);
        //now.y = Mathf.Lerp(now.y, pos.y, 0.1f);
        //Follow with player
        now.x = pos.x;
        now.y = pos.y;
        transform.position = now;
        */
    }
	void LateUpdate(){
		if (player != null) {
			Vector3 now = transform.position;
			Vector3 pos = player.transform.position;
			Vector3 center = player.nowStage.transform.position;
			float viewLeft = player.nowStage.GetComponent<Stage> ().viewLeft;
			float viewRight = player.nowStage.GetComponent<Stage> ().viewRight;
			float viewUp = player.nowStage.GetComponent<Stage> ().viewUp;
			float viewDown = player.nowStage.GetComponent<Stage> ().viewDown;


			pos.x = Mathf.Clamp (pos.x, center.x - viewLeft, center.x + viewRight);
			pos.y = Mathf.Clamp (pos.y, center.y - viewDown, center.y + viewUp);
			now.x = pos.x;
			now.y = pos.y;

			transform.position = now;
		}
	}

	public IEnumerator begin()
	{
		Manager.main.setFlippable (false);
		Manager.main.setPlayerControlable (false);
		StartCoroutine(mask.changeMaskColor (Color.white, new Color(1f, 1f, 1f, 0f), changePeriod));
		yield return StartCoroutine (fadeIn (changePeriod));
		player.unlockMotion();
		Manager.main.setPlayerControlable (true);
		Manager.main.setFlippable (true);
	}

	public IEnumerator end()
	{
		Manager.main.setFlippable (false);
		player.lockMotion();
		StartCoroutine(Mask.main.changeMaskColor (new Color(1f, 1f, 1f, 0f), Color.white, changePeriod));
		yield return StartCoroutine (fadeOut (changePeriod));
		yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene ("Ending");
    }

	protected IEnumerator fadeIn(float changePeriod){
		float timeNow = 0;
		while(timeNow<changePeriod){
			Camera.main.GetComponent<AudioSource> ().volume = timeNow/changePeriod;
			timeNow += Time.deltaTime;
			yield return null;
		}
	}

	protected IEnumerator fadeOut(float changePeriod){
        float timeNow = 0;
		while(timeNow<changePeriod){
			Camera.main.GetComponent<AudioSource> ().volume = 1 - timeNow/changePeriod;
            timeNow += Time.deltaTime;
            yield return null;
        }
    }

    protected void setTransparent(ref GameObject bg, float a) {
        Color tmpColor = bg.GetComponent<SpriteRenderer> ().color;
        tmpColor.a = a;
        bg.GetComponent<SpriteRenderer> ().color = tmpColor;
        return;
    }

}
