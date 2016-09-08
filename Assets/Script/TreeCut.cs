using UnityEngine;
using System.Collections;

public class TreeCut : Entity
{
    public float fallPeriod;
    public bool isCut;

    new void Start(){
        isCut = false;
        base.Start();
    }

    public IEnumerator cut()
    {
        if (isCut) yield break;
        isCut = true;
        float timeNow = 0;
        Vector3 eular = transform.localEulerAngles;
        while (timeNow < fallPeriod)
        {
            while(isFreezed) yield return null;
            eular.z = 90 * Mathf.Cos(timeNow / fallPeriod * Mathf.PI / 2)-90;
            transform.localEulerAngles = eular;
            timeNow += Time.deltaTime;
            yield return null;
        }
        eular.z = -90;
        transform.localEulerAngles = eular;
        tag = "Floor";
    }

}
