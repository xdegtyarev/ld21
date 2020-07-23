using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public class ObjectPoolInfo
{
	[SerializeField]
	public string Name;
	[SerializeField]
	public ObjectPool Pool;
}
public class PoolManager : MonoBehaviour {
	public ObjectPoolInfo[] Pools;
	public static PoolManager Instance;

	public void Awake(){
		Instance = this;
	}

	ObjectPool getObjectPoolByName(string name)
	{
		foreach (var o in Pools) {
			if(o.Name == name)
			{
				return o.Pool;
			}
		}
		Debug.LogError ("Nothing Was found with name:" + name);
		return null; 
	}

	public static ObjectPool GetObjectPoolByName(string name)
	{
		return Instance.getObjectPoolByName(name);
	}
}
