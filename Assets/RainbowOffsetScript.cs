using UnityEngine;
using System.Collections;

public class RainbowOffsetScript : MonoBehaviour {

	public float scrollSpeed = 0.5F;

	public MeshRenderer rend;

	// Use this for initialization
	void Start () {
		rend = gameObject.GetComponent<MeshRenderer> ();

	}
	
	// Update is called once per frame
	void Update () {
		float offset = Time.time * scrollSpeed;
		rend.material.SetTextureOffset ("_MainTex", new Vector2(0,offset));
	}
}
