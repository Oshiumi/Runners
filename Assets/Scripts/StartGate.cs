using UnityEngine;
using System.Collections;

public class StartGate : MonoBehaviour {

    public int gateNum = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col){

        if (col.gameObject.tag != "Player")
            return;

        col.gameObject.name = "Player" + gateNum;
        col.gameObject.GetComponent<PlayerData>().PlayerNumber = gateNum;

        gameObject.GetComponent<BoxCollider2D> ().enabled = false;
    }
}
