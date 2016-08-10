using UnityEngine;
using System.Collections;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
	public class esCapeCharacterController : MonoBehaviour {
		// For a full explanation of the API, look at ExampleCharacterController.cs
		// This example will assume knowledge of the API to code a moving first-person character

		public float speed = 17f;
		public float reticleMaxLength = 2f;
		//public GameObject laserPrefab;

		private static CardboardControl cardboard;
		private bool moving = false;

		private ThirdPersonCharacter character;

		void Start() {
			cardboard = GameObject.Find("CardboardControlManager").GetComponent<CardboardControl>();
			cardboard.trigger.OnDown += ToggleMove;
			cardboard.trigger.OnUp += ToggleMove;
			//cardboard.trigger.OnClick += Interact;
			character = GetComponent<ThirdPersonCharacter>();
		}

		void ToggleMove(object sender) {
			moving = !moving;
		}

		void Update() {
		// If you don't need as much control over what happens when moving is toggled,
		// you can replace this with cardboard.trigger.IsHeld() and remove ToggleMove()
			if (moving) {
//				m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
//				m_Move = v*m_CamForward + h*Camera.main.transform.righ m_Cam.right;
				Vector3 movement = Camera.main.transform.forward;
				transform.Translate (movement * speed * Time.deltaTime);
				//character.Move(movement, false, false);
			}
		}
	}
}