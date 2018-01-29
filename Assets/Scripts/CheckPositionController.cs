using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPositionController : MonoBehaviour {

    public List<GameObject> autos;

	// Use this for initialization
	void Start () {
        autos.Add(GameObject.FindGameObjectWithTag("Player"));
        autos.AddRange(GameObject.FindGameObjectsWithTag("car"));
        autos.Sort(SortByX);
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    static int SortByX(GameObject p1, GameObject p2)
    {
        return p1.transform.position.x.CompareTo(p2.transform.position.x);
    }

    public void UpdatePosicion()
    {
        autos.Sort(SortByX);
    }



}
