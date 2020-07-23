using UnityEngine;

public class ParticleMover : MonoBehaviour {
	// Update is called once per frame
	void Update () {
		Vector3 delta = Vector3.left * Screen.width/6f * Time.deltaTime * Llama.Instance.speed;
		transform.position = transform.position + delta;
	}
}
