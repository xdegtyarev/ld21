using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {
	public tk2dButton button;
	// Use this for initialization
	void Start () {
		button.ButtonPressedEvent += HandleButtonPressedEvent;
	}

	void HandleButtonPressedEvent (tk2dButton source)
	{
		gameObject.active = false;
		Llama.Instance.StartGame();
	}
	// Update is called once per frame
}
