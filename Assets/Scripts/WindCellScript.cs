using UnityEngine;
using System.Collections;

public class WindCellScript : MonoBehaviour
{

    float time;
    // Use this for initialization
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 0.2f)
        {
            time = 0;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag != "Player") return;

        if (c.gameObject != (GameObject.Find("PunManager")).GetComponent<PunScript>().player)
        {
            c.gameObject.GetComponent<CharacterMove>().StartCoroutine("Damage", c.gameObject.tag);
        }
    }
}
