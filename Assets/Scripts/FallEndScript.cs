using UnityEngine;
using System.Collections;

public class FallEndScript : MonoBehaviour
{

    public GameObject ReviveArea;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.transform.position = ReviveArea.transform.position + new Vector3(0, 5, 0);
        }

        //ここでダメージ(ペナルティ)を与えよ
    }
}
