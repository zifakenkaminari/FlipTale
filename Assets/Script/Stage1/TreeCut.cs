using UnityEngine;
using System.Collections;

public class TreeCut : Entity
{
    public float fallPeriod;
    public bool isCut;
    public Sprite[] cutProgressFront;
    public Sprite[] cutProgressBack;
    protected int idx;

    new void Start(){
        isCut = false;
        idx = 0;
        base.Start();
    }

    public IEnumerator cut()
    {
        if (isCut) yield break;
        if (idx < cutProgressFront.Length) {
            front.GetComponent<SpriteRenderer> ().sprite = cutProgressFront[idx];
            back.GetComponent<SpriteRenderer> ().sprite = cutProgressBack[idx];
            idx++;
        }
        else {
            isCut = true;
            GameObject treeCutTop = transform.FindChild ("TreeCutTop").gameObject;
            GameObject treeCutBottom = transform.FindChild ("TreeCutBottom").gameObject;

            treeCutTop.SetActive (true);
            treeCutTop.transform.SetParent (transform.parent);

            treeCutBottom.SetActive (true);
            treeCutBottom.transform.SetParent (transform.parent);

            treeCutTop.GetComponent<TreeCutTop> ().cutDown ();
            Destroy (this.gameObject);
        }
    }

}
