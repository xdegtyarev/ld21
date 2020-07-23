using UnityEngine;
using System.Collections.Generic;

public class Layer : MonoBehaviour {
	public float time;
	public ObjectPool pool;
	public float depth;
	public Vector3 offset;
	List<Transform> chunks;
	public bool AttachToScreenWidth;
	public bool AttachToScreenHeight;
	public ObstacleSpawner spawner;
	bool isStart;
	void Start () {
		isStart = true;
		if(AttachToScreenWidth)
		{
			transform.position = Vector3.right * Screen.width;
		}
		if(AttachToScreenHeight)
		{
			transform.position = Vector3.up * Screen.height;
		}
		transform.position += offset;
		transform.position += Vector3.forward * depth;

		chunks = new List<Transform>();

		do{
			AddChunk();
		}while(chunks[chunks.Count-1].localPosition.x > Screen.width);
		isStart = false;
	}

	void AddChunk()
	{
		var t = pool.GetObject();
		t.parent = transform;
		float x = 0;
		if(chunks.Count>0)
		{
			x = chunks[chunks.Count-1].localPosition.x + chunks[chunks.Count-1].GetComponent<tk2dSprite>().GetBounds().size.x;
		}
		t.localPosition = Vector3.right * x;
		chunks.Add(t);
		if(!isStart){
			if(spawner != null)
			{
				spawner.OnChunkAdded(t);
			}
		}
	}

	void Update()
	{
		Vector3 delta = Vector3.left * Screen.width/time * Time.deltaTime * Llama.Instance.speed;
		for(int i = 0; i<chunks.Count; i++)
		{
			chunks[i].localPosition = chunks[i].localPosition + delta;
			if(chunks[i].localPosition.x + chunks[i].GetComponent<tk2dSprite>().GetBounds().size.x <= -Screen.width)
			{
				pool.ReturnToPool(chunks[i]);
				chunks.RemoveAt(i);
			}
			if(i == chunks.Count-1)
			{
				if(chunks[i].localPosition.x < Screen.width){
					AddChunk();
					break;
				}
			}
		}
			
	}
}
