using UnityEngine;
using System.Collections;

public class StoneButton : Machine
{
    public Puller puller;
    public CaveExit caveExit;

    public override void use(GameObject player)
    {
        if (puller.state == 3 && !caveExit.isOpen) {
            StartCoroutine(caveExit.open());
        }
    }
}
