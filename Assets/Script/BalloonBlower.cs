using UnityEngine;
using System.Collections;

public class BalloonBlower : Machine {
    public GameObject balloon;
    public override void use(GameObject player)
    {
        GameObject newBalloon = (GameObject)Instantiate(balloon, transform.parent);
        newBalloon.transform.position = transform.position;
    }
}
