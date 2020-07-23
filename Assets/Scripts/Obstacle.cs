using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
	public GameObject crashFX;
	public GameObject flowerFX;
	public bool isFlower;
	public void OnHit()
	{
		if(!isFlower){
			Destroy(GameObject.Instantiate(crashFX,transform.position,Quaternion.identity),3f);
			AudioController.Play("Brickwall");
		}
		else
		{
			Destroy(GameObject.Instantiate(flowerFX,transform.position,Quaternion.identity),3f);
			AudioController.Play("Chewing");
		}
		ObstacleSpawner.Instance.ReturnObstacleToPool(transform);

	}
	void Update()
	{
		if(transform.position.x < 0f)
		{
			ObstacleSpawner.Instance.ReturnObstacleToPool(transform);
		}
	}
}
