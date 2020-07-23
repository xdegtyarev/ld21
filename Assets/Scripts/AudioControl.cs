using UnityEngine;
using System.Collections;

public class AudioControl : MonoBehaviour {
	public tk2dSprite spriteOn;
	public tk2dSprite spriteOff;
	public tk2dButton button;
	bool isOn = true;
	public void Start(){
		button.ButtonDownEvent += HandleButtonDownEvent;
	}

	void HandleButtonDownEvent (tk2dButton source)
	{
		isOn = !isOn;
		if(isOn){
			spriteOn.gameObject.active = true;
			spriteOff.gameObject.active = false;
			AudioController.SetGlobalVolume(1f);
		}
		else{
			spriteOn.gameObject.active = false;
			spriteOff.gameObject.active = true;
			AudioController.SetGlobalVolume(0.001f);
		}
	}
}
