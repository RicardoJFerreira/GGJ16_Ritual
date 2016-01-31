using UnityEngine;
using System.Collections;

public class CalderonScript : MonoBehaviour {

	GameObject[] items = new GameObject[4];
	public int itemCount = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
