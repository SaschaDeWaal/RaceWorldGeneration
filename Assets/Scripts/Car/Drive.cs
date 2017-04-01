using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour {

    private const float SPEED = 15;
    private const float TURN_SPEED = 80;
	
	private void Update () {

        transform.Translate(Vector3.forward * Time.deltaTime * Input.GetAxis("Vertical") * SPEED);
        transform.Rotate(Vector3.up * Time.deltaTime * Input.GetAxis("Horizontal") * TURN_SPEED);

        if(Input.GetKey(KeyCode.R)) {
            transform.LookAt(Vector3.zero);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y - 90, 0);
        }
	}
}
