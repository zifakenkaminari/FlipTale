using UnityEngine;
using System.Collections;

public class StageTrigger : MonoBehaviour {

    public GameObject stage0;
    public GameObject stage1;
    public float autoMovePath;

    // Use this for initialization
    void Start () {
    }

    //change stage
    void OnTriggerEnter2D(Collider2D collider) {
        Player player = collider.GetComponent<Player> ();
        if (player) {
            if (player.nowStage == stage0) {
                player.nowStage = stage1;
                player.transform.parent = stage1.transform;
            }
            else {
                player.nowStage = stage0;
                player.transform.parent = stage0.transform;
            }
            Vector3 pos = player.transform.position;
            pos.x += ((player.nowStage.transform.position.x > pos.x)?1:-1) * autoMovePath;
            player.transform.position = pos;
        }
    }
}
