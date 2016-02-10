using UnityEngine;
using System.Collections;
using System;

public class ResultData : MonoBehaviour
{

    public static Color[] Colors = new Color[4];
    public static int[] PlayerNumbers = new int[4];
    public static DateTime[] Times = new DateTime[4];
    public static int PlayerNum;

    //サンプルデータ（実用時は除去すること）
    public static void LoadSample()
    {
        PlayerNum = 2;
        Colors[0] = new Color(255, 0, 0);
        Colors[1] = new Color(0, 255, 0);
        Colors[2] = new Color(0, 0, 255);
        Colors[3] = new Color(255, 255, 0);

        PlayerNumbers[0] = 3;
        PlayerNumbers[1] = 2;
        PlayerNumbers[2] = 4;
        PlayerNumbers[3] = 1;

        Times[0] = new DateTime(1000000);
        Times[1] = new DateTime(2000000);
        Times[2] = new DateTime(3000000);
        Times[3] = new DateTime(4000000);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
