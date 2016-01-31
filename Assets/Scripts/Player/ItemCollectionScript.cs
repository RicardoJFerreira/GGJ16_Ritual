using UnityEngine;
using System.Collections;

public class ItemCollectionScript : MonoBehaviour {

	GameObject[] items = new GameObject[4];
	int itemCount = 0;

	public GameObject slots;
	private SpriteRenderer slotsSprite;

	public GameObject calderonObject;
	CalderonScript calderon;


	// Use this for initialization
	void Start () {
		for (int i = 0; i < 4; i++) {
			items [i] = null;
		}
		slotsSprite = slots.GetComponentInChildren<SpriteRenderer> ();
		calderon = calderonObject.GetComponent<CalderonScript> ();

	}
	
	// Update is called once per frame
	void Update () {
		
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
			updateItemCollectionDisplay ();

		}
	}

	public void UseItem(int item){
		if (CanUseItem(item)) {
			//put the item on the calderon

			calderon.AddItem(items[item]);
			items[item].transform.position = gameObject.transform.position;
			items [item].GetComponent<ItemScript> ().GoToCalderon ();
			//items[item].SetActive(false);
			items [item] = null;
			updateItemCollectionDisplay ();
			itemCount--;
		}
	}

	public bool CanAddItem(){
		return itemCount < 4;
	}

	public bool CanUseItem(int key){
		return (items [key] != null) && calderon.CanAddItem ();
	}

	public void updateItemCollectionDisplay(){
		for (int i = 0; i < 4; i++) {
			if (items [i] != null) {
				float value = (i==0?10: (i==1? 5 : (i==2? -5 : -10)) );
				items [i].transform.position = new Vector3 (slotsSprite.bounds.min.x + value + (slotsSprite.bounds.extents.x/4) + (slotsSprite.bounds.extents.x / 2) * i,slotsSprite.bounds.center.y,-4);
				items [i].GetComponent<CircleCollider2D> ().enabled = false;
				items [i].SetActive (true);
				items [i].transform.SetParent (null);
			}
		}
	}

	public bool HasItems(){
		return itemCount > 0;
	}
}
