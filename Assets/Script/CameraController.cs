using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class CameraController : MonoBehaviour {
    public Player player;

    public float changePeriod;
    protected float startTime;
    protected bool isChanging;
    protected bool toChange;

    void Start()
    {
        isChanging = false;
    }
    void FixedUpdate ()
    {
        Vector3 now = transform.position;
        Vector3 pos = player.transform.position;
        Vector3 center = player.nowStage.transform.position;
        float viewLeft = player.nowStage.GetComponent<Stage>().viewLeft;
        float viewRight = player.nowStage.GetComponent<Stage>().viewRight;
        float viewUp = player.nowStage.GetComponent<Stage>().viewUp;
        float viewDown = player.nowStage.GetComponent<Stage>().viewDown;
        if (pos.x > center.x + viewLeft) pos.x = center.x + viewLeft;
        if (pos.x < center.x - viewRight) pos.x = center.x - viewRight;
        if (pos.y > center.y + viewUp) pos.y = center.y + viewUp;
        if (pos.y < center.y - viewDown) pos.y = center.y - viewDown;

        now.x = Mathf.Lerp(now.x, pos.x, 0.1f);
        now.y = Mathf.Lerp(now.y, pos.y, 0.1f);
        transform.position = now;

        if (isChanging)
        {
            if (Time.time - startTime < changePeriod)
            {
                GetComponent<Camera>().backgroundColor = Color.white * (Time.time - startTime) / changePeriod;
            }
            else
            {
                GetComponent<Camera>().backgroundColor = Color.white;
                isChanging = false;
                SceneManager.LoadScene("Ending");
            }
        }
    }
    public void end()
    {
        startTime = Time.time;
        isChanging = true;
        toChange = false;
    }



}
