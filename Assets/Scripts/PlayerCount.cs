using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerCount : MonoBehaviour {

    public Text playerCount; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update () {
        int count = (GameObject.Find ("PunManager")).GetComponent<PunScript> ().countPlayer;
        playerCount.text =  count + " 人";
	}
}
