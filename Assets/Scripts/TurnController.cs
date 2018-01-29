using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour {

    public GameObject actualTurn;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Turns(int cantTurns)
    {
        Vector2 posTurn;
        for (int i = 0; i < cantTurns; i++)
        {
            Debug.Log("Turn " + i);
            foreach (var car in GameObject.Find("CheckPosition").GetComponent<CheckPositionController>().autos)
            {
                posTurn = new Vector2(car.transform.position.x, car.transform.position.y + 7);
                actualTurn.transform.position = posTurn;
                car.GetComponent<DriverStats>().state = DriverStats.DriverSM.Playing;
                yield return new WaitForSeconds(5f);
                car.GetComponent<DriverStats>().state = DriverStats.DriverSM.Waiting;
            }
        }
    }
}
