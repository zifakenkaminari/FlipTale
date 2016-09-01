using UnityEngine;
using System.Collections;

public class FlagAxe : Item {
	public GameObject treeCut;

	protected new void Start() {
		base.Start ();
		treeCut = null;
		pickable = true;
	}

    public override void use(GameObject player)
    {
        Debug.Log("Try cut");
		if(treeCut&&!face) {
            Debug.Log("Cut");
			StartCoroutine(treeCut.GetComponent<TreeCut>().cut());
			treeCut = null;
		}
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
