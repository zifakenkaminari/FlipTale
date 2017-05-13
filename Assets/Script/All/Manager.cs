using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

    public static Manager main;

    public string loadScene;
    public string menuScene;
    public GameObject GM_jump;
    public bool GM_mode;
    public GameObject[] stages;
    protected Player player;
	protected bool flippable;

    // Use this for initialization
    void Start () {
        main = this;
		GameObject playerObj = GameObject.Find ("Player");
		if (playerObj != null) {
			player = playerObj.GetComponent<Player>();
		}
		flippable = true;
    }

    // Update is called once per frame
    void Update () {
		if(Input.GetKeyDown(KeyCode.Z) && flippable){
            foreach (GameObject stage in stages)
            {
                if (stage.GetComponentInChildren<Stage>().isFlipping) 
                    return;
            }
            flip();
        }
        if (Input.GetKeyDown (KeyCode.F8) && GM_mode) {
            if (!GM_jump)
                return;
            if (GM_jump.activeSelf == false) {
                GM_jump.SetActive (true);
            } 
            else {
                GM_jump.SetActive (false);
            }
        }
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
            StartCoroutine(stage.GetComponentInChildren<Stage>().flip());
        }
    }

    public void jumpPlayer(){
        print("Stage1_" + GM_jump.GetComponent<InputField> ().text);
        GameObject jumpStage = GameObject.Find("Stage1_" + GM_jump.GetComponent<InputField>().text);
        if (jumpStage != null)
        {
            player.nowStage = jumpStage;
            player.transform.parent = jumpStage.transform;
            player.teleport(jumpStage.transform.position);
        }
    }

    public void lockMotion()
    {
        foreach (GameObject stage in stages) 
        {
            stage.GetComponent<Stage>().lockMotion();
        }
    }

    public void unlockMotion()
    {
        foreach (GameObject stage in stages) 
        {
            stage.GetComponent<Stage>().unlockMotion();
        }
    }


    public void setPlayerControlable(bool controlable)
    {
        player.setControlable(controlable);
    }

	public void setFlippable(bool flippable){
		this.flippable = flippable;
	}
	public bool isFlippable(){
		return flippable;
	}
}
