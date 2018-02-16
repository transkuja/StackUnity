using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Vector3 cameraOffset;
    [SerializeField]
    GameManager gameManager;

    private void Start()
    {
        cameraOffset = transform.position;    
    }

    void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("mouse clicked");
            AudioManager.Instance.PlayOneShot(AudioManager.Instance.coinFX);
        }
	}


    void OnClickProcess()
    {
        // Pose le bloc
        // S'il est dessus
        // s'il est parfaitement dessus ==> combo ==> play ok sound
        // sinon ==> cut ==> play cut sound
        // gamemanager position up
        // Spawn à l'autre point de spawn
        // Sinon lose
    }
}
