using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public List<GameObject> autos;
    public GameObject player;
    public GameObject menuPanel;
    public GameObject startPanel;
    public GameObject accionPanel;
    public GameObject offenssivePanel;
    public GameObject defensivePanel;
    public GameObject preparationPanel;
    public GameObject waitPanel;
    public GameObject level;
	// Use this for initialization

	void Start () {
        foreach (GameObject car in GameObject.FindGameObjectsWithTag("car"))
        {
            autos.Add(car);
        }
        player = GameObject.FindGameObjectWithTag("Player");
        autos.Add(player);

        //inicia primer nivel
        //level.GetComponent<LevelController>().Level01();

        
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
        if (Input.GetKeyDown("p"))
        {
            CheckPosition();
        }
        if (Input.GetKeyDown("k"))    //Con este boton inician los turnos!!!!
        {
            StartCoroutine(Turns(2));
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

    public void WaitPanel()
    {
        ActiveDeactivePanel(waitPanel, accionPanel);
    }

    public void MenuPanel()
    {
        ActiveDeactivePanel(menuPanel, startPanel);
    }

    public void AccionPanelForce()
    {
        ActiveDeactivePanel(accionPanel, preparationPanel);
        ActiveDeactivePanel(accionPanel, offenssivePanel);
        ActiveDeactivePanel(accionPanel, defensivePanel);
        ActiveDeactivePanel(accionPanel, waitPanel);
    }

    public void TimeToPlay()
    {
        switch (player.GetComponent<DriverStats>().state)
        {
            case DriverStats.DriverSM.Playing:
                AccionPanelForce();
                break; 

            case DriverStats.DriverSM.Waiting:
                WaitPanel();
                break;
        }
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


    void CheckPosition()
    {
        foreach (var car in GameObject.Find("CheckPosition").GetComponent<CheckPositionController>().autos)
        {
            Debug.Log(car.name.ToString());
        }
        
    }

    IEnumerator Turns(int cantTurns)
    {
        GameObject checkPosition = GameObject.Find("CheckPosition");
        for (int i = 0; i < cantTurns; i++)
        {
            Debug.Log("Turn " + i);
            foreach (var car in checkPosition.GetComponent<CheckPositionController>().autos)
            {

                car.transform.GetChild(3).GetComponent<SpriteRenderer>().enabled = true;
                car.GetComponent<DriverStats>().state = DriverStats.DriverSM.Playing;
                TimeToPlay();
                yield return new WaitForSeconds(5f);
                car.GetComponent<DriverStats>().state = DriverStats.DriverSM.Waiting;
                car.transform.GetChild(3).GetComponent<SpriteRenderer>().enabled = false;
            }
            checkPosition.GetComponent<CheckPositionController>().UpdatePosicion();
        }
    }

}
