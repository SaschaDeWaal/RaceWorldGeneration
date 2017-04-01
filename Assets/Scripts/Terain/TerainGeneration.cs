using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Thanks to https://www.youtube.com/watch?v=1HV8GbFnCik 
public class TerainGeneration : MonoBehaviour {

    public int divisions;
    public float size;
    public float height;

    private Vector3[] verts;
    private int vertCount;

    public Mesh CreateTerainMesh() {
        
        vertCount = (divisions + 1) * (divisions + 1);
        verts = new Vector3[vertCount];

        Vector2[] uvs = new Vector2[vertCount];
        int[] tris    = new int[divisions* divisions*6];

        float halfSize     = size * 0.5f;
        float divisionSize = size / divisions;
        int tryOffset      = 0;

        //create mesh
        for(int x = 0; x <= divisions; x++) {
            for(int y = 0; y <= divisions; y++) { 

                verts[x*(divisions+1)+y] = new Vector3(-halfSize+y* divisionSize, 0, halfSize-x* divisionSize);
                uvs[x * (divisions+1) + y] = new Vector2((float)x/divisions, (float)y/divisions);

                if(x < divisions && y < divisions) {

                    int topLeft = x * (divisions + 1) + y;
                    int botLeft = (x + 1) * (divisions + 1) + y;

                    tris[tryOffset]   = topLeft;
                    tris[tryOffset+1] = topLeft+1;
                    tris[tryOffset+2] = botLeft+1;

                    tris[tryOffset + 3] = topLeft;
                    tris[tryOffset + 4] = botLeft+1;
                    tris[tryOffset + 5] = botLeft;

                    tryOffset += 6;

                }

            }
        }

        //set corners on random heights
        verts[0].y = Random.Range(-height, height);
        verts[divisions].y = Random.Range(-height, height);
        verts[verts.Length-1].y = Random.Range(-height, height);
        verts[verts.Length - 1- divisions].y = Random.Range(-height, height);

        int iterations = (int)Mathf.Log(divisions, 2);
        int numSquares = 1;
        int squarSize = divisions;

        //iterations over world
        for(int i = 0; i < iterations; i++) {
            int row = 0;

            for(int j = 0; j < numSquares; j++) {
                int col = 0;

                for(int k = 0; k < numSquares; k++) {
                    DiamandSquare(row, col, squarSize, height);
                    col += squarSize;
                }

                row += squarSize;
            }

            numSquares *= 2;
            squarSize /= 2;
            height *= 0.3f;

        }

        //Set and create Mesh
        Mesh mesh = new Mesh();
        mesh.vertices  = verts;
        mesh.uv        = uvs;
        mesh.triangles = tris;
        mesh.name      = "Terain";

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        return mesh;
    }

    private void DiamandSquare(int row, int col, int size, float offset) {

        int halfSize = (int)(size * 0.5f);
        int topLeft  = row * (divisions + 1) + col;
        int botLeft  = (row + size) * (divisions + 1) + col;
        int mid      = (int)(row + halfSize) * (divisions + 1) + (int)(col + halfSize);

        verts[mid].y = (verts[topLeft].y + verts[topLeft + size].y + verts[botLeft].y + verts[botLeft + size].y) * 0.25f + Random.Range(-offset, offset);

        verts[topLeft + halfSize].y = (verts[topLeft].y + verts[topLeft + size].y + verts[mid].y)    / 3 + Random.Range(-offset, offset);
        verts[mid - halfSize].y     = (verts[topLeft].y + verts[botLeft].y + verts[mid].y)           / 3 + Random.Range(-offset, offset);
        verts[mid+halfSize].y       = (verts[topLeft+size].y + verts[botLeft+size].y + verts[mid].y) / 3 + Random.Range(-offset, offset);
        verts[botLeft+halfSize].y   = (verts[botLeft].y + verts[botLeft+size].y + verts[mid].y)      / 3 + Random.Range(-offset, offset);
    }

}
