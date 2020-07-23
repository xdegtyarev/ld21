using UnityEngine;
using System.Collections;

public interface ITriggerable
{
	void OnTriggerEnter(Collider collider);
}

public class Trigger : MonoBehaviour {
	public GameObject target;
	public ITriggerable trg;
	void Awake(){
		trg = target.GetComponent(typeof(ITriggerable)) as ITriggerable;
	}
	void OnTriggerEnter (Collider collider)
	{
		trg.OnTriggerEnter (collider);
	}
}
