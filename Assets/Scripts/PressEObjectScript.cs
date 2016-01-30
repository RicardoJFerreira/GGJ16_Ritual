using UnityEngine;
using System.Collections;

public class PressEObjectScript : MonoBehaviour {

	private Vector3 downPosition;
	private Vector3 upPosition;
	public float animationSpeed;
	private bool goingUp;


	// Use this for initialization
	void Start () {
		goingUp = true;
		downPosition = new Vector3 (0,67,0);
		upPosition = new Vector3 (0,80,0);
	}
	
	// Update is called once per frame
	void Update () {
		float value = Mathf.Clamp(((goingUp ? 1 : -1) * animationSpeed * Time.deltaTime) + transform.localPosition.y, downPosition.y,upPosition.y);
		transform.localPosition = new Vector3 (0,value,0);

		if (transform.localPosition.y == downPosition.y || transform.localPosition.y == upPosition.y) {
			goingUp = !goingUp;
		}
	}
}
