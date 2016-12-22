using UnityEngine;
using System.Collections;
using System;

public class SpikeRemover : Entity {
    public GameObject spikes;
    bool isUsed;

    protected override void Start()
    {
        base.Start();
        isUsed = false;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (!isUsed && !isFreezed && collider.gameObject.GetComponent<Balloon>()) {
            isUsed = true;
            Destroy(collider.gameObject);
            StartCoroutine(removeSpike());
        }
    }

    protected IEnumerator removeSpike() {
        Spike[] allSpike = spikes.GetComponentsInChildren<Spike>();
        Array.Sort(allSpike, delegate (Spike a, Spike b) {
            return a.transform.position.x.CompareTo(b.transform.position.x);
        });
        foreach (Spike spike in allSpike)
        {
            StartCoroutine(spike.disappear());
            yield return new WaitForSeconds(0.1f);
        }

    }



}
