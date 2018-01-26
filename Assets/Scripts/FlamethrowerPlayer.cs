using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerPlayer : MonoBehaviour {

    private GameObject player;
    private GameObject Flame;
    private bool keepPlaying;
    private int failSafe = 20;
    
    private Vector3 offsetFlame;


	// Use this for initialization
	void Start () {
        
        player = GameObject.FindGameObjectWithTag("Player");
        Flame = GameObject.FindGameObjectWithTag("Flame");
        if (Flame && player)
        {
            keepPlaying = true;
            GetComponent<SpriteRenderer>().enabled = false;
            offsetFlame = Flame.transform.position - player.transform.position;
        }
        



    }

    public IEnumerator Flaming()
    {
        int cycles = 0;
        while (keepPlaying == true)
        {
            
            GetComponent<Animation>().Play("Flamethrower");
            yield return new WaitForSeconds(5f);
            GetComponent<Animation>().Stop("Flamethrower");
            cycles++;
            if (cycles >= failSafe)
            {
                yield break;
            }
        }

        yield return null;
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void LateUpdate()
    {
        if (Flame && player)
        {
            Flame.transform.position = player.transform.position + offsetFlame;
        }
        
    }


}
