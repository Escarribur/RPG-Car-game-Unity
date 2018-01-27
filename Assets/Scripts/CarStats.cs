using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Normal,
    Shielded,
    Dead

}

public class CarStats : MonoBehaviour
    {


        public string carName;
        public int velocity;
        public int agarre;
        public int aceleracion;
        public int frenos;
        public int nitroso;
        public int integrity;
        public int shields;
        public State state = State.Normal;


        public void SetCarStats(string carName, int velocity, int agarre, int aceleracion, int frenos, int nitroso, int integrity, int shields)
        {
            this.carName = carName;
            this.velocity = velocity;
            this.agarre = agarre;
            this.aceleracion = aceleracion;
            this.frenos = frenos;
            this.nitroso = nitroso;
            this.integrity = integrity;
            this.shields = shields;
            if (shields > 0)
            {
                this.state = State.Shielded;
            }
        }


    }
