using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers : MonoBehaviour {

    GameObject car;
    bool powerUsing;

    private void Start()
    {
        powerUsing = false;
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

    IEnumerator Animation(Transform anim)
    {
        Animator ani = anim.GetComponent<Animator>();
        anim.GetComponent<SpriteRenderer>().enabled = true;
        for (float f = 2f; f >= 0 ; f-=0.1f)
        {
            ani.enabled = true;
            yield return null;
        }
        anim.GetComponent<SpriteRenderer>().enabled = false;
        ani.enabled = false;
        
        yield return null;
    }

    IEnumerator ShieldForSecs(Transform anim, float sec)
    {
        Animator ani = anim.GetComponent<Animator>();
        SpriteRenderer spriRend = anim.GetComponent<SpriteRenderer>();

        ani.enabled = true;
        spriRend.enabled = true;
        yield return new WaitForSeconds(sec);
        ani.enabled = false;
        spriRend.enabled = false;
        DecreaseShieldCar(this.gameObject);
        powerUsing = false;

    }

    IEnumerator WaitAnimationForSecs(Transform anim, float sec)
    {
        Animator ani = anim.GetComponent<Animator>();
        SpriteRenderer spriRend = anim.GetComponent<SpriteRenderer>();

        ani.enabled = true;
        spriRend.enabled = true;
        yield return new WaitForSeconds(sec);
        ani.enabled = false;
        spriRend.enabled = false;
        powerUsing = false;

    }



    //Habilidades ======================================================================

    //avanza una posicion a menos que tenga escudo
    public void Hook()
    {
        GameObject carInFront = this.GetComponent<CarMovement>().InFront();
        int effect = CanUsePower(carInFront);

        if (effect == 2 && this.GetComponent<CarStats>().aceleracion >= carInFront.GetComponent<CarStats>().aceleracion)
        {
            //animacion de gancho falta <------
            this.GetComponent<CarMovement>().Swap(carInFront);
        }
        if (effect == 1)
        {
            DecreaseShieldCar(carInFront);
        }
        if (effect == 0)

        {
            Debug.Log("No puedo enganchar");
        }

    }

    //aumenta en 20 para el burst la velocidad, si la diferencia con el auto delantero es 50 adelanta
    public void SpeedBurst()
    {
        GameObject carInFront = this.GetComponent<CarMovement>().InFront();
        if (carInFront && this.GetComponent<CarStats>().velocity + 20 - carInFront.GetComponent<CarStats>().velocity > 50)
        {
            this.GetComponent<CarMovement>().Swap(carInFront);
        }
        else
        {
            Debug.Log("No puedo adelantar");
        }
    }

    //aumenta estadisticas de conductor selfcontrol+10, reflex+10, permanente
    public void Concentracion()
    {
        this.GetComponent<DriverStats>().selfControl += 10;
        this.GetComponent<DriverStats>().reflex += 10;
    }

    //disminuye habilidades auto detras, agarre-10, velocity-10, permamente
    public void Aceite()
    {
        GameObject carBehind = this.GetComponent<CarMovement>().Behind();
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
        if (effect == 0)
        {
            Debug.Log("botaste aceite a la nada");
        }
    }


    //afecta estadisticas conductor detras agility-40, reflex-40
    public void Humo()
    {
        GameObject carBehind = this.GetComponent<CarMovement>().Behind();

        if (carBehind)
        {
            carBehind.GetComponent<DriverStats>().agility -= 40;
            carBehind.GetComponent<DriverStats>().reflex -= 60;
        }
        else
        {
            Debug.Log("El humo se espacio en la nada");
        }
    }

    //Afecta auto detras integrity-40, velocity-40 
    public void LanzaLLamas()
    {

        GameObject carBehind = this.GetComponent<CarMovement>().Behind();
        int effect = CanUsePower(carBehind);

        if (effect == 2)
        {
            carBehind.GetComponent<CarStats>().integrity -= 40;
            carBehind.GetComponent<CarStats>().velocity -= 20;

            
            StartCoroutine(Animation(this.transform.GetChild(0)));

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
        GameObject carInFront = this.GetComponent<CarMovement>().InFront();
        int effect = CanUsePower(carInFront);

        if (effect == 2)
        {
            carInFront.GetComponent<CarStats>().integrity -= 10;
            carInFront.GetComponent<DriverStats>().agility -= 20;
            carInFront.GetComponent<DriverStats>().selfControl -= 20;
            carInFront.GetComponent<DriverStats>().reflex -= 20;
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
        GameObject carBehind = this.GetComponent<CarMovement>().Behind();
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
        GameObject carBehind = this.GetComponent<CarMovement>().Behind();
        GameObject carInFront = this.GetComponent<CarMovement>().InFront();
        int effect1 = CanUsePower(carBehind);
        int effect2 = CanUsePower(carInFront);
        if (effect1 == 2)
        {
            carBehind.GetComponent<DriverStats>().agility -= 30;
            carBehind.GetComponent<DriverStats>().selfControl -= 30;
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
            carInFront.GetComponent<DriverStats>().agility -= 30;
            carInFront.GetComponent<DriverStats>().selfControl -= 30;
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
        GameObject carInFront = this.GetComponent<CarMovement>().InFront();
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
        this.GetComponent<DriverStats>().agility += 30;
    }

    //Proteje jugador de 1 poder
    public void SteelShield()
    {
        
        if (powerUsing == true)
        {
            Debug.Log("Ya usaste esta habilidad");
        }
        else
        {
            powerUsing = true;
            IncreaseShieldCar(this.gameObject);
            StartCoroutine(ShieldForSecs(this.transform.GetChild(1), 10f));
        }
    }

    //Avanza o retrocede dos posiciones.
    public void Kamikaze()
    {
        float random = Random.Range(1f,2f);
        if (random < 1.5f)
        {
            for (int i = 0; i < 3; i++)
            {
                GetComponent<CarMovement>().MoveFoward();
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                GetComponent<CarMovement>().MoveBack();
            }
        }
    }


    public void Laser()
    {
        if (powerUsing == true)
        {
            Debug.Log("Ya usaste esta habilidad");
        }
        else
        {
            powerUsing = true;
            //Realiza la animación antes de ejecutar el poder
            StartCoroutine(WaitAnimationForSecs(this.transform.GetChild(2), 5f));

            GameObject[] cars = GameObject.FindGameObjectsWithTag("car");
            float random1;
            float random2;
            for (int i = 0; i < cars.Length; i++)
            {
                random1 = Random.Range(0, cars.Length - 1);
                random2 = Random.Range(0, cars.Length - 1);
                GetComponent<CarMovement>().Swap2(cars[(int)random1], cars[(int)random2]);
            }
            random1 = Random.Range(-cars.Length, cars.Length - 1);
            if (random1 >= 0)
            {
                for (int i = 0; i < random1; i++)
                {
                    GetComponent<CarMovement>().MoveFoward();
                }
            }
            else
            {
                for (int i = 0; i > random1; i--)
                {
                    GetComponent<CarMovement>().MoveBack();
                }
            }
        }
    }
}
