using UnityEngine;
using System.Collections;

public class CalderonScript : MonoBehaviour {

	GameObject[] items = new GameObject[4];
	public int itemCount = 0;
    public Animator _anim;
    private bool animationStarted;
    private bool _evil_chosen;
    private float timeAnimationChosen;
    private float timeToAnimate;
    private bool animationPlayed;
    public GameObject ballon;
    public GameObject potion;
    public GameObject gameMenu;
    public GameObject finishGameMenu;

    private float timePotionShowed;
    private float timeToShowMenu;

    // Use this for initialization
    void Start () {
        _anim = GameObject.Find("explosion").GetComponent<Animator>();
	    animationStarted = false;
	    timeAnimationChosen = 0.0f;
	    timeToAnimate = 3;
        timeToShowMenu = 3;
        timePotionShowed = 0;
	    animationPlayed = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (itemCount == 4) {
			bool good = true;
			bool evil = true;
			for (int i = 0; i < 4; i++) {
				bool itemEvil = items [i].GetComponent<ItemScript> ().evil;
				good = good && !itemEvil;
				evil = evil && itemEvil;
			}



		    if (!animationStarted)
		    {
		        if (evil)
		        {
		            print("AllEvil");
		           // _anim.SetBool("GameWon", true);
		            _evil_chosen = true;

		            //  _anim.Play("explode",4);
		        }
		        else
		        {
		            print("Mixed");
		           // _anim.SetBool("GameWon", false);
                   // _anim.Play("explode", 4);
		            _evil_chosen = false;

		        }
                animationStarted = true;
		        timeAnimationChosen = Time.time;
		    }

		    if (!animationPlayed && animationStarted && (Time.time >=( timeAnimationChosen + timeToAnimate) ))
		    {
		        if (_evil_chosen)
		        {
		            _anim.Play("explode");
                    potion.SetActive(true);
		            timePotionShowed = Time.time;
		            //gameMenu.SetActive(true);

		        }
		        else
		        {
                    ballon.SetActive(true);
                    //Complete with animation name!!!!
                   // _anim.Play("explode_2");
		        }


		        animationPlayed = true;
		    }

            if(_evil_chosen && animationPlayed &&  (Time.time >= (timePotionShowed + timeToShowMenu)))
		    {
                finishGameMenu.SetActive(true);
		    }
		}
	}


    public void HasAnim()
    {
        _anim.Play("explode");
    }
    public bool CanAddItem(){
		return itemCount < 4;
	}

	public void AddItem(GameObject item){
		if (CanAddItem()) {
			for (int i = 0; i < 4; i++) {
				if (items [i] == null) {
					items [i] = item;
					itemCount++;
					break;
				}
			}

		}
	}
}
