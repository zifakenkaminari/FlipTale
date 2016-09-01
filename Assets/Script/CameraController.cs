using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public GameObject player;

	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
    void Update()
    {
    }
	void FixedUpdate () {
        Vector3 pos = transform.position;
		pos.x = Mathf.Lerp(pos.x, player.GetComponent<Player>().nowStage.transform.position.x, 0.1f);
        transform.position = pos;
	}
}
