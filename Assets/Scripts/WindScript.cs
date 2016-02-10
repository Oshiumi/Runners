using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class WindScript : MonoBehaviour {

    public int NumberOfCells;
    public int[] SafeCellIndices;
    public GameObject WindCell;
    public List<GameObject> cells;
    Vector2 velocity;
    public float Speed;
    public int Lifespan;
    int count;

	// Use this for initialization
	void Start ()
    {
        cells = new List<GameObject>();
        Quaternion q = transform.rotation;
        velocity = ((Vector2)(q * new Vector3(1, 0, 0))).normalized * Speed;
        
        var nv1 = ((Vector2)(Quaternion.Euler(0, 0, 90) * velocity)).normalized;
        var nv2 = (nv1 * -1).normalized;

        Vector2 istart = (Vector2)transform.position + nv1.normalized * (NumberOfCells / 2) * 2;
        for (int i = 0; i < NumberOfCells; ++i)
        {
            if(Array.IndexOf(SafeCellIndices, i) != -1)
            {
                istart += nv2 * 2;
            }
            else
            {
                var instance = (GameObject)Instantiate(WindCell, istart, Quaternion.Euler(0, 0, 0));
                instance.transform.parent = transform;
                cells.Add(instance);
                istart += nv2 * 2;
            }
            
        }

        count = 0;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position += (Vector3)velocity;

        if (count++ == Lifespan)
        {
            foreach(var c in cells)
            {
                Destroy(c.gameObject);
            }

            Destroy(gameObject);
        }
	}
}
