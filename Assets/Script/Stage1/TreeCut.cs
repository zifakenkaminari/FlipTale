using UnityEngine;
using System.Collections;

public class TreeCut : Entity
{
	[SerializeField] protected GameObject treeCutTop;
	[SerializeField] protected GameObject treeCutBottom;
	[SerializeField] protected AudioSource treeCuttingSound;
	[SerializeField] protected AudioSource treeFallingSound; 
    public float fallPeriod;
    public bool isCut;
    public Sprite[] cutProgressFront;
    public Sprite[] cutProgressBack;
    protected int idx;

	protected override void Start(){
		base.Start();
		treeCutTop.GetComponent<Entity> ().setAlpha(0);
		treeCutBottom.GetComponent<Entity> ().setAlpha(0);
		isCut = false;
		idx = 0;
    }

    public IEnumerator cut()
    {
        if (isCut) yield break;
		if (idx < cutProgressFront.Length) {
			treeCuttingSound.Play ();
			setSprite (cutProgressFront [idx], cutProgressBack [idx]);
            idx++;
        }
		else {
			
            isCut = true;

			treeFallingSound.Play ();
			treeCutTop.GetComponent<Entity> ().setAlpha(1);
			treeCutBottom.GetComponent<Entity> ().setAlpha(1);
			treeCutTop.GetComponent<TreeCutTop> ().cutDown ();
            treeCutTop.tag = "Floor";
            treeCutBottom.tag = "Floor";

			setAlpha(0);
			GetComponent<Collider2D> ().enabled = false;
        }
    }

}
