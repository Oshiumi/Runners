using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateWindow : MonoBehaviour
{

    public Text Message;
    public Text Name;
    public float Speed = 10f;

    float StartX;

    void Start()
    {
        StartX = gameObject.transform.position.x;

        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while(gameObject.transform.position.x - StartX < 300f)
        {
            gameObject.transform.Translate(new Vector2(Speed, 0));
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(3.0f);

        while (gameObject.transform.position.x - StartX > 0)
        {
            gameObject.transform.Translate(new Vector2(-Speed, 0));
            yield return new WaitForFixedUpdate();
        }

        Destroy(gameObject);
    }

    public void HitMe(int attacker)
    {
        Name.text = "Player " + attacker;
        Name.color = SetColor(attacker);
        Message.text = "妨害された！";
    }

    public void HitOther(int attacker)
    {
        Name.text = "Player " + attacker;
        Name.color = SetColor(attacker);
        Message.text = "当たった！";
    }

    Color SetColor(int number)
    {
        Color color;

        switch (number)
        {
            case 1:
                color = Color.green;
                break;
            case 2:
                color = Color.yellow;
                break;
            case 3:
                color = Color.blue;
                break;
            case 4:
                color = Color.red;
                break;
            default:
                color = Color.white;
                break;
        }

        return color;
    }
}

