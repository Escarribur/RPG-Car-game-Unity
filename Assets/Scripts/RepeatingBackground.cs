using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour {

    private SpriteRenderer sRender;
    private float groundHorizontalLength;
    private float positionInitial;
	
	void Start ()
    {
        positionInitial = transform.position.x;
        sRender = GetComponent<SpriteRenderer>();
        groundHorizontalLength = sRender.size.x;
    }


	void Update ()
    {
        if (transform.position.x < positionInitial - 6f * groundHorizontalLength)
        {
            RepositionBackground();
        }
	}

    private void RepositionBackground()
    {
        Vector3 groundOffset = new Vector3(groundHorizontalLength * 6f, 0, 0);
        transform.position = transform.position + groundOffset;
    }
}
