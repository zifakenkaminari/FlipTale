using UnityEngine;
using System.Collections;

public class Cloud : Entity {


    // Use this for initialization
    protected override void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == "PlainOneway")
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        base.Start();
    }

    public override IEnumerator flip()
    {
        if (face)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).name == "PlainOneway")
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).name == "PlainOneway")
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
        yield return base.flip();
    }
}
