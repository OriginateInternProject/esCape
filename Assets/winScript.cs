using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class winScript : MonoBehaviour {


	// Update is called once per frame
	void Update () {
		var x = transform.position.x;
		var z = transform.position.z;

		if (x > 56.75 && x < 60.0 && z > 52.1 && z < 58) {
			SceneManager.LoadScene (1);
		}
	}
}
