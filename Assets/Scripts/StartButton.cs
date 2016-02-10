using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ClickToStart(){
        (GameObject.Find ("PunManager")).GetComponent<PunScript> ().StartGame ();
    }

    public void enableButton(){
        GetComponent<Button> ().enabled = true;
    }

    public void deleteButton(){
        Destroy (gameObject);
    }
}
