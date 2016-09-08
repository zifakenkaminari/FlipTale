using UnityEngine;
using System.Collections;

public class Spike : Entity {
    void OnTriggerStay2D(Collider2D collider) {
        if (!isFreezed && collider.gameObject.CompareTag("Balloon")) {
            Destroy(collider.gameObject);
        }
    }
	

}
