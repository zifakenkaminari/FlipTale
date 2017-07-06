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
    public virtual void OnTriggerExit2D(Collider2D collider)

	{
        Player player = collider.GetComponent<Player>();
        if (player)
        {
            GameObject nxtStage = null;
            if (direction == 0)
            {                                   // horizontal
                if (player.transform.position.x > this.transform.position.x)
                {
                    nxtStage = (stage0.transform.position.x > stage1.transform.position.x) ? stage0 : stage1;
                }
                else
                {
                    nxtStage = (stage0.transform.position.x < stage1.transform.position.x) ? stage0 : stage1;
                }
            }
            else if (direction == 1)
            {                              // vertical
                if (player.transform.position.y > this.transform.position.y)
                {
                    nxtStage = (stage0.transform.position.y > stage1.transform.position.y) ? stage0 : stage1;
                }
                else
                {
                    nxtStage = (stage0.transform.position.y < stage1.transform.position.y) ? stage0 : stage1;
                }
            }
            if (player.nowStage != nxtStage)
            {
				StartCoroutine(switchScene(player, nxtStage, Vector3.zero));
            }
        }
        
    }

	public IEnumerator switchScene(Player player, GameObject nxtStage, Vector3 move)
	{
		Manager.main.setFlippable(false);
        player.lockMotion();
		Color transparnt = new Color(0f, 0f, 0f, 0f);
		Color blackout = new Color(0f, 0f, 0f, 1f);
		yield return StartCoroutine(Mask.main.changeMaskColor (transparnt, blackout, 0.20f));

        player.nowStage = nxtStage;
		player.transform.parent = player.nowStage.transform;
		player.teleport(player.transform.position + move);

		yield return StartCoroutine(Mask.main.changeMaskColor (blackout, transparnt, 0.20f));

		player.unlockMotion();
		Manager.main.setFlippable(true);
    }

    // 0: horizontal, 1: vertical
    public void setDirection(int dir) {
        this.direction = dir;
    }
    public int getDirection() {
        return this.direction;
    }
}
