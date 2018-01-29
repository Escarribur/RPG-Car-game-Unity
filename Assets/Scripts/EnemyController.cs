using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    bool utilizandoPoder = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (this.gameObject.GetComponent<DriverStats>().state == DriverStats.DriverSM.Playing && this.utilizandoPoder == false)
        {
            StartCoroutine(Playing());
        }
	}

    IEnumerator Playing()
    {
        this.utilizandoPoder = true;
        UsePowers();
        yield return new WaitForSeconds(4f);
        if (this.gameObject.GetComponent<DriverStats>().state == DriverStats.DriverSM.Playing)
        {
            UsePowers();
        }
        this.utilizandoPoder = false;
    }

    void UsePowers()
    {
        int valorPoder = Random.Range(1, 14);
        Powers poder = this.gameObject.GetComponent<Powers>();
        Debug.Log("Nombre objeto: " + this.gameObject.name);
        switch (valorPoder)
        {
            case 1:
                poder.Hook();
                break;
            case 2:
                poder.Humo();
                break;
            case 3:
                poder.Aceite();
                break;
            case 4:
                poder.Ametralladora();
                break;
            case 5:
                poder.Concentracion();
                break;
            case 6:
                poder.Escarcha();
                break;
            case 7:
                poder.Kamikaze();
                break;
            case 8:
                poder.LanzaLLamas();
                break;
            case 9:
                poder.Laser();
                break;
            case 10:
                poder.NoBrakes();
                break;
            case 11:
                poder.SpeedBurst();
                break;
            case 12:
                poder.SteelShield();
                break;
            case 13:
                poder.UkeleleNoise();
                break;
            case 14:
                poder.TecnicaNinja();
                break;
        }
    }
}
