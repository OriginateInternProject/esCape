using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class RandomMatchmaker : Photon.PunBehaviour
{
	public string playerPrefabName = "esCapeCharacter";
	public Vector3 spawnPosition = new Vector3(1.26f, 1f, 1f);

	private PhotonView myPhotonView;

    // Use this for initialization
    public void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("JoinRandom");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnConnectedToMaster()
    {
        // when AutoJoinLobby is off, this method gets called when PUN finished the connection (instead of OnJoinedLobby())
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnPhotonRandomJoinFailed()
    {
        PhotonNetwork.CreateRoom(null);
    }

    public override void OnJoinedRoom()
    {
        GameObject player = PhotonNetwork.Instantiate(playerPrefabName, spawnPosition, Quaternion.identity, 0);
		esCapeCharacterController controller = player.GetComponent<esCapeCharacterController> ();
		ThirdPersonCharacter animatorScript = player.GetComponent<ThirdPersonCharacter> ();
		controller.enabled = true;
		animatorScript.enabled = true;
//        monster.GetComponent<myThirdPersonController>().isControllable = true;
		myPhotonView = player.GetComponent<PhotonView>();
    }

    public void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
}
