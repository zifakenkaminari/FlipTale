using UnityEngine;
using System.Collections;

public class Paper : Item {

    public Sprite frontPaperCrumpled;
    public Sprite backPaperCrupmpled;
    public Sprite frontPapaerPlane;
    public Sprite backPaperPlane;
    public Sprite paperCrumpled;
    public Sprite paperPlane;
    public float destroyPeriod;
    protected GameObject mapDesign;
    protected int paperState; //0: normal, 1: crumpled, 2: plane
    protected bool isOnFloor;

    protected new void Start()
    {
        base.Start();
        isOnFloor = false;
        paperState = 0; // normal
    }
    public void magic() {
        paperState = 2;
        front.GetComponent<SpriteRenderer> ().sprite = paperPlane;
        back.GetComponent<SpriteRenderer> ().sprite = paperPlane;
    }

    public override void drop(GameObject player)
    {
        base.drop(player);
        pickable = false;
        StartCoroutine(disappear());
    }

    public IEnumerator disappear() {
        float timeNow = 0;
        while(timeNow < destroyPeriod){
            while (isFreezed) yield return null;
            setAlpha(1 - timeNow / destroyPeriod);
            timeNow += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

    public override bool use(GameObject player)
    {
        if (paperState == 0) {          
            //normal
            if (mapDesign == null || mapDesign.GetComponent<Entity>().face)
            {
                //become crumpled
                paperState = 1;         
                front.GetComponent<SpriteRenderer> ().sprite = paperCrumpled;
                back.GetComponent<SpriteRenderer> ().sprite = paperCrumpled;
                frontOnHand = frontPaperCrumpled;
                backOnHand = backPaperCrupmpled;
            }
            else
            {
                //become plane
                paperState = 2;     
                front.GetComponent<SpriteRenderer> ().sprite = paperPlane;
                back.GetComponent<SpriteRenderer> ().sprite = paperPlane;
                frontOnHand = frontPapaerPlane;
                backOnHand = backPaperPlane;
            }
            player.GetComponent<Player>().pickItem(this);
            base.use (player);
        }
        else if (paperState == 1)
        {
            //throw paper crumpled
            paperState = 3;
            player.GetComponent<Player>().dropItem();
            transform.parent = player.transform.parent;
            Vector3 scale = transform.localScale;
            scale.x = (player.GetComponent<Player>().front.GetComponent<SpriteRenderer>().flipX ^ face) ? 1 : -1;
            transform.localScale = scale;
            StartCoroutine(thrown(player));
        }
        else if (paperState == 2)
        {          
            //plane
            paperState = 3;
            player.GetComponent<Player>().dropItem();
            transform.parent = player.transform.parent;
            Vector3 scale = transform.localScale;
            scale.x = (player.GetComponent<Player>().front.GetComponent<SpriteRenderer>().flipX ^ face)?1:-1;
            transform.localScale = scale;
            StartCoroutine(fly(player));
        }
        return false;
    }


    protected IEnumerator thrown(GameObject player)
    {
        Vector3 scale = transform.localScale;
        //show image
        setAlpha(1);

        //fly physics
        float b = 0.2f;
        Vector3 v = new Vector3(5f * scale.x, 6f, 0f);
        Vector3 g = new Vector3(0, -10f, 0f);
        Vector3 eular = transform.localEulerAngles;
        while (!isOnFloor && front.GetComponent<SpriteRenderer>().isVisible)
        {
            while (isFreezed) yield return null;
            eular.z += -scale.x*360 * Time.deltaTime;
            transform.localEulerAngles = eular;
            transform.position += v * Time.deltaTime;
            v += g * Time.deltaTime;
            v -= b * v * Time.deltaTime;
            yield return null;
        }
        StartCoroutine(disappear());
    }

    protected IEnumerator fly(GameObject player)
    {
        setAlpha(1);
        //fly physics
        Vector3 scale = transform.localScale;
        float b = 0.6f;
        Vector3 v = new Vector3(9f * scale.x, 6f, 0f);
        Vector3 g = new Vector3(0, -1f, 0f);
        Vector3 eular = transform.localEulerAngles;
        while (front.GetComponent<SpriteRenderer>().isVisible)
        {
            while (isFreezed) yield return null;
            eular.z = Mathf.Atan(v.y/v.x)*180/Mathf.PI-15*scale.x;
            transform.localEulerAngles = eular;
            transform.position += v * Time.deltaTime;
            v += g * Time.deltaTime;
            v -= b * new Vector3(0, v.y, 0) * Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);

    }

    public int getPaperState() {
        return paperState;
    }
 
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "MapDesign")
        {
            mapDesign = collider.gameObject;
        }
        if (collider.gameObject.CompareTag("Floor"))
        {
            isOnFloor = true;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (mapDesign && collider.gameObject == mapDesign)
        {
            mapDesign = null;
        }
        if (collider.gameObject.CompareTag("Floor"))
        {
            isOnFloor = false;
        }
    }
}
