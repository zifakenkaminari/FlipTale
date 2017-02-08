using UnityEngine;
using System.Collections;

public class Machine : Entity{
	protected bool usable;

    public virtual void use(GameObject player)
    {
        //TODO: activate machine
    }

	public virtual void setUsable(bool usable){
		this.usable = usable;
	}

	public virtual bool isUsable(){
		return usable;
	} 
}
