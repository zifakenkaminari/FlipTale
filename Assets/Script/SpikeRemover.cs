using UnityEngine;
using System.Collections;

public class SpikeRemover : Entity {

    public GameObject spikes;

    void OnTriggerStay2D(Collider2D collider)
    {
        if (!isFreezed && collider.gameObject.CompareTag("Balloon")) {
            Destroy(collider.gameObject);
            foreach (Transform spikeTrans in spikes.transform) {
                Destroy(spikeTrans.gameObject);
            }
        }
    }



}
