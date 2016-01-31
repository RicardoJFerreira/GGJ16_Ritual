using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class CharacterTriggers : MonoBehaviour
{

    public float ladder_MaxSpeed = 150f;
    private Rigidbody2D _myRigidBody;
    private bool _canClimb = false;
	public bool _isTouchingItem = false;
	public bool _isTouchingCalderon = false;
	ItemCollectionScript itemCollection;
    public Animator _anim;
    private bool animSkull = false;

    public GameObject _pressEObject;
	GameObject _touchingItem;

	public GameObject _pressNumObject;
	bool canShowNumObject;

    //skull anim
    private float timePassed = 0f;
    // Skull Tracking variables
    private Vector3 skullPositionTarget;
    private Vector3 skullRotationTarget;
    private Transform skull;
    



    // Use this for initialization
    void Start ()
    {
        skull = GameObject.Find("Skull").GetComponent<Transform>();
        skullPositionTarget = new Vector3(-515, -86, 0);
        skullRotationTarget = new Vector3(0, 0, -35);
        _anim = GetComponent<Animator>();
        _myRigidBody = GetComponent<Rigidbody2D>();
		itemCollection = GetComponent<ItemCollectionScript> ();
		canShowNumObject = true;
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
                if (_touchingItem.name == "Foot")
                {
                    animSkull = true;
                }
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

			if (itemCollection.HasItems () && canShowNumObject) {
				_pressNumObject.SetActive (true);
			} else {
				_pressNumObject.SetActive (false);
			}


			if (Input.GetButton ("UseItem1") && itemCollection.CanUseItem (0)) {
				itemCollection.UseItem (0);
				canShowNumObject = false;
			} else if (Input.GetButton ("UseItem2") && itemCollection.CanUseItem (1)) {
				itemCollection.UseItem (1);
				canShowNumObject = false;
			} else if (Input.GetButton ("UseItem3") && itemCollection.CanUseItem (2)) {
				itemCollection.UseItem (2);
				canShowNumObject = false;
			} else if (Input.GetButton ("UseItem4") && itemCollection.CanUseItem (3)) {
				itemCollection.UseItem (3);
				canShowNumObject = false;
			}
		} else {
			_pressNumObject.SetActive (false);
		}

	    if (animSkull)
	        SkullAnimation();



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
			canShowNumObject = true;
		}
    }

    void SkullAnimation()
    {

        if (skullPositionTarget.y == skull.transform.position.y)
            return;
        timePassed = Mathf.Min(timePassed, 4f);
        skull.transform.position = skull.transform.position + (skullPositionTarget - skull.transform.position) * (timePassed / 4f);
        skull.transform.Rotate(skullRotationTarget, Time.deltaTime * 18f);
        timePassed += Time.deltaTime;

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
