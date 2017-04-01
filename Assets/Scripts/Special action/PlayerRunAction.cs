using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunAction : MonoBehaviour {

    private SpecialCarry specialCarry;

    private void Start() {
        specialCarry = GetComponent<SpecialCarry>();
    }

    private void Update () {
		if(Input.GetKey(KeyCode.Space) && specialCarry.Carry != SpecialActions.none) {
            RunSpecialAction.instance.Run(gameObject, specialCarry.Carry);
            specialCarry.Clear();
        }
	}
}
