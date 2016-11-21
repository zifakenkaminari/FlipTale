using UnityEngine;
using System.Collections;

public class LakeIce : Entity {

    protected override void Start ()
    {
        BoxCollider2D[] colliders = GetComponents<BoxCollider2D> ();
        foreach (BoxCollider2D collider in colliders) {
            collider.isTrigger = true;
        }
        base.Start ();
    }

    public override IEnumerator flip ()
    {
        if (face) {
            BoxCollider2D[] colliders = GetComponents<BoxCollider2D> ();
            foreach (BoxCollider2D collider in colliders) {
                collider.isTrigger = false;
            }
        }
        else {
            BoxCollider2D[] colliders = GetComponents<BoxCollider2D> ();
            foreach (BoxCollider2D collider in colliders) {
                collider.isTrigger = true;
            }
        }
        face = !face;
        yield return null;
    }
}
