using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextrueGeneration : MonoBehaviour {

    private int pixelsPerMeter = 1;

    public Texture2D[] textrues = new Texture2D[1];
    public GameObject textTemplate;

    public Texture2D Create(Mesh mesh, int divisions) {


        Texture2D tex  = new Texture2D(Mathf.RoundToInt(divisions * pixelsPerMeter), Mathf.RoundToInt(divisions * pixelsPerMeter));
        Vector3[] vets = mesh.vertices;


        float min = mesh.bounds.min.y;
        float height = mesh.bounds.size.y;


        for(int x = 0; x < divisions; x++) {
            for(int y = 0; y < divisions; y++) {

                Color pixel = positionToColor(x, y, vets[(x+y) + ((y) * divisions)], min, height);
                tex.SetPixel((y * pixelsPerMeter), (x * pixelsPerMeter), pixel);



            }
        }


        tex.Apply();
        return tex;
    }

    private Color positionToColor(int x, int y, Vector3 pos, float min, float height) {
        float percentage   = (pos.y - min) / height;
        //int index          = Mathf.RoundToInt((textrues.Length-1) * percentage);

        //Color color1 = textrues[0].GetPixel(x, y);
        //Color color2 = textrues[1].GetPixel(x, y);

        /*GameObject temp = Instantiate(textTemplate, transform);
        temp.name = x + "," + y + ", " + percentage + ", " + pos.y;
        temp.transform.localPosition = pos;
        temp.GetComponent<TextMesh>().text = percentage.ToString();
        temp.GetComponent<TextMesh>().color = new Color(percentage, 1f - percentage, 0);*/


        return new Color(percentage, 1f- percentage, 0);
    }

    private float CombinePixel(float colVal1, float colVal2, float per) {
        return ((colVal1 * per) + (colVal2 * (1f - per))) / 2f;
    }

    private Color CombineColor(Color one, Color two, float perc) {
        return new Color(CombinePixel(one.r, two.r, perc),
                         CombinePixel(one.g, two.g, perc),
                         CombinePixel(one.b, two.b, perc));
    }


}
