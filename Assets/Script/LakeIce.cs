using UnityEngine;
using System.Collections;

public class LakeIce : Entity {

    protected override void Start ()
    {
        GetComponent<BoxCollider2D> ().isTrigger = true;
        base.Start ();
    }

    public override IEnumerator flip ()
    {
        if (face) {
            GetComponent<BoxCollider2D> ().isTrigger = false;
        }
        else {
            GetComponent<BoxCollider2D> ().isTrigger = true;
        }
        return base.flip ();
    }
}
