using UnityEngine;
using System.Collections;

public class BalloonBlower : Machine {
    public GameObject balloon;
    public override void use(GameObject player)
    {
        GameObject newBalloon = (GameObject)Instantiate(balloon, transform);
        newBalloon.transform.localPosition = Vector3.zero;
    }
}
