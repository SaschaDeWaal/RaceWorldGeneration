using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialVieuwer : MonoBehaviour {

    public SpecialCarry specialCarry;

    private SpecialActions lastSpecial = SpecialActions.none;

    private void Start () {
        for(int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
	
	private void Update () {
		if (specialCarry.Carry != lastSpecial) {
            lastSpecial = specialCarry.Carry;

            for(int i = 0; i < transform.childCount; i++) {
                bool selected = (specialCarry.Carry.ToString() == transform.GetChild(i).gameObject.name);
                transform.GetChild(i).gameObject.SetActive(selected);
            }
        }
	}
}
