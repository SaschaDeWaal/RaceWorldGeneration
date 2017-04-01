using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    public GameObject player;

    private float abovePlayer = 10;
    private float behindPlayer = 10;

	private void Start () {
		
	}

    private void Update () {
        transform.LookAt(player.transform.position);

        transform.Translate(Vector3.forward * Time.deltaTime * (Vector3.Distance(transform.position, player.transform.position)- behindPlayer));
        transform.position = new Vector3(transform.position.x, player.transform.position.y+ abovePlayer, transform.position.z);
	}
}
