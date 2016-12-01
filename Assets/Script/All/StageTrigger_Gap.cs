using UnityEngine;
using System.Collections;

public class StageTrigger_Gap : StageTrigger {

    [SerializeField]
    protected float gap;

    public void OnTriggerEnter2D(Collider2D collider) {
        Player player = collider.GetComponent<Player> ();
        if (player) {

            if (direction == 0) {                                   // horizontal
                if (player.transform.position.x < this.transform.position.x) {
                    player.nowStage = (stage0.transform.position.x > stage1.transform.position.x) ? stage0 : stage1;
                    Vector3 tmpPos = player.transform.position;
                    tmpPos += new Vector3 (gap, 0, 0);
                    player.transform.position = tmpPos;
                } else {
                    player.nowStage = (stage0.transform.position.x < stage1.transform.position.x) ? stage0 : stage1;
                    Vector3 tmpPos = player.transform.position;
                    tmpPos += new Vector3 (-gap, 0, 0);
                    player.transform.position = tmpPos;
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
}
