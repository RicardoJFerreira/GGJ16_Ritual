using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class CharacterTriggers : MonoBehaviour
{

    public float ladder_MaxSpeed = 150f;
    private Rigidbody2D _myRigidBody;
    private bool _canClimb = false;
	private bool _isTouchingItem = false;
	ItemCollectionScript itemCollection;

	public GameObject _pressEObject;
	GameObject _touchingItem;



    // Use this for initialization
    void Start ()
    {
        _myRigidBody = GetComponent<Rigidbody2D>();
		itemCollection = GetComponent<ItemCollectionScript> ();
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

		if (itemCollection.CanAddItem() && _isTouchingItem) {
			_pressEObject.SetActive (true);
			_pressEObject.transform.position = _touchingItem.transform.position;

			if (Input.GetButton ("Grab")) 
			{
				_touchingItem.SetActive (false);
				itemCollection.AddItem (_touchingItem);
				_touchingItem = null;
				_isTouchingItem = false;
				_pressEObject.SetActive (false);
			}

		} else {
			_pressEObject.SetActive (false);
		}


	}

    void OnTriggerEnter2D(Collider2D collider)
    {
		if (collider.gameObject.tag == "Ladder") {
			_canClimb = true;       
			// require Ground Collider 
			//_anim.SetBool("Climb", true);
		} else if (collider.gameObject.tag == "Item") {
			_isTouchingItem = true;
			_touchingItem = collider.gameObject;

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
		else if (collider.gameObject.tag == "Item") {
			_isTouchingItem = false;
			_touchingItem = null;
		}
    }
}
