using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public List<GameObject> autos;
    public GameObject player;

	// Use this for initialization

	void Start () {
        foreach (GameObject car in GameObject.FindGameObjectsWithTag("car"))
        {
            autos.Add(car);
        }
        player = GameObject.FindGameObjectWithTag("Player");
        autos.Add(player);
        Level01();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("a"))
        {
            player.GetComponent<CarMovement>().MoveBack();
        }
        if (Input.GetKeyDown("d"))
        {
            player.GetComponent<CarMovement>().MoveFoward();
        }

        DrawAttribute();
    }


    //LLamada desde los botones =====================
    public void MoveFoward()
    {
        player.GetComponent<CarMovement>().MoveFoward();
    }

    public void MoveBackward()
    {
        player.GetComponent<CarMovement>().MoveBack();
    }

    public void Focus()
    {
        player.GetComponent<Powers>().Concentracion();
    }

    public void SmokeWall()
    {
        player.GetComponent<Powers>().Humo();
    }

    public void Hook()
    {
        player.GetComponent<Powers>().Hook();
    }

    public void UkeleleNoise()
    {
        player.GetComponent<Powers>().UkeleleNoise();
    }

    public void OilTrace()
    {
        player.GetComponent<Powers>().Aceite();
    }

    public void SpeedBurst()
    {
        player.GetComponent<Powers>().SpeedBurst();
    }

    public void NinjaTechnique()
    {
        player.GetComponent<Powers>().TecnicaNinja();
    }

    public void NoBrakes()
    {
        player.GetComponent<Powers>().NoBrakes();
    }

    public void MachineGun()
    {
        player.GetComponent<Powers>().Ametralladora();
    }

    public void Flamethrower()
    {
        player.GetComponent<Powers>().LanzaLLamas();
    }

    public void IcyRoad()
    {
        player.GetComponent<Powers>().Escarcha();
    }


    //inicializa valores nivel
    void Level01()
    {
        player.GetComponent<CarStats>().SetCarStats("R8", 12, 6, 22, 5, 5, 100, 1);
        player.GetComponent<DriverStats>().SetPlayerStats("Escar", 30, 4, 60, 80, 60, 100);
        int i = 0;
        foreach (GameObject car in autos)
        {
            if (car.name != "Player")
            {
                //asignamiento pajero de  valores
                car.GetComponent<CarStats>().SetCarStats("R" + i, 10 + i, 5, 20 + i, 5, 5, 100, 0);
                car.GetComponent<DriverStats>().SetPlayerStats("car " + i, 28 + i, 2 + i, 58 + i, 78 + i, 58 + i, 98 + i);
                i++;
            }
        }
    }



    //Estadisticas en pantalla
    void DrawAttribute()
    {
        GameObject[] stats = GameObject.FindGameObjectsWithTag("stats");
        GameObject[] stats2 = GameObject.FindGameObjectsWithTag("stat2");
        foreach (var stat in stats)
        {
            if (stat.name == "Text")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<DriverStats>().level.ToString();
            }
            if (stat.name == "Text (1)")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<DriverStats>().agility.ToString();
            }
            if (stat.name == "Text (2)")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<DriverStats>().reflex.ToString();
            }
            if (stat.name == "Text (3)")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<DriverStats>().selfControl.ToString();
            }
            if (stat.name == "Text (4)")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<DriverStats>().haste.ToString();
            }
            if (stat.name == "Text (5)")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<DriverStats>().money.ToString();
            }
        }

        foreach (var stat in stats2)
        {
            if (stat.name == "Text")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<CarStats>().carName.ToString();
            }
            if (stat.name == "Text (1)")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<CarStats>().velocity.ToString();
            }
            if (stat.name == "Text (2)")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<CarStats>().agarre.ToString();
            }
            if (stat.name == "Text (3)")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<CarStats>().aceleracion.ToString();
            }
            if (stat.name == "Text (4)")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<CarStats>().frenos.ToString();
            }
            if (stat.name == "Text (5)")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<CarStats>().nitroso.ToString();
            }
            if (stat.name == "Text (6)")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<CarStats>().integrity.ToString();
            }
        }
    }

}
