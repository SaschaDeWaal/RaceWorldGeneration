using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackObjectPlacer : MonoBehaviour {

    public GameObject[] placeRandom = new GameObject[0];
    public int[] randomAmount = new int[0];

    public GameObject[] placeOnPoints = new GameObject[0];
    public float[] placeAtPoint;

    public void PlaceObjects() {

        TrackPoint[] points = TrackGenerator.Instance.waypoints;

        //random placement
        for(int j = 0; j < placeRandom.Length; j++) { 
            for(int i = 0; i < randomAmount[j]; i++) {
                int random = Random.Range(0, points.Length);
                CreateObject(placeRandom[j], points[random]);
            }
        }

        //place on position
        for(int j = 0; j < placeOnPoints.Length; j++) {
            int numer = Mathf.RoundToInt(placeAtPoint[j] * points.Length * 1f);
            CreateObject(placeOnPoints[j], points[numer]);
        }
    }

    private void CreateObject(GameObject template, TrackPoint point) {
        Vector2 trackPos       = new Vector2(point.worldPosition.x, point.worldPosition.z);
        float height           = Tools.GetHeight(trackPos);
        GameObject obj         = Instantiate(template, point.worldPosition, Quaternion.Euler(0, 0, 0));

        obj.transform.position = new Vector3(obj.transform.position.x, height, obj.transform.position.z);

        obj.transform.LookAt(Vector3.zero);
        obj.transform.eulerAngles = new Vector3(0, obj.transform.eulerAngles.y + 90, 0);
    }

}
