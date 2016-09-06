using UnityEngine;
using System.Collections;

public class StoneButton : Entity
{
    GameObject player;
    public Totem totem;
    public CaveExit caveExit;

    protected override void Start(){
        base.Start();

    }

    protected void Update()
    {
        if (player && Input.GetKeyDown(KeyCode.C))
        {
            use(player);
        }
    }

    public void use(GameObject player)
    {
        if (totem.state == 1 && !caveExit.isOpen) {
            StartCoroutine(caveExit.open());
        }
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            player = collider.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (player && collider.gameObject == player)
        {
            player = null;
        }
    }

}
