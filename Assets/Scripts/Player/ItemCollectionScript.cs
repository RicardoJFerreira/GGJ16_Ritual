using UnityEngine;
using System.Collections;

public class ItemCollectionScript : MonoBehaviour {

	GameObject[] items = new GameObject[4];
	int itemCount = 0;

	public GameObject slots;
	private SpriteRenderer slotsSprite;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 4; i++) {
			items [i] = null;
		}
		slotsSprite = slots.GetComponentInChildren<SpriteRenderer> ();
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

	public bool CanAddItem(){
		return itemCount < 4;
	}

	public void updateItemCollectionDisplay(){
		for (int i = 0; i < 4; i++) {
			if (items [i] != null) {
				items [i].transform.position = new Vector3 (slotsSprite.bounds.min.x + (slotsSprite.bounds.extents.x/4) + (slotsSprite.bounds.extents.x / 2) * i,slotsSprite.bounds.center.y,items[i].transform.position.z);
				items [i].SetActive (true);
			}
		}
	}
}
