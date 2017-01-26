using UnityEngine;
using System.Collections;

public class StageTrigger_Gap : StageTrigger {

    [SerializeField]
    protected float gap;

    public void OnTriggerEnter2D(Collider2D collider) {
		Player player = collider.GetComponent<Player>();
		if (player)
		{
			GameObject nxtStage = null;
			if (direction == 0)
			{                                   // horizontal
				if (player.transform.position.x < this.transform.position.x)
				{
					nxtStage = (stage0.transform.position.x > stage1.transform.position.x) ? stage0 : stage1;
					StartCoroutine(switchScene(player, nxtStage, gap * Vector3.right));
				}
				else
				{
					nxtStage = (stage0.transform.position.x < stage1.transform.position.x) ? stage0 : stage1;
					StartCoroutine(switchScene(player, nxtStage, gap * -Vector3.right));
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
				StartCoroutine(switchScene(player, nxtStage, Vector3.zero));
			}
		}
    }

	public override void OnTriggerExit2D(Collider2D collider)
	{
		//override. Do nothing.
	}
}
