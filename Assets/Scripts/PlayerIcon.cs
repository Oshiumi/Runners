using UnityEngine;
using System.Collections;

public class PlayerIcon : MonoBehaviour {

    int PlayerNumber;

    // 重なった時にずれる距離
    public float IconMoveY;

    public void SetPlayerNumber(int number)
    {
        PlayerNumber = number;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Icon" && col.GetComponent<PlayerIcon>().PlayerNumber<PlayerNumber)
        {
            gameObject.transform.Translate(new Vector2(0, IconMoveY));
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Icon" && col.GetComponent<PlayerIcon>().PlayerNumber<PlayerNumber)
        {
            gameObject.transform.Translate(new Vector2(0, -IconMoveY));
        }
    }
}