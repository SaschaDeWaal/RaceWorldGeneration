using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGeneration : MonoBehaviour {

    public GameObject tree;

    private float range = 400;
    private float amount = 200;

    public void Place() {
        for(int i = 0; i < amount; i++) {
            Vector2 pos = new Vector2(Random.Range((range* -0.5f), (range*0.5f)), Random.Range((range * -0.5f), (range * 0.5f)));
            if(AllowPlacing(pos)) {
                CreateObject(tree, pos);
                pos = new Vector2((range * -0.5f), (range * 0.5f));
            }
        }
    }

    private void CreateObject(GameObject template, Vector2 point) {
        Vector3 pos = new Vector3(point.x, GetHeight(point), point.y);

        GameObject obj   = Instantiate(template, pos, Quaternion.Euler(0, Random.Range(0, 360), 0));
    }

    private float GetHeight(Vector2 pos) {
        RaycastHit hit;
        if(Physics.Raycast(new Vector3(pos.x, 400, pos.y), -Vector3.up, out hit)) {
            return hit.point.y;
        }
        return 0;
    }

    private bool AllowPlacing(Vector2 pos) {
        RaycastHit hit;
        if(Physics.Raycast(new Vector3(pos.x, 400, pos.y), -Vector3.up, out hit)) {
            return (hit.transform.gameObject.tag == "terain");
        }
        return false;
    }
}
