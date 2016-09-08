using UnityEngine;
using System.Collections;

public class Ladder : Machine {
    //state = 0
    public Sprite frontStep1;
    public Sprite backStep1;
    public Sprite frontStep2;
    public Sprite backStep2;
    public Sprite frontStep3;
    public Sprite backStep3;
    int state;
    public void build() {
        if (state == 0) {
            front.GetComponent<SpriteRenderer>().sprite = frontStep1;
            back.GetComponent<SpriteRenderer>().sprite = backStep1;
        }
        else if (state == 1)
        {
            front.GetComponent<SpriteRenderer>().sprite = frontStep2;
            back.GetComponent<SpriteRenderer>().sprite = backStep2;
        }
        else if (state == 2)
        {
            front.GetComponent<SpriteRenderer>().sprite = frontStep3;
            back.GetComponent<SpriteRenderer>().sprite = backStep3;
        }
        state++;
    }

    public override void use(GameObject player)
    {
        if (state >= 3)
        { 
            
        }

    }
	
}
