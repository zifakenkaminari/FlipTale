using UnityEngine;
using System.Collections;

public class HouseMushrrom : Machine {
    public GameObject stage;
    public override void use (GameObject player)
    {
        player.GetComponent<Player>().nowStage = stage;
        float moveY = stage.transform.position.y - player.transform.parent.position.y;
        player.transform.parent = stage.transform;
        Vector3 pos = player.transform.position;
        pos.y += moveY;
        player.transform.position = pos;
    } 
}
