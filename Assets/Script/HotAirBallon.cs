using UnityEngine;
using System.Collections;

public class HotAirBallon : Entity {

    protected int getItemCount;
    protected GameObject[] getItems;

    protected override void Start ()
    {
        getItemCount = 0;
        getItems = new GameObject [3];
        front.SetActive (false);
        back.SetActive (false);
        base.Start ();
    }

    public void getItem(GameObject item) {
        item.transform.parent = transform;
        item.GetComponent<Item> ().pickable = false;
        getItems [getItemCount] = item;
        getItemCount++;
        if (getItemCount == 3) {
            front.SetActive (true);
            back.SetActive (false);
            foreach(GameObject eachItem in getItems) {
                eachItem.SetActive (false);
            }
        }
    }
}
