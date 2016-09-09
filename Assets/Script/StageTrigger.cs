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
    public virtual void OnTriggerEnter2D(Collider2D collider) {
        Player player = collider.GetComponent<Player> ();
        if (player) {
            float moveY;
            if (player.nowStage == stage0) {
                player.nowStage = stage1;
                player.transform.parent = stage1.transform;
            }
            else {
                player.nowStage = stage0;
                player.transform.parent = stage0.transform;
            }
            Vector3 pos = player.transform.position;
            pos.x += ((player.nowStage.transform.position.x > pos.x)?1:-1) * autoMovePath.x;
            pos.y += ((player.nowStage.transform.position.y > pos.y)?1:-1) * autoMovePath.y;
            player.transform.position = pos;
        }
    }
}
