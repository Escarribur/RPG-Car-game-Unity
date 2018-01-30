using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {
    List<Vector2> posiciones = new List<Vector2>();
    GameObject player ;
    GameObject[] autos;

    void CargaPosiciones()
    {
        posiciones.Add(new Vector2(-45.7f, -57.4f));
        posiciones.Add(new Vector2(-25.7f, -64.4f));
        posiciones.Add(new Vector2(-5.7f, -57.4f));
        posiciones.Add(new Vector2(14.3f, -64.4f));
        posiciones.Add(new Vector2(34.3f, -57.4f));
    }

    public void Level01()
    {
        CargaPosiciones();
        player = GameObject.FindGameObjectWithTag("Player");
        autos =  GameObject.FindGameObjectsWithTag("car");

        player.GetComponent<CarStats>().SetCarStats("R8", 12, 6, 22, 5, 5, 100, 0);
        player.GetComponent<DriverStats>().SetPlayerStats("Escar", 30, 4, 60, 80, 60, 100);
        player.transform.position = posiciones[0];

        autos[0].GetComponent<CarStats>().SetCarStats("R8", 12, 6, 22, 5, 5, 100, 0);
        autos[0].GetComponent<DriverStats>().SetPlayerStats("Car 1", 28, 2, 58, 78, 58, 98);
        autos[0].transform.position = posiciones[1];

        autos[1].GetComponent<CarStats>().SetCarStats("R8", 12, 6, 22, 5, 5, 100, 0);
        autos[1].GetComponent<DriverStats>().SetPlayerStats("Car 2", 28, 2, 58, 78, 58, 98);
        autos[1].transform.position = posiciones[2];

        autos[2].GetComponent<CarStats>().SetCarStats("R8", 12, 6, 22, 5, 5, 100, 0);
        autos[2].GetComponent<DriverStats>().SetPlayerStats("Car 3", 28, 2, 58, 78, 58, 98);
        autos[2].transform.position = posiciones[3];

        autos[3].GetComponent<CarStats>().SetCarStats("R8", 12, 6, 22, 5, 5, 100, 0);
        autos[3].GetComponent<DriverStats>().SetPlayerStats("Car 4", 28, 2, 58, 78, 58, 98);
        autos[3].transform.position = posiciones[4];

    }

}
