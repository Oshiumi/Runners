using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ErrorMsg : MonoBehaviour {

    public Text errorMsg;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void ErrorMassage(){
        StartCoroutine("Error");
    }

    IEnumerator Error(){
        errorMsg.text = "2人以上でないと始められません";

        yield return new WaitForSeconds (5.0f);

        errorMsg.enabled = false;
    }
}
