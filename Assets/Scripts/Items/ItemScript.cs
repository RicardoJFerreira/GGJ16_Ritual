using UnityEngine;
using System.Collections;

public class ItemScript : MonoBehaviour {

	public int id;
	bool goingToCalderon;
	bool dropping;
	bool collidingWithCalderon;
	float timePassed;
	float animationTime;

	public Vector3 positionTarget;
	public Vector3 originalPosition;

	// Use this for initialization
	void Start () {
		positionTarget = new Vector3 (0,-85,0);
		goingToCalderon = false;
		animationTime = 1.0f;
		dropping = false;
		collidingWithCalderon = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (goingToCalderon && transform.position!=positionTarget && !dropping) 
		{
			timePassed = Mathf.Min (timePassed, animationTime);
			transform.position = originalPosition + (positionTarget - originalPosition) * (timePassed / animationTime);



			timePassed += Time.deltaTime;
		}
		if (transform.position == positionTarget) {
			dropping = true;
			gameObject.GetComponent<Rigidbody2D> ().gravityScale = 40;
			gameObject.GetComponent<CircleCollider2D> ().enabled = true;
			gameObject.GetComponent<CircleCollider2D> ().radius = 22.0f;
		}
		if (collidingWithCalderon && dropping) {
			gameObject.SetActive (false);
		}
	}

	public int getId()
	{
		return id;
	}

	public void GoToCalderon(){
		goingToCalderon = true;
		timePassed = 0;
		originalPosition = transform.position;
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag.Equals ("Calderon")) {
			collidingWithCalderon = true;
		}
	}

}
