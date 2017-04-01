using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPoint {

    public Vector2 point;
    public int rotation; 
    public int index = -1;
    public float height = 0;

    public override string ToString() {
        return "TrackPoint(" + point.x + ", " + point.y + ", " + index + ")";
    }

    public Vector2 waypaoint {
        get {
            return point;
        }
    }

    public Vector3 worldPosition {
        get {
            return new Vector3(point.x, height, point.y);
        }
    }
}
