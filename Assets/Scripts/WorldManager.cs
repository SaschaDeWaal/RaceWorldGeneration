using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

    public GameObject Terain;
    public GameObject Track;

    public TrackObjectPlacer trackObjectPlacer;

    private void Start () {
        StartCoroutine(CreateWorld());
    }

    IEnumerator CreateWorld() {

        Mesh terain = Terain.GetComponent<TerainGeneration>().CreateTerainMesh();
        yield return null;

        Terain.GetComponent<MeshFilter>().mesh = terain;
        Terain.GetComponent<MeshCollider>().sharedMesh = terain;
        yield return null;

        TrackGenerator.Instance.Create();
        yield return null;

        trackObjectPlacer.PlaceObjects();
        yield return null;

        Terain.GetComponent<TreeGeneration>().Place();
        yield return null;

        //set players on start
        SetObjectsOnTrack(GameObject.FindGameObjectsWithTag("car"));

    }

    private void SetObjectsOnTrack(GameObject[] objects) {

        int index = TrackGenerator.Instance.waypoints.Length - 1;

        foreach(GameObject obj in objects) {
            TrackPoint point = TrackGenerator.Instance.waypoints[index];
            Vector3 startPos = point.worldPosition;

            index--;
            startPos.y = Tools.GetHeight(point.point) + 1;

            obj.transform.position = startPos;
            obj.transform.LookAt(Vector3.zero);
            obj.transform.eulerAngles = new Vector3(0, obj.transform.eulerAngles.y-90, 0);
        }
    }


	
}
