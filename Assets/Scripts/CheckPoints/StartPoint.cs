using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour {

    private CheckPoint[] checkPoint = new CheckPoint[0];

    private void Start() {
        GameObject[] found = GameObject.FindGameObjectsWithTag("checkpoint");
        checkPoint         = new CheckPoint[found.Length];

        for(int i = 0; i < found.Length; i++) {
            checkPoint[i] = found[i].GetComponent<CheckPoint>();
        }
    }

    public void OnTriggerEnter(Collider other) {
        string name = other.gameObject.name;

        if(OtherCheckPoints(name)) {
            FindObjectOfType<EndScreen>().Set(name);
            Destroy(this);
        }
    }

    private bool OtherCheckPoints(string name) {
        foreach(CheckPoint check in checkPoint) {
            if(check.checkedNames.IndexOf(name) == -1) {
                return false;
            }
        }

        return true;
    }
}
