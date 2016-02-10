using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text[] MenuTexts;
    public int maxFont = 50;
    public int minFont = 25; 

    int length;
    int select = 0;

    void Start()
    {
        length = MenuTexts.Length;

        MenuTexts[0].fontSize = 50;
    }

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)) {
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                select--;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                select++;
            }

            if(select < 0)
            {
                select = length - 1;
            }

            select %= length;

            for (int i = 0; i < length; i++)
            {
                if (select == i)
                {
                    MenuTexts[i].fontSize = maxFont;
                }
                else
                {
                    MenuTexts[i].fontSize = minFont;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (select < length - 1)
            {
                Application.LoadLevel("CharacterSelect");
            }
            else
            {
                Application.Quit();
            }
        }
	}
}
