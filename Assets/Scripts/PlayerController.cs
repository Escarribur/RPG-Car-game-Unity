using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float velocity;

	// Use this for initialization
	void Start () {
        velocity = 10;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(velocity, 0));
        }
       this.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity, 0);
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        //Debug.Log("HeeeeeloOOOOO" + coll.name);
        if (coll.gameObject.name == "EndCheck")
            this.transform.position = new Vector2(-64.5f, this.transform.position.y);

    }
}


   