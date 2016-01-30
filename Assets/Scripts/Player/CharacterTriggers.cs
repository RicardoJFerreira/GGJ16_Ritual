using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class CharacterTriggers : MonoBehaviour
{

    public float ladder_MaxSpeed = 150f;
    private Rigidbody2D _myRigidBody;
    private bool _canClimb = false;

    // Use this for initialization
    void Start ()
    {
        _myRigidBody = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {
        float h = CrossPlatformInputManager.GetAxis("Vertical");
        if (h != 0 && _canClimb == true)
        {
            int direction = h > 0 ? 1 : -1;
	        _myRigidBody.isKinematic = true;
	        transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * direction * ladder_MaxSpeed,
                                             transform.position.z ); 
	    }
	    else
	    {
	        _myRigidBody.isKinematic = false;
	    }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Ladder")
        {
            _canClimb = true;
            //_anim.SetBool("Climb", true);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Ladder")
        {
            _canClimb = false;
           // _anim.SetBool("Climb", false);
        }
    }
}
