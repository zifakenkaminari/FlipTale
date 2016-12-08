using UnityEngine;
using System.Collections;

public class StageTrigger : MonoBehaviour {

    public GameObject stage0;
    public GameObject stage1;
    [SerializeField]
    [HideInInspector]
    protected int direction;         // 0: horizontal, 1: vertical

    // Use this for initialization
    void Start () {
    }

    //change stage
    public void OnTriggerExit2D(Collider2D collider) {
        Player player = collider.GetComponent<Player> ();
        if (player) {
            
            if (direction == 0) {                                   // horizontal
                if (player.transform.position.x > this.transform.position.x) {
                    player.nowStage = (stage0.transform.position.x > stage1.transform.position.x) ? stage0 : stage1;
                } else {
                    player.nowStage = (stage0.transform.position.x < stage1.transform.position.x) ? stage0 : stage1;
                }
            }
            else if (direction == 1) {                              // vertical
                if (player.transform.position.y > this.transform.position.y) {
                    player.nowStage = (stage0.transform.position.y > stage1.transform.position.y) ? stage0 : stage1;
                } else {
                    player.nowStage = (stage0.transform.position.y < stage1.transform.position.y) ? stage0 : stage1;
                }
            }
            player.transform.parent = player.nowStage.transform;
        }
    }

    // 0: horizontal, 1: vertical
    public void setDirection(int dir) {
        this.direction = dir;
    }
    public int getDirection() {
        return this.direction;
    }
}
