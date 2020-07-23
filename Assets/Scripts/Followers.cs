using UnityEngine;
using System.Collections;

public class Followers : MonoBehaviour,ITriggerable {
	public int hp;

	public void OnTriggerEnter (Collider collider)
	{
		var obstacle = collider.GetComponent<Obstacle>();
		if(obstacle.isFlower){
			hp++;
		}else{
			hp--;
			if(hp <= 0)
			{
				Die();
			}
		}
		obstacle.OnHit();
	}

	public void Die(){
		iTween.MoveTo(gameObject,transform.position + Vector3.left * 300,1f);
	}
}
