using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Scripting;

public class PlayerMovement : MonoBehaviour {

    public GameObject[] autos;
    public GameObject player;

    

    CarStats carStats;
    PlayerStats playerStats;
    bool start;

    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start() {
        autos = GameObject.FindGameObjectsWithTag("car");
        player = GameObject.Find("Player");
        
        start = true;

        carStats = GetComponent<CarStats>();
        playerStats = GetComponent<PlayerStats>();
        
    }

    // Update is called once per frame
    void Update() {

        if (start)
        {
            Level01();
            start = false;
        }

        if (Input.GetKeyDown("a"))
        {
            MoveBack();
        }
        if (Input.GetKeyDown("d"))
        {
            MoveFoward();
        }
        if (Input.GetKeyDown("p")) //inicializa valores de gameobjects del nivel
        {
            
            Debug.Log("PLAYER MOVEMENT: " + carStats.nameCar);
            Debug.Log("goal: " + autos[0].GetComponent<CarStats>().nameCar);
        }
        
        DrawAttribute();
    }

    GameObject InFront()
    {
        GameObject carInFront = null;
        autos = GameObject.FindGameObjectsWithTag("car");
        player = GameObject.Find("Player");
        foreach (var car in autos)
        {
            if (!carInFront)
            {
                if (DistanceFromPlayer(car) < 0 || car == player)
                {
                    continue;
                }
                carInFront = car;
            }

            if (DistanceFromPlayer(car) > 0 && DistanceFromPlayer(car) < DistanceFromPlayer(carInFront))
            {
                carInFront = car;
            }
        }

        return carInFront;
    }

    GameObject Behind()
    {
        GameObject carBehind = null;
        autos = GameObject.FindGameObjectsWithTag("car");
        player = GameObject.Find("Player");
        foreach (var car in autos)
        {
            if (!carBehind)
            {
                if (DistanceFromPlayer(car) > 0 || car == player)
                {
                    continue;
                }
                carBehind = car;
            }

            if (DistanceFromPlayer(car) < 0 && DistanceFromPlayer(car) > DistanceFromPlayer(carBehind))
            {
                carBehind = car;
            }
        }

        return carBehind;
    }

    float DistanceFromPlayer(GameObject obj)
    {
        return obj.transform.position.x - player.transform.position.x;
    }

    public void Move(GameObject car)
    {
        if (car)
        { 
            Vector2 temp = player.transform.position;
            player.transform.position = car.transform.position;
            car.transform.position = temp;

        }
    }   



    //for test
    void ArrayOfDistances()
    {
        float[] x = new float[5];
        int i = 0;
        foreach (var car in autos)
        {
            x[i] = DistanceFromPlayer(car);
            i++;
        }
        i = 0;
        foreach (var car in autos)
        {

            Debug.Log(car.name + ": " + x[i]);
            i++;
        }
    }


    public void MoveFoward()
    {
        GameObject carInFront = InFront();
        if (carInFront)
        {
            Move(carInFront);
        }
    }

    public void MoveBack()
    {
        GameObject carBehind = Behind();
        if (carBehind)
        {
            Move(carBehind);
        }
    }


    // nivel provisorio

    void Level01()
    {
        //autos = GameObject.FindGameObjectsWithTag("car");
        carStats.SetCarStats("R8", 12, 6, 22, 5, 5, 100,1);
        playerStats.SetPlayerStats("Escar",30,4,60,80,60,100);
        int i = 0;
        foreach (GameObject car in autos)
        {
            if (car.name != "Player")
            {
                //asignamiento pajero de  valores
                car.GetComponent<CarStats>().SetCarStats("R" + i, 10+i, 5, 20+i, 5, 5, 100,0);
                playerStats.SetPlayerStats("car "+i, 28+i, 2+i, 58+i, 78+i, 58+i, 98+i);
                i++;
            }
        }
    }

    //utilities========================================================================

    bool IsShielded(GameObject obj)
    {
        if (obj.GetComponent<CarStats>().state == State.Shielded)
        {
            return true;
        }

        return false;
    }


    //Reviso si puede usar poder en obj
    //si no puedo 0, 1 si tiene escudo, 2 si puedo
    int CanUsePower(GameObject obj)
    {
        if (obj)
        {
            if (IsShielded(obj))
            {
                return 1;
            }
            return 2;
        }
        return 0;
    }

    void DecreaseShieldCar(GameObject obj)
    {
        obj.GetComponent<CarStats>().shields--;

        if (obj.GetComponent<CarStats>().shields == 0)
        {
            obj.GetComponent<CarStats>().state = State.Normal;
        }
    }

    void IncreaseShieldCar(GameObject obj)
    {
        obj.GetComponent<CarStats>().shields++;

        if (obj.GetComponent<CarStats>().shields > 0)
        {
            obj.GetComponent<CarStats>().state = State.Shielded;
        }
    }

    

    //Habilidades ======================================================================

    //avanza una posicion a menos que tenga escudo
    public void Hook()
    {
        GameObject carInFront = InFront();
        int effect = CanUsePower(carInFront);

        if (effect == 2 && player.GetComponent<CarStats>().aceleracion >= carInFront.GetComponent<CarStats>().aceleracion)
        {
            //animacion de gancho falta <------
            Move(carInFront);
        }
        if (effect == 1)
        {
            DecreaseShieldCar(carInFront);
        }
        if(effect == 0)

        {
            Debug.Log("No puedo enganchar");
        }
        
    }

    //aumenta en 20 para el burst la velocidad, si la diferencia con el auto delantero es 50 adelanta
    public void SpeedBurst()
    {
        GameObject carInFront = InFront();
        if (carInFront && player.GetComponent<CarStats>().velocity + 20 - carInFront.GetComponent<CarStats>().velocity > 50 )
        {
            Move(carInFront);
        }
        else
        {
            Debug.Log("No puedo adelantar");
        }
    }

    //aumenta estadisticas de conductor selfcontrol+10, reflex+10, permanente
    public void Concentracion()
    {
        player.GetComponent<PlayerStats>().selfControl += 10;
        player.GetComponent<PlayerStats>().reflex += 10;
    }

    //disminuye habilidades auto detras, agarre-10, velocity-10, permamente
    public void Aceite()
    {
        GameObject carBehind = Behind();
        int effect = CanUsePower(carBehind);
        if (effect == 2)
        {
            carBehind.GetComponent<CarStats>().agarre -= 10;
            carBehind.GetComponent<CarStats>().velocity -= 10;
        }
        if (effect == 1)
        {
            DecreaseShieldCar(carBehind);
        }
        if(effect == 0)
        {
            Debug.Log("botaste aceite a la nada");
        }
    }


    //afecta estadisticas conductor detras agility-40, reflex-40
    public void Humo()
    {
        GameObject carBehind = Behind();

        if (carBehind)
        {
            carBehind.GetComponent<PlayerStats>().agility -= 40;
            carBehind.GetComponent<PlayerStats>().reflex -= 60;
        }
        else
        {
            Debug.Log("El humo se espacio en la nada");
        }
    }

    //Afecta auto detras integrity-40, velocity-40 
    public void LanzaLLamas()
    {
        
        GameObject carBehind = Behind();
        int effect = CanUsePower(carBehind);

        if (effect == 2)
        {
            carBehind.GetComponent<CarStats>().integrity -= 40;
            carBehind.GetComponent<CarStats>().velocity -= 20;
            //Este hace visible la llamarada
            GameObject.FindGameObjectWithTag("Flame").GetComponent<SpriteRenderer>().enabled = true;
            //Debug.Log("Active? " + gameObject.activeInHierarchy);
            //Este es el intento de activar el Player
            //GameObject.FindGameObjectWithTag("Player").SetActive(true);
            //ACA está la llamada de la corutina    
            //StartCoroutine(GameObject.FindGameObjectWithTag("Flame").GetComponent<FlamethrowerPlayer>().Flaming());
        }
        if (effect == 1)
        {
            DecreaseShieldCar(carBehind);
        }
        if (effect == 0)
        {
            Debug.Log("Quemaste el aire");
        }
        
    }

    //baja estadisticas auto y conductor delante 
    public void Ametralladora()
    {
        GameObject carInFront = InFront();
        int effect = CanUsePower(carInFront);

        if (effect == 2)
        {
            carInFront.GetComponent<CarStats>().integrity -= 10;
            carInFront.GetComponent<PlayerStats>().agility -= 20;
            carInFront.GetComponent<PlayerStats>().selfControl -= 20;
            carInFront.GetComponent<PlayerStats>().reflex -= 20;
        }
        if (effect == 1)
        {
            DecreaseShieldCar(carInFront);
        }
        if (effect == 0)
        {
            Debug.Log("Diparaste al aire");
        }
    }

    //afecta estadostocas auto detras
    public void NoBrakes()
    {
        GameObject carBehind = Behind();
        int effect = CanUsePower(carBehind);
        if (effect == 2)
        {
            carBehind.GetComponent<CarStats>().frenos -= 30;
        }
        if (effect == 1)
        {
            DecreaseShieldCar(carBehind);
        }
        if (effect == 0)
        {
            Debug.Log("no le puedes cortar los frenos a la nada");
        }
    }

    //afecta estadisticas de conductores delante y atras
    public void UkeleleNoise()
    {
        GameObject carBehind = Behind();
        GameObject carInFront = InFront();
        int effect1 = CanUsePower(carBehind);
        int effect2 = CanUsePower(carInFront);
        if (effect1 == 2)
        {
            carBehind.GetComponent<PlayerStats>().agility -= 30;
            carBehind.GetComponent<PlayerStats>().selfControl -= 30;
        }
        if (effect1 == 1)
        {
            DecreaseShieldCar(carBehind);
        }
        if (effect1 == 0)
        {
            Debug.Log("Por suerte nadie detras escucho eso");
        }
        if (effect2 == 2)
        {
            carInFront.GetComponent<PlayerStats>().agility -= 30;
            carInFront.GetComponent<PlayerStats>().selfControl -= 30;
        }
        if (effect2 == 1)
        {
            DecreaseShieldCar(carInFront);
        }
        if (effect2 == 0)
        {
            Debug.Log("Por suerte nadie delante escucho eso");
        }
    }

    //afecta estadisticas auto delante
    public void Escarcha()
    {
        GameObject carInFront = InFront();
        int effect = CanUsePower(carInFront);
        if (effect == 2)
        {
            carInFront.GetComponent<CarStats>().agarre -= 10;
            carInFront.GetComponent<CarStats>().velocity -= 10;
        }
        if (effect == 1)
        {
            DecreaseShieldCar(carInFront);
        }
        if (effect == 0)
        {
            Debug.Log("Escarchita al suelo");
        }
    }

    //aumenta estadistica conductor
    public void TecnicaNinja()
    {
        player.GetComponent<PlayerStats>().agility += 30;
    }

    //Proteje jugador de 1 poder
    public void SteelShield()
    {
        IncreaseShieldCar(player);
        //animacion de escudo here!!!
    }


    //atributos en pantalla

    void DrawAttribute()
    {
        GameObject[] stats = GameObject.FindGameObjectsWithTag("stats");
        GameObject[] stats2 = GameObject.FindGameObjectsWithTag("stat2");
        foreach (var stat in stats)
        {
            if (stat.name == "Text")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<PlayerStats>().level.ToString();
            }
            if (stat.name == "Text (1)")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<PlayerStats>().agility.ToString();
            }
            if (stat.name == "Text (2)")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<PlayerStats>().reflex.ToString();
            }
            if (stat.name == "Text (3)")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<PlayerStats>().selfControl.ToString();
            }
            if (stat.name == "Text (4)")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<PlayerStats>().haste.ToString();
            }
            if (stat.name == "Text (5)")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<PlayerStats>().money.ToString();
            }
        }

        foreach (var stat in stats2)
        {
            if (stat.name == "Text")
            {
                stat.GetComponent<UnityEngine.UI.Text>().text = player.GetComponent<CarStats>().nameCar.ToString();
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



