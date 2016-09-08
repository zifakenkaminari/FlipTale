using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public Player player;

	void FixedUpdate () {
        Vector3 pos = transform.position;
		pos.x = Mathf.Lerp(pos.x, player.nowStage.transform.position.x, 0.1f);
        pos.y = Mathf.Lerp(pos.y, player.nowStage.transform.position.y, 0.1f);
        transform.position = pos;
	}
}
