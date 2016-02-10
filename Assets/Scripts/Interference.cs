using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Interference : MonoBehaviour {

    public GameObject Prefab;

    public void HitMe(int attacker)
    {
        GameObject prefab = Create();

        prefab.GetComponent<CreateWindow>().HitMe(attacker);
    } 

    public void HitOther(int other)
    {
        GameObject prefab = Create();

        prefab.GetComponent<CreateWindow>().HitOther(other);
    }

    GameObject Create()
    {
        GameObject prefab = (GameObject)Instantiate(Prefab);
        prefab.transform.SetParent(gameObject.transform, false);

        return prefab;
    }
}
