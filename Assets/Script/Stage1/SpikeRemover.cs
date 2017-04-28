using UnityEngine;
using System.Collections;
using System;

public class SpikeRemover : MonoBehaviour{
	[SerializeField]	protected Sprite pressed;
    public GameObject spikes;
    bool isUsed;


    protected void Start()
    {
        isUsed = false;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (!isUsed && collider.gameObject.GetComponent<Balloon>()) {
            isUsed = true;
            Destroy(collider.gameObject);
            StartCoroutine(removeSpike());
        }
    }

    protected IEnumerator removeSpike() {
		GetComponent<SpriteRenderer> ().sprite = pressed;
		Manager.main.setFlippable(false);
		Manager.main.setPlayerControlable(false);

        Spike[] allSpike = spikes.GetComponentsInChildren<Spike>();
        Array.Sort(allSpike, delegate (Spike a, Spike b) {
            return a.transform.position.x.CompareTo(b.transform.position.x);
        });
        foreach (Spike spike in allSpike)
        {
            StartCoroutine(spike.disappear());
            yield return new WaitForSeconds(0.05f);
        }

		Manager.main.setPlayerControlable(true);
		Manager.main.setFlippable(true);

    }



}
