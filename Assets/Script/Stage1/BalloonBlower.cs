using UnityEngine;
using System.Collections;

public class BalloonBlower : Machine {
    public GameObject balloon;
    public override void use(GameObject player)
    {
        GameObject newBalloon = (GameObject)Instantiate(balloon, transform);
        newBalloon.transform.localPosition = new Vector3(0, 0.7f, 0);
    }
}
