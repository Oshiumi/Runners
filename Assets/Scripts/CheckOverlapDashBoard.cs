using UnityEngine;
using System.Collections;

public class CheckOverlapDashBoard : MonoBehaviour {

    public bool IsOverlapping { get; private set; }
	// Use this for initialization
	void Start () {
        IsOverlapping = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag == "DashBoard")
        {
            IsOverlapping = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "DashBoard")
        {
            IsOverlapping = false;
        }
    }
}
