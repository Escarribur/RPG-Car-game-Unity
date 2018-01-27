using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverStats : MonoBehaviour {

    public string driverName { get; set; }
    public int agility { get; set; }
    public int level { get; set; }
    public int selfControl { get; set; }
    public int haste { get; set; }
    public int reflex { get; set; }
    public int money { get; set; }

    public void SetPlayerStats(string driverName, int agility, int level, int selfControl, int haste, int reflex, int money)
    {
        this.driverName = driverName;
        this.agility = agility;
        this.level = level;
        this.selfControl = selfControl;
        this.haste = haste;
        this.reflex = reflex;
        this.money = money;
    }
}
