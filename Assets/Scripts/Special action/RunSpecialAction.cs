using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunSpecialAction : MonoBehaviour {

    private static RunSpecialAction _instance;
    public static RunSpecialAction instance {
        get {
            if(_instance == null) {
                _instance = (new GameObject("RunSpecialAction")).AddComponent<RunSpecialAction>();
            }
            return _instance;
        }
    }

    public void Run(GameObject car, SpecialActions action) {

        switch(action) {
            case SpecialActions.dash:
                StartCoroutine(Dash(car));
                break;
            case SpecialActions.teleport:
                Teleport(car);
                break;
        }

    }

    private void Teleport(GameObject car) {
        TrackPoint[] points = TrackGenerator.Instance.waypoints;

        int closed    = 0;
        float lastDis = 100;

        for(int i = 0; i < points.Length; i++) {
            float d = Vector3.Distance(points[i].worldPosition, car.transform.position);
            if(d < lastDis) {
                lastDis = d;
                closed = i;
            }
        }

        closed += 10;
        if (closed > points.Length) {
            closed = points.Length - closed;
        }

        Vector3 pos = points[closed].worldPosition;
        pos.y       = Tools.GetHeight(new Vector3(pos.x, pos.z));
        car.transform.position = pos;
        car.transform.LookAt(Vector3.zero);
        car.transform.eulerAngles = new Vector3(0, car.transform.eulerAngles.y - 90, 0);


    }

    private IEnumerator Dash(GameObject car) {
        float speed = 30;

        while(speed > 0) {
            speed *= 1f - Time.deltaTime;
            car.transform.Translate(Vector3.forward * Time.deltaTime * speed);
            yield return null;
        }
    }


}
