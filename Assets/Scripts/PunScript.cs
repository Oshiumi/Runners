using UnityEngine;
using System.Collections;

public class PunScript : Photon.MonoBehaviour {
    
    public GameObject player;
    public int countPlayer = 0;
    private bool host = false;
    private GameObject stone;
    private GoalScript goal;
    public GameObject[] Starts;
    public int myNum = -1;
    private CautionManager caution;
    private Interference inter;
    private Vector3 deltaStone = new Vector3 (30,5,0);
    private Vector3 deltaWind = new Vector3 (10, 0, 0);
    private bool isFirst = true;
    private string selectedName;

    // Use this for initialization
    void Start () {
        //  Initialization of photon
        inter = (GameObject.Find ("/UI/Window")).GetComponent<Interference> ();
        PhotonNetwork.ConnectUsingSettings ("0.1");
        caution = (GameObject.Find ("/UI/Caution/CautionMark")).GetComponent<CautionManager> ();
        selectedName = CharacterSelect.SelectedCharacter.Name;
    }
    
    // Update is called once per frame
    void Update () {
        
    }
    
    //  Join random room
    void OnJoinedLobby(){
        PhotonNetwork.JoinRandomRoom();
    }
    
    //Failed to join random room
    void OnPhotonRandomJoinFailed(){
        //Make room (Room Name is sample)
        PhotonNetwork.CreateRoom ("sample",true,true,4);
        countPlayer++;
        host = true;
    }
    
    //  Success to join random room
    void OnJoinedRoom(){
        // Make charactor
        if (!host) {
            (GameObject.Find ("Button")).GetComponent<StartButton> ().deleteButton ();
            photonView.RPC ("CountPlayer", PhotonTargets.All);
        } else {
            myNum = 1;
            makeCharacter ();
        }
    }
    
    public void StartGame(){
        if (countPlayer > 1) {
            photonView.RPC ("StartFlagOn", PhotonTargets.All);
        } else {
            (GameObject.Find("ErrorMassage")).GetComponent<ErrorMsg>().ErrorMassage();
        }
    }
    
    void makeCharacter(){
        player = PhotonNetwork.Instantiate (selectedName, Starts [myNum-1].transform.position + new Vector3 (0, -0.1f, 0), Quaternion.identity, 0);
        (GameObject.Find ("Main Camera")).GetComponent<TrackingCamera> ().Player = player;
        (GameObject.Find ("Main Camera")).GetComponent<TrackingCamera> ().setMove ();
        (GameObject.Find ("Main Camera")).GetComponent<TrackingCamera> ().join = true;
        player.GetComponent<CharacterMove> ().enabled = false;
        player.GetComponent<StaminaScript> ().enabled = false;
    }
    
    public void fallStone(int num){
        photonView.RPC ("RPCStone", PhotonTargets.Others,num);
    }
    
    public void windBlow(int num){
        photonView.RPC ("RPCWind",PhotonTargets.Others,num);
    }
    
    public void boostAura(int num){
        photonView.RPC ("RPCAura",PhotonTargets.All,num);
    }
    
    [PunRPC]
    void RPCAura(int num){
        switch (num) {
        case 1:
            (GameObject.Find("Player1")).GetComponent<StaminaScript>().StartAura();
            break;
        case 2:
            (GameObject.Find("Player2")).GetComponent<StaminaScript>().StartAura();
            break;
        case 3:
            (GameObject.Find("Player3")).GetComponent<StaminaScript>().StartAura();
            break;
        case 4:
            (GameObject.Find("Player4")).GetComponent<StaminaScript>().StartAura();
            break;
        }
    }
    
    [PunRPC]
    void RPCWind(int num){
        caution.CautionEnter ();
        inter.HitMe (num);
        transform.Rotate (new Vector3 (0, 0, 180));
        GameObject wind = PhotonNetwork.Instantiate ("Wind", player.transform.position + deltaWind, transform.rotation, 0);
    }
    
    [PunRPC]
    void RPCStone(int num){
        inter.HitMe (num);
        caution.CautionEnter ();
        PhotonNetwork.Instantiate ("Stone", player.transform.position + deltaStone, player.transform.rotation, 0);
    }
    
    [PunRPC]
    void CountPlayer(){
        if (!host) {
            return;
        }
        countPlayer++;
        photonView.RPC ("countClient", PhotonTargets.Others, countPlayer);
        if (countPlayer == 4) {
            StartGame();
        }
    }
    
    [PunRPC]
    void countClient(int count){
        countPlayer = count;
        if (isFirst) {
            myNum = countPlayer;
            makeCharacter ();
            isFirst = false;
        }
    }
    
    [PunRPC]
    void StartFlagOn(){
        Destroy(GameObject.Find("Match"));
        (GameObject.Find ("CountDown")).GetComponent<StartText> ().gameStart (player);
    }
}