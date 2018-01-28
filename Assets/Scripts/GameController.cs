using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public List<GameObject> autos;
    public GameObject player;
    public GameObject accionPanel;
    public GameObject offenssivePanel;
    public GameObject defensivePanel;
    public GameObject preparationPanel;

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

    //utilities
    void ActiveDeactivePanel(GameObject panel1, GameObject panel2)
    {
        panel1.SetActive(true);
        panel2.SetActive(false);
    }

    public void OffensivePanel()
    {
        ActiveDeactivePanel(offenssivePanel, accionPanel);
    }

    public void DefensivePanel()
    {
        ActiveDeactivePanel(defensivePanel, accionPanel);
    }

    public void PreparationPanel()
    {
        ActiveDeactivePanel(preparationPanel, accionPanel);
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
        ActiveDeactivePanel(accionPanel, preparationPanel);
    }

    public void SmokeWall()
    {
        player.GetComponent<Powers>().Humo();
        ActiveDeactivePanel(accionPanel, defensivePanel);
    }

    public void Hook()
    {
        player.GetComponent<Powers>().Hook();
        ActiveDeactivePanel(accionPanel, offenssivePanel);
    }

    public void UkeleleNoise()
    {
        player.GetComponent<Powers>().UkeleleNoise();
        ActiveDeactivePanel(accionPanel, offenssivePanel);
    }

    public void OilTrace()
    {
        player.GetComponent<Powers>().Aceite();
        ActiveDeactivePanel(accionPanel, offenssivePanel);
    }

    public void SpeedBurst()
    {
        player.GetComponent<Powers>().SpeedBurst();
        ActiveDeactivePanel(accionPanel, offenssivePanel);
    }

    public void NinjaTechnique()
    {
        player.GetComponent<Powers>().TecnicaNinja();
        ActiveDeactivePanel(accionPanel, preparationPanel);
    }

    public void NoBrakes()
    {
        player.GetComponent<Powers>().NoBrakes();
        ActiveDeactivePanel(accionPanel, offenssivePanel);
    }

    public void MachineGun()
    {
        player.GetComponent<Powers>().Ametralladora();
        ActiveDeactivePanel(accionPanel, offenssivePanel);
    }

    public void Flamethrower()
    {
        player.GetComponent<Powers>().LanzaLLamas();
        ActiveDeactivePanel(accionPanel, offenssivePanel);
    }

    public void IcyRoad()
    {
        player.GetComponent<Powers>().Escarcha();
        ActiveDeactivePanel(accionPanel, offenssivePanel);
    }

    public void SteelShield()
    {
        player.GetComponent<Powers>().SteelShield();
        ActiveDeactivePanel(accionPanel, defensivePanel);
    }

    public void Kamikaze()
    {
        player.GetComponent<Powers>().Kamikaze();
        ActiveDeactivePanel(accionPanel, offenssivePanel);
    }

    public void Laser()
    {
        player.GetComponent<Powers>().Laser();
        ActiveDeactivePanel(accionPanel, offenssivePanel);
    }

    //inicializa valores nivel
    void Level01()
    {
        player.GetComponent<CarStats>().SetCarStats("R8", 12, 6, 22, 5, 5, 100, 0);
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
            if (stat.name == "LevelValue")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<DriverStats>().level.ToString();
            }
            if (stat.name == "AgilityValue")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<DriverStats>().agility.ToString();
            }
            if (stat.name == "ReflexValue")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<DriverStats>().reflex.ToString();
            }
            if (stat.name == "SelfControlValue")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<DriverStats>().selfControl.ToString();
            }
            if (stat.name == "HasteValue")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<DriverStats>().haste.ToString();
            }
            if (stat.name == "MoneyValue")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<DriverStats>().money.ToString();
            }
        }

        foreach (var stat in stats2)
        {
            if (stat.name == "ModelValue")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<CarStats>().carName.ToString();
            }
            if (stat.name == "VelocityValue")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<CarStats>().velocity.ToString();
            }
            if (stat.name == "ControlValue")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<CarStats>().agarre.ToString();
            }
            if (stat.name == "AccelerationValue")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<CarStats>().aceleracion.ToString();
            }
            if (stat.name == "BrakesValue")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<CarStats>().frenos.ToString();
            }
            if (stat.name == "NitroValue")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<CarStats>().nitroso.ToString();
            }
            if (stat.name == "IntegrityValue")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<CarStats>().integrity.ToString();
            }
            if (stat.name == "ShieldValue")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<CarStats>().shields.ToString();
            }
        }
    }

}
