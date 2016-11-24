using UnityEngine;
using System.Collections;

public class StageTrigger : MonoBehaviour {

    public GameObject stage0;
    public GameObject stage1;
    public Vector2 autoMovePath;

    // Use this for initialization
    void Start () {
    }

    //change stage
    public void OnTriggerExit2D(Collider2D collider) {
        Player player = collider.GetComponent<Player> ();
        if (player) {
            if (player.transform.position.x > this.transform.position.x) {
                player.nowStage = (stage0.transform.position.x > stage1.transform.position.x) ? stage0 : stage1;
            }
            else {
                player.nowStage = (stage0.transform.position.x < stage1.transform.position.x) ? stage0 : stage1;
            }
        }
    }
}
