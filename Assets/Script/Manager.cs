﻿using UnityEngine;
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
        if(Input.GetKeyDown(KeyCode.Z)){
            foreach (GameObject stage in stages)
            {
                if (stage.GetComponentInChildren<Flipper>().isFlipping) 
                    return;
            }
            flip();
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
            StartCoroutine(stage.GetComponentInChildren<Flipper>().flip());
        }
    }
}
