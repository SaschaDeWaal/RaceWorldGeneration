using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Thansk to
//http://www.gamasutra.com/blogs/GustavoMaciel/20131229/207833/Generating_Procedural_Racetracks.php
//https://www.youtube.com/watch?v=ZnTiWcIznEQ
//https://en.wikipedia.org/wiki/Gift_wrapping_algorithm

public class TrackGenerator : MonoBehaviour {

    public GameObject place;

    //settings
    public float trackWidth = 0.3f;
    public float size = 100;
    public float VertecesInOneMeter = 2;
    public int amountRandomPoints = 30;
    public float aboveGround = 0.5f;

    public TrackPoint[] waypoints = new TrackPoint[0];

    private static TrackGenerator instance = null;
    public static TrackGenerator Instance {
        get {
            if(instance == null) {
                instance = GameObject.FindGameObjectWithTag("track").GetComponent<TrackGenerator>();
            }
            return instance;
        }
    }

    public void Create() {

        //needer variables
        List<TrackPoint> randomPoints = new List<TrackPoint>();
        List<TrackPoint> trackPoints = new List<TrackPoint>();

        //Create Track
        randomPoints = CreateRandomPoints(amountRandomPoints, Mathf.RoundToInt(size));
        trackPoints  = GiftWrapping(randomPoints);
        trackPoints  = CreateRounding(trackPoints);
        trackPoints  = SetHeight(trackPoints);

        waypoints = trackPoints.ToArray();

        //set track
        Mesh mesh = createMesh(trackPoints);
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;

    }

    private List<TrackPoint> CreateRounding(List<TrackPoint> points) {

        int i = 0;
        while(i < points.Count) { 
            int next  = i+1 < points.Count ? i+1 : 0;
            int count = -points.Count;
            points = CreateRounding(points, i, next);
            i += count + points.Count + 1;
        }

        return points;
    }

    private List<TrackPoint> CreateRounding(List<TrackPoint> points, int pointIndexA, int pointIndexB) {

        List<TrackPoint> b = new List<TrackPoint>();
        float dis          = Vector2.Distance(points[pointIndexA].point, points[pointIndexB].point);
        Vector2 controll   = ((points[pointIndexA].point + points[pointIndexB].point) * 0.5f) * dis*0.01f*Random.Range(0f, 2f);

        for(int i = 0; i < dis* VertecesInOneMeter; i++) {
            Vector2 pos      = Tools.Bezier(points[pointIndexA].point, points[pointIndexB].point, controll, i/ (dis* VertecesInOneMeter));
            TrackPoint track = new TrackPoint();
            track.point      = pos;
            b.Add(track);
        }

        for(int i = b.Count - 1; i >= 0; i--) {
            points.Insert(pointIndexB, b[i]);
        }

        return points;
    }

    private List<TrackPoint> CreateRandomPoints(int points, int fieldSize) {
        List<TrackPoint> randomPoints = new List<TrackPoint>();

        for(int i = 0; i < points; i++) {
            TrackPoint newPoint = new TrackPoint();
            newPoint.point = new Vector2(Random.Range(-(fieldSize * 0.5f), fieldSize * 0.5f), Random.Range(-(fieldSize * 0.5f), fieldSize * 0.5f));

            randomPoints.Add(newPoint);
        }

        return randomPoints;
    }

    private TrackPoint FindMostLeft(List<TrackPoint> points) {
        TrackPoint found = points[0];

        foreach(TrackPoint point in points) {
            if (point.point.x < found.point.x) {
                found = point;
            }
        }

        return found;
    }

    private List<TrackPoint> GiftWrapping(List<TrackPoint> allPoints) {

        List<TrackPoint> list   = new List<TrackPoint>();
        TrackPoint currentPoint = FindMostLeft(allPoints);
        
        //check one round
        float targetAngle  = 90;
        int index          = 0;
        TrackPoint next    = null;
        
        while(currentPoint.index < 0) {

            list.Add(currentPoint);
            currentPoint.index    = index;
            currentPoint.rotation = Mathf.RoundToInt(targetAngle);

            float bestDiff  = 10000;
            float nextAngle = 1000;

            //oneRound
            foreach(TrackPoint point in allPoints) {
                if(point != currentPoint) {

                    float angle = Angle(0, currentPoint.point, point.point);
                    float diff  = targetAngle - angle;

                    if(diff < 0) {
                        diff = 360 + diff;
                    }

                    if(diff < bestDiff) {
                        nextAngle = angle;
                        bestDiff  = diff;
                        next      = point;
                    }
                }
            }


            targetAngle  = nextAngle;
            currentPoint = next;
            index++;
        }

        return list;
    }

    private float Angle(float globalRotation, Vector2 vec1, Vector2 vec2) {
        Vector2 v2 = vec2 - vec1;
        return Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
    }


    private List<TrackPoint> SetHeight(List<TrackPoint> allPoints) {
        foreach(TrackPoint point in allPoints) {
            point.height = Tools.GetHeight(point.point) + aboveGround;
        }

        return allPoints;
    }

    private Mesh createMesh(List<TrackPoint> allPoints) {

        
        float halfWidth = trackWidth * 0.5f;

        List<Vector3> verts = new List<Vector3>();
        List<Vector2> uvs   = new List<Vector2>();
        List<int> trian     = new List<int>();

        for(int i = 0; i < allPoints.Count; i++) {

            int nextIndex = i + 1 < allPoints.Count ? i + 1 : 0;

            Vector3 start = new Vector3(allPoints[i].point.x, 0, allPoints[i].point.y);
            Vector3 end = new Vector3(allPoints[nextIndex].point.x, 0, allPoints[nextIndex].point.y);

            //next point
            Vector3 insideEndVert = end * (1f- halfWidth);
            end *= 1f+halfWidth;
            float endHeight = Mathf.Max(Tools.GetHeight(new Vector2(end.x, end.z)), GetHeight(insideEndVert)) + aboveGround;
            verts.Add(end + (endHeight * Vector3.up));
            verts.Add(insideEndVert + (endHeight * Vector3.up));

            //this point
            Vector3 insideVert = start * (1f - halfWidth);
            start *= 1f + halfWidth;
            float startHeight  = Mathf.Max(Tools.GetHeight(new Vector2(start.x, start.z)), GetHeight(insideVert)) + aboveGround;
            verts.Add(start + (startHeight * Vector3.up));
            verts.Add(insideVert + (startHeight * Vector3.up));

            trian.Add(0 + (i*4));
            trian.Add(1 + (i * 4));
            trian.Add(3 + (i * 4));

            trian.Add(0 + (i * 4));
            trian.Add(3 + (i * 4));
            trian.Add(2 + (i * 4));

            uvs.Add(new Vector2(0, 0));
            uvs.Add(new Vector2(0, 1));
            uvs.Add(new Vector2(1, 1));
            uvs.Add(new Vector2(1, 0));
        }

        Mesh mesh      = new Mesh();
        mesh.vertices  = verts.ToArray();
        mesh.uv        = uvs.ToArray();
        mesh.triangles = trian.ToArray();

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        return mesh;
    }


    private float GetHeight(Vector3 pos) {
        return Tools.GetHeight(new Vector2(pos.x, pos.z));
    }

}
