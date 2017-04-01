using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

    const float aboveRow = 5;
    const float speed = 50;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "car") {
            TrackPoint[] points = TrackGenerator.Instance.waypoints;

            if(points.Length > 0) {

                TrackPoint closed = points[0];
                float lastDis = 100;
                for(int i = 0; i < points.Length; i++) {
                    float d = Vector3.Distance(points[i].worldPosition, collision.gameObject.transform.position);

                    if(d < lastDis) {
                        lastDis = d;
                        closed = points[i];
                    }
                }

                StartCoroutine(MoveCar(closed.worldPosition + (Vector3.up * aboveRow), collision.gameObject));
            }
        }
    }

    private IEnumerator MoveCar(Vector3 target, GameObject obj) {

        while(Vector3.Distance(target, obj.transform.position) > 1f) {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, target, Time.deltaTime * speed);
            yield return null;
        }

        obj.transform.LookAt(Vector3.zero);
        obj.transform.eulerAngles = new Vector3(0, obj.transform.eulerAngles.y - 90, 0);
    }
}
