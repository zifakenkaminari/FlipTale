using UnityEngine;
using System.Collections;

public class Tomb : Entity {

    public Sprite frontWithFlower;
    public Sprite backWithFlower;
    public bool hasFlower;
    protected override void Start ()
    {
        hasFlower = false;
        base.Start ();
    }

}
