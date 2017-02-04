using UnityEngine;
using System.Collections;

public class StageEnter : MonoBehaviour {

    public GameObject stageEnter;
    public float moveX;
    public float moveY;
    [HideInInspector]
    public bool[] canEnter = new bool[2];

    public void enter(GameObject playerObject) {
		StartCoroutine (switchScene(playerObject.GetComponent<Player>(), stageEnter, new Vector3(moveX, moveY, 0)));
    }

	public IEnumerator switchScene(Player player, GameObject nxtStage, Vector3 move)
	{
		Manager.main.setFlippable(false);
		player.lockMotion();
		Color transparnt = new Color(0f, 0f, 0f, 0f);
		Color blackout = new Color(0f, 0f, 0f, 1f);
		yield return StartCoroutine(Camera.main.GetComponent<CameraController> ().changeMaskColor (transparnt, blackout, 0.20f));

		player.nowStage = stageEnter;
		player.transform.parent = stageEnter.transform;
		player.teleport(stageEnter.transform.position + move);

		yield return StartCoroutine(Camera.main.GetComponent<CameraController> ().changeMaskColor (blackout, transparnt, 0.20f));

		player.unlockMotion();
		Manager.main.setFlippable(true);
	}
}
