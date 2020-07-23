using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public static Player Instance;
	void Awake()
	{
		Instance = this;
	}
	public int GetBestDistance(){
		return PlayerPrefs.GetInt("bestDist",0);
	}
	public void SetDistance(int distance){
		if(PlayerPrefs.GetInt("bestDist",0) < distance){
			PlayerPrefs.SetInt("bestDist",distance);
			PlayerPrefs.Save();
		}
	}

	public bool IsFBAuthorized()
	{
		return PlayerPrefs.GetInt("FBAuth",0) != 0;
	}
}
