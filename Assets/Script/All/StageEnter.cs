using UnityEngine;
using System.Collections;

public class StageEnter : MonoBehaviour {

    public GameObject stageEnter;
    public float moveX;
    public float moveY;
    [HideInInspector]
    public bool[] canEnter = new bool[2];

    public void enter(GameObject playerObject) {
        Player player = playerObject.GetComponent<Player>();
        player.nowStage = stageEnter;
        player.transform.parent = stageEnter.transform;
        player.transform.position = stageEnter.transform.position + new Vector3(moveX, moveY, 0);
        if (player.itemOnHand)
            player.itemOnHand.transform.position = player.transform.position;
    }
}
