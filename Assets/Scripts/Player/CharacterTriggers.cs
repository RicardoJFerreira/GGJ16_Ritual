using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class CharacterTriggers : MonoBehaviour
{

    public float ladder_MaxSpeed = 150f;
    private Rigidbody2D _myRigidBody;
    private bool _canClimb = false;
	private bool _isTouchingItem = false;
	public bool _isTouchingCalderon = false;
	ItemCollectionScript itemCollection;
    public Animator _anim;

    public GameObject _pressEObject;
	GameObject _touchingItem;




    // Use this for initialization
    void Start ()
    {
        _anim = GetComponent<Animator>();
        _myRigidBody = GetComponent<Rigidbody2D>();
		itemCollection = GetComponent<ItemCollectionScript> ();
    }
	
	// Update is called once per frame
	void Update () {
        float h = CrossPlatformInputManager.GetAxis("Vertical");
        if (h != 0 && _canClimb == true)
        {
            _anim.SetBool("isClimb", true);
            int direction = h > 0 ? 1 : -1;
	        _myRigidBody.isKinematic = true;
           // Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("Platforms").GetComponent<Collider2D>(), true);
            transform.position += new Vector3(0, Time.deltaTime * direction * ladder_MaxSpeed, 0 ); 
	    }else if (h == 0 && _canClimb)
	    {
            _anim.SetBool("isClimb", false);
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

		if (_isTouchingCalderon) {

			if (Input.GetButton ("UseItem1") && itemCollection.CanUseItem(0)) {
				itemCollection.UseItem (0);
			} else if (Input.GetButton ("UseItem2") && itemCollection.CanUseItem(1)) {
				itemCollection.UseItem (1);
			} else if (Input.GetButton ("UseItem3") && itemCollection.CanUseItem(2)) {
				itemCollection.UseItem (2);
			}else if(Input.GetButton ("UseItem4") && itemCollection.CanUseItem(3)) {
				itemCollection.UseItem (3);
			}
		}




	}

    void OnTriggerEnter2D(Collider2D collider)
    {
		if (collider.gameObject.tag == "Ladder") {
           // _anim.SetBool("Ground", false);
            _canClimb = true;       
			// require Ground Collider 
			_anim.SetBool("Climb", true);
		} else if (collider.gameObject.tag == "Item") {
			_isTouchingItem = true;
			_touchingItem = collider.gameObject;

		}else if (collider.gameObject.tag == "Calderon") {
			_isTouchingCalderon = true;
		}
    }

    void OnTriggerExit2D(Collider2D collider)
    {
		if (collider.gameObject.tag == "Ladder") {
			_canClimb = false;
			Physics2D.IgnoreCollision (GetComponent<Collider2D> (), GameObject.Find ("Platforms").GetComponent<Collider2D> (), true);
		    _anim.SetBool("Climb", false);
		} else if (collider.gameObject.tag == "Item") {
			_isTouchingItem = false;
			_touchingItem = null;
		} else if (collider.gameObject.tag == "Calderon") {
			_isTouchingCalderon = false;
		}
    }
}
