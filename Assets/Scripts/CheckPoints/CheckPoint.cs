using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    public List<string> checkedNames = new List<string>();

    public void OnTriggerEnter(Collider other) {
        checkedNames.Add(other.gameObject.name);
    }
}
