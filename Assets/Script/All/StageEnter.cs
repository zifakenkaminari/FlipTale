using UnityEngine;
using System.Collections;

public class StageEnter : MonoBehaviour {

    public GameObject stageEnter;
    public float moveX;
    public float moveY;
    [HideInInspector]
    public bool[] canEnter = new bool[2];

    public void enter(GameObject player) {
        player.GetComponent<Player> ().nowStage = stageEnter;
        player.transform.parent = stageEnter.transform;

        Vector3 pos = this.stageEnter.transform.position;
        pos.x += moveX;
        pos.y += moveY;
        player.transform.position = pos;
    }
}
