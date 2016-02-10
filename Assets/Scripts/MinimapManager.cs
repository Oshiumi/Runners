using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MinimapManager : MonoBehaviour
{

    public Image[] Icons;
    public Image Load;
    GameObject[] Players;
    int Number = 0;
    public GameObject StartPoint;
    public GameObject GoalPoint;

    Vector2 MapStart;

    public float MapStart_X;
    public float MapStart_Y;

    float MapHeight;
    float MapWidth;
    float LoadWidth;
    float MapSpeed;

    public int PlayerMax = 4;

    int[] PlayerNumber;

    // Use this for initialization
    void Start()
    {
        PlayerNumber = new int[PlayerMax];

        Players = GameObject.FindGameObjectsWithTag("Player");

        int tmp;

        do
        {
            tmp = Number;

            for (int i = 0; i < Players.Length; ++i)
            {
                if (Players[i].GetComponent<PlayerData>().PlayerNumber == Number + 1)
                {
                    PlayerNumber[Number] = i;
                    Number++;
                    break;
                }
            }
        } while (Number != tmp && Number < PlayerMax);

        LoadWidth = Mathf.Abs(GoalPoint.transform.position.x - StartPoint.transform.position.x);
        MapWidth = Load.GetComponent<RectTransform>().rect.width;

        MapSpeed = MapWidth / LoadWidth;

        MapHeight = Load.rectTransform.rect.height / 6;

        MapStart = new Vector2(MapStart_X, MapStart_Y);

        for(int i = Number; i < Icons.Length; ++i)
        {
            Icons[i].enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Number > 0) {
            for (int i = 0; i < Number; ++i) {
                float distance = Players [PlayerNumber [i]].transform.position.x - StartPoint.transform.position.x;
                float mapDistance = MapSpeed * distance;
                Vector2 replace = new Vector2 (mapDistance, -MapHeight * (i + 1));
                Icons [i].rectTransform.anchoredPosition = MapStart + replace;
            }
        }
    }

    public void Set()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");

        int tmp;

        do
        {
            tmp = Number;

            for (int i = 0; i < Players.Length; ++i)
            {
                if (Players[i].GetComponent<PlayerData>().PlayerNumber == Number + 1)
                {
                    PlayerNumber[Number] = i;
                    Number++;
                    break;
                }
            }
        } while (Number != tmp && Number < PlayerMax);

        for (int i = 0; i < Icons.Length; ++i)
        {
            Icons[i].enabled = true;
        }

        for (int i = Number; i < Icons.Length; ++i)
        {
            Icons[i].enabled = false;
        }
        Debug.Log (Number);
    }
}
