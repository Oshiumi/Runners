using UnityEngine;
using System.Collections;

public class ItemBox : MonoBehaviour {

    public GameObject[] Items;
    public GameObject player;
    int number;

	// Use this for initialization
	void Start () {
        number = Random.Range(0, Items.Length);
        player = (GameObject.Find ("PunManager")).GetComponent<PunScript> ().player;
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<CharacterMove>().GetItem(Items[number],col.gameObject.GetComponent<PlayerData>().PlayerNumber);
        }
        Destroy(gameObject);
    }
}
