using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class RandomMatchmaker : Photon.PunBehaviour
{
    private PhotonView myPhotonView;
	public Vector3 spawnPosition = new Vector3(1.26f, 1f, 1f);

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
        GameObject player = PhotonNetwork.Instantiate("esCapeCharacter", spawnPosition, Quaternion.identity, 0);
		esCapeCharacterController controller = player.GetComponent<esCapeCharacterController> ();
		controller.enabled = true;
//        monster.GetComponent<myThirdPersonController>().isControllable = true;
		myPhotonView = player.GetComponent<PhotonView>();
    }

    public void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());

        if (PhotonNetwork.inRoom)
        {
            bool shoutMarco = GameLogic.playerWhoIsIt == PhotonNetwork.player.ID;

            if (shoutMarco && GUILayout.Button("Marco!"))
            {
                myPhotonView.RPC("Marco", PhotonTargets.All);
            }
            if (!shoutMarco && GUILayout.Button("Polo!"))
            {
                myPhotonView.RPC("Polo", PhotonTargets.All);
            }
        }
    }
}
