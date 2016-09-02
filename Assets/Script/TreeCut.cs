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
        isCut = true;
        float beginTime = Time.time;
        Quaternion rotation = transform.rotation;
        Vector3 eular = rotation.eulerAngles;
        while (Time.time - beginTime < fallPeriod)
        {
            eular.z = 90 * Mathf.Cos((Time.time - beginTime) / fallPeriod * Mathf.PI / 2)-90;
            rotation.eulerAngles = eular;
            transform.rotation = rotation;
            yield return null;
        }
        eular.z = -90;
        rotation.eulerAngles = eular;
        transform.rotation = rotation;
        tag = "Floor";
    }

}
