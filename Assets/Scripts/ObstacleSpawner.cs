using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour {
	public ObjectPool obstaclePool;
	public float spawnChance = 0.5f;
	public static ObstacleSpawner Instance;
	public void Awake()
	{
		Instance = this;
	}

	public void ReturnObstacleToPool(Transform t)
	{
		obstaclePool.ReturnToPool(t);
	}

	public void OnChunkAdded(Transform t)
	{
		if(Llama.Instance.isRunning){
			if(Random.Range(0f,1f) < spawnChance)
			{
				var obstacle = obstaclePool.GetObject();
				obstacle.parent = t;
				obstacle.localPosition = Vector3.right * Random.Range(0,t.GetComponent<tk2dSprite>().GetBounds().size.x);
				obstacle.localPosition = Vector3.up * (t.GetComponent<tk2dSprite>().GetBounds().size.y - 20f);
			}
		}
	}
}
