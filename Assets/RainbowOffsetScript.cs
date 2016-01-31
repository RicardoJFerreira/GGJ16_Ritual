using UnityEngine;
using System.Collections;

public class RainbowOffsetScript : MonoBehaviour {

	public float scrollSpeed = 0.5f;
	public float tileSizeZ;

	public MeshRenderer rend;

	public Vector3 startPosition;


	// Use this for initialization
	void Start () {
		
		startPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		float newPosition = Mathf.Repeat (Time.time * scrollSpeed, tileSizeZ);
		print ((startPosition + Vector3.forward * newPosition).ToString());
		gameObject.transform.position = startPosition + new Vector3(0,1,0) * newPosition;
	}
}
