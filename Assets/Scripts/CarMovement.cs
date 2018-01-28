using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour {

    List<GameObject> autos;

	// Use this for initialization
	void Start () {
        autos = GameObject.Find("GameController").GetComponent<GameController>().autos;
        foreach(GameObject auto in autos)
        {
            auto.GetComponent<BoxCollider2D>().size = auto.GetComponent<SpriteRenderer>().size;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    float DistanceFromThis(GameObject obj)
    {
        return obj.transform.position.x - this.transform.position.x;
    }

    public GameObject InFront()
    {
        GameObject carInFront = null;
        foreach (var car in autos)
        {
            if (!carInFront)
            {
                if (DistanceFromThis(car) < 0 || car == this.gameObject)
                {
                    continue;
                }
                carInFront = car;
            }

            if (DistanceFromThis(car) > 0 && DistanceFromThis(car) < DistanceFromThis(carInFront))
            {
                carInFront = car;
            }
        }

        return carInFront;
    }

    public GameObject Behind()
    {
        GameObject carBehind = null;
        foreach (var car in autos)
        {
            if (!carBehind)
            {
                if (DistanceFromThis(car) > 0 || car == this.gameObject)
                {
                    continue;
                }
                carBehind = car;
            }

            if (DistanceFromThis(car) < 0 && DistanceFromThis(car) > DistanceFromThis(carBehind))
            {
                carBehind = car;
            }
        }

        return carBehind;
    }

    public void Swap(GameObject car)
    {
        if (car)
        {
            Vector2 temp = this.transform.position;
            this.transform.position = car.transform.position;
            car.transform.position = temp;

        }
    }

    public void MoveFoward()
    {
        GameObject carInFront = InFront();
        if (carInFront)
        {
            Swap(carInFront);
        }
    }

    public void MoveBack()
    {
        GameObject carBehind = Behind();
        if (carBehind)
        {
            Swap(carBehind);
        }
    }
}
