using UnityEngine;
using System.Collections;

public class Stone : MonoBehaviour {

    private float timer = 0;
    private int fromPlauerNum = -1;

    // Use this for initialization
    void Start () {
    
    }
    
    // Update is called once per frame
    void Update () {
        if(gameObject.GetComponent<Rigidbody2D>().velocity.magnitude == 0){
            timer += Time.deltaTime;
            if(timer > 1.0f){
                Destroy(gameObject);
            }
        }
    }
    
    void OnCollisionEnter2D(Collision2D c){
        if(c.gameObject.tag != "Player") return;
        
        if (c.gameObject != (GameObject.Find ("PunManager")).GetComponent<PunScript> ().player) {
            c.gameObject.GetComponent<CharacterMove> ().StartCoroutine ("Damage", c.gameObject.tag);
        }
    }
}