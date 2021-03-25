using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
  public List<Vector3> newVertices = new List<Vector3>();
  public List<int> newTriangles = new List<int>();
  public List<Vector2> newUV = new List<Vector2>();
  private Mesh mesh;
  private int squareCount;
  public int meshSize = 15;
  public byte[,] blocks;

  private float tUnit = 0.25f; //Так как текстура 4 на 4 тайла
  Dictionary<string,Vector2> terrains = new Dictionary<string, Vector2>();

    // Start is called before the first frame update
    void Start()
    {
      terrains["Stone"] = new Vector2(0,0);
      terrains["Grass"] = new Vector2(0,1);

      mesh = GetComponent<MeshFilter> ().mesh;



      RecreateMesh();

    }
    public void RecreateMesh(){
      GenTerrain();
      BuildMesh();
      UpdateMesh();
    }
    void GenSquare(int x, int y, Vector2 texture){

      newVertices.Add( new Vector3 (x  , y  , 0 ));
      newVertices.Add( new Vector3 (x + 1 , y  , 0 ));
      newVertices.Add( new Vector3 (x + 1 , y-1 , 0 ));
      newVertices.Add( new Vector3 (x  , y-1 , 0 ));

      newTriangles.Add(squareCount*4);
      newTriangles.Add((squareCount*4)+1);
      newTriangles.Add((squareCount*4)+3);
      newTriangles.Add((squareCount*4)+1);
      newTriangles.Add((squareCount*4)+2);
      newTriangles.Add((squareCount*4)+3);

      newUV.Add(new Vector2 (tUnit * texture.x, tUnit * texture.y + tUnit));
      newUV.Add(new Vector2 (tUnit*texture.x+tUnit, tUnit*texture.y+tUnit));
      newUV.Add(new Vector2 (tUnit * texture.x + tUnit, tUnit * texture.y));
      newUV.Add(new Vector2 (tUnit * texture.x, tUnit * texture.y));

      squareCount++;

    }


    void GenTerrain(){
      blocks=new byte[meshSize,meshSize];

      for(int px=0;px<blocks.GetLength(0);px++){
        for(int py=0;py<blocks.GetLength(1);py++){
          float n = UnityEngine.Random.value;
          if (n>0.5f){
            blocks[px,py] = 1;
          }else{
            blocks[px,py] = 2;
          }
        }
      }
    }

    void BuildMesh(){
      for(int px=0;px<blocks.GetLength(0);px++){
        for(int py=0;py<blocks.GetLength(1);py++){

          if(blocks[px,py]==1){
            GenSquare(px,py,terrains["Stone"]);
          } else if(blocks[px,py]==2){
            GenSquare(px,py,terrains["Grass"]);
          }

        }
      }
    }


    void UpdateMesh(){
      mesh.Clear ();
      mesh.vertices = newVertices.ToArray();
      mesh.triangles = newTriangles.ToArray();
      mesh.uv = newUV.ToArray();
      mesh.Optimize ();
      mesh.RecalculateNormals ();

      squareCount=0;
      newVertices.Clear();
      newTriangles.Clear();
      newUV.Clear();
    }
    // Update is called once per frame
    void Update()
    {
        //UpdateMesh();
    }
}
