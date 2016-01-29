﻿using UnityEngine;
using System.Collections;

public class ResizeSpriteToScreen : MonoBehaviour
{

    private SpriteRenderer sr;

    // Use this for initialization
    void Start () {
    SpriteRenderer sr = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update ()
	{
	    Resize();
	}

    void Resize()
    {
        if (sr == null) return;

        transform.localScale = new Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;


        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector3 xWidth = transform.localScale;
        xWidth.x = worldScreenWidth / width;
        transform.localScale = xWidth;
        //transform.localScale.x = worldScreenWidth / width;
        Vector3 yHeight = transform.localScale;
        yHeight.y = worldScreenHeight / height;
        transform.localScale = yHeight;
        //transform.localScale.y = worldScreenHeight / height;

    }
}
