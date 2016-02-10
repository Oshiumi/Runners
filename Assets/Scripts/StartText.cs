using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartText : MonoBehaviour {

    public Text start;
    private bool startFlag = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void gameStart(GameObject player){
        StartCoroutine("countDown",player);
    }

    IEnumerator countDown(GameObject player){
        (GameObject.Find ("MiniMapManager")).GetComponent<MinimapManager> ().Set();
        for (int i=3; i > 0; i--) {
            start.text = i + "";

            yield return new WaitForSeconds (1.0f);
        }
        start.fontSize = 150;
        start.text = "GO!!";
        player.GetComponent<CharacterMove> ().enabled = true;
        player.GetComponent<StaminaScript>().enabled = true;
        yield return new WaitForSeconds (1.0f);
        start.text = "";
    }
}
