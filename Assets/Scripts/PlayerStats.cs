using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public string playerName { get; set; }
    public int agility { get; set; }
    public int level { get; set; }
    public int selfControl { get; set; }
    public int haste { get; set; }
    public int reflex { get; set; }
    public int money { get; set; }
   

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetPlayerStats(string playerName, int agility, int level, int selfControl, int haste, int reflex, int money)
    {
        this.playerName = playerName;
        this.agility = agility;
        this.level = level;
        this.selfControl = selfControl;
        this.haste = haste;
        this.reflex = reflex;
        this.money = money;
    }


}
