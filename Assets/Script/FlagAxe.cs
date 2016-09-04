using UnityEngine;
using System.Collections;

public class FlagAxe : Item {
	public GameObject treeCut;

	protected new void Start() {
		base.Start ();
		treeCut = null;
		pickable = true;
	}

    public override bool use(GameObject player)
    {
		if(treeCut&&!face) {
			StartCoroutine(treeCut.GetComponent<TreeCut>().cut());
			treeCut = null;
		}
        return false;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.gameObject.CompareTag("TreeCut") && !collider.gameObject.GetComponent<TreeCut>().isCut) {
			treeCut = collider.gameObject;
		}
	}
	void OnTriggerExit2D(Collider2D collider) {
		if (collider.gameObject.CompareTag ("TreeCut")) {
			treeCut = null;
		}
	}
}
