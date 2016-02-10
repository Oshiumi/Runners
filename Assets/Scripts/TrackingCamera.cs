using UnityEngine;
using System.Collections;

public class TrackingCamera : MonoBehaviour {

    public GameObject Player;
    Transform PlayerTransform;
    public bool join = false;

    CharacterMove Move;

    public float BackXPosition = 8f;
    public float DownYPosition = 5f;
    float plus = 0f;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (!join){
            return;
        }
        float accel = Move.GetCameraPlace();

        if (accel > plus) plus += 1f;
        else if(accel < plus) plus -= 1f;

        float place = -plus / 10 + BackXPosition;

        transform.position = new Vector3(Player.transform.position.x + place, Player.transform.position.y + DownYPosition, gameObject.transform.position.z);
	}

    public void setMove(){
        Move = Player.GetComponent<CharacterMove>();
    }
}
