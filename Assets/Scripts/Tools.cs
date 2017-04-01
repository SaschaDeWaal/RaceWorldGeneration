using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour {

    //Thanks to http://www.theappguruz.com/blog/bezier-curve-in-games
    public static Vector2 Bezier(Vector2 p0, Vector2 p2, Vector2 p1, float t) {
        //B(t) = (1-t)3P0 + 3(1-t)2tP1 + 3(1-t)t2P2 + t3P3 , 0 < t < 1 
        return (((1f - t) *2f) * p0 +        (2 * (1-t)*t*p1)    + (t*2*p2)) * 0.5f;
    }

    public static float GetHeight(Vector2 pos) {
        RaycastHit hit;
        if(Physics.Raycast(new Vector3(pos.x, 800, pos.y), -Vector3.up, out hit)) {
            return hit.point.y;
        }
        return 0;
    }
}
