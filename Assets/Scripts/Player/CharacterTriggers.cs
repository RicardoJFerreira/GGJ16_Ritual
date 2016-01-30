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
           // Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("Platforms").GetComponent<Collider2D>(), true);
            transform.position += new Vector3(0, Time.deltaTime * direction * ladder_MaxSpeed, 0 ); 
	    }else if (h == 0 && _canClimb)
	    {
           // Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("Platforms").GetComponent<Collider2D>(), true);
            _myRigidBody.isKinematic = true;
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
            // require Ground Collider 
            //_anim.SetBool("Climb", true);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Ladder")
        {
            _canClimb = false;
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("Platforms").GetComponent<Collider2D>(), true);
            // _anim.SetBool("Climb", false);
        }
    }
}
