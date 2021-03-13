using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AreaSelector : MonoBehaviour {


    public SpawnerController spawnerController;

    Renderer render;



    private float tUnit = 0.25f;
    private Vector2 tStone = new Vector2(0, 3);
    private Vector2 tGrass = new Vector2(0, 1);


    public List<Vector3> newVertices = new List<Vector3>();
    public List<Vector2> newVertices2 = new List<Vector2>();

    // The triangles tell Unity how to build each section of the mesh joining
    // the vertices
    public List<int> newTriangles = new List<int>();

    // The UV list is unimportant right now but it tells Unity how the texture is
    // aligned on each polygon
    public List<Vector2> newUV = new List<Vector2>();


    // A mesh is made up of the vertices, triangles and UVs we are going to define,
    // after we make them up we'll save them as this mesh
    private Mesh mesh;

    // Use this for initialization
    void Start () {

        render = GetComponent<Renderer>();
        render.sortingOrder = -2;

        mesh = GetComponent<MeshFilter>().mesh;

        spawnerController = FindObjectOfType<SpawnerController>();

        square();

    }
	
	// Update is called once per frame
	void Update () {
        LifeTime();
        square();
    }


    void square()
    {

        newTriangles.Clear();
        newVertices.Clear();
        newVertices2.Clear();
        newUV.Clear();

        newTriangles.Add(0);
        newTriangles.Add(1);
        newTriangles.Add(3);
        newTriangles.Add(1);
        newTriangles.Add(2);
        newTriangles.Add(3);


        if ((spawnerController.mouseClickStart.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x == 0) || (spawnerController.mouseClickStart.y - Camera.main.ScreenToWorldPoint(Input.mousePosition).y == 0))
        {

            newVertices.Add(new Vector3(spawnerController.mouseClickStart.x, spawnerController.mouseClickStart.y, 0));
            newVertices.Add(new Vector3(spawnerController.mouseClickStart.x, spawnerController.mouseClickStart.y + .01f, 0));
            newVertices.Add(new Vector3(spawnerController.mouseClickStart.x + .01f, spawnerController.mouseClickStart.y + .01f, 0));
            newVertices.Add(new Vector3(spawnerController.mouseClickStart.x + .01f, spawnerController.mouseClickStart.y, 0));

            newVertices2.Add(new Vector2(spawnerController.mouseClickStart.x - 0.25f, spawnerController.mouseClickStart.y - 0.25f));
            newVertices2.Add(new Vector2(spawnerController.mouseClickStart.x - 0.25f, spawnerController.mouseClickStart.y + 0.25f));
            newVertices2.Add(new Vector2(spawnerController.mouseClickStart.x + 0.25f, spawnerController.mouseClickStart.y + 0.25f));
            newVertices2.Add(new Vector2(spawnerController.mouseClickStart.x + 0.25f, spawnerController.mouseClickStart.y - 0.25f));
        }
        else
        {

            if ((Camera.main.ScreenToWorldPoint(Input.mousePosition).x < spawnerController.mouseClickStart.x && Camera.main.ScreenToWorldPoint(Input.mousePosition).y > spawnerController.mouseClickStart.y) || (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > spawnerController.mouseClickStart.x && Camera.main.ScreenToWorldPoint(Input.mousePosition).y < spawnerController.mouseClickStart.y))
            {
                newVertices.Add(new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, spawnerController.mouseClickStart.y, 0));
                newVertices.Add(new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0));
                newVertices.Add(new Vector3(spawnerController.mouseClickStart.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0));
                newVertices.Add(new Vector3(spawnerController.mouseClickStart.x, spawnerController.mouseClickStart.y, 0));

                newVertices2.Add(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, spawnerController.mouseClickStart.y));
                newVertices2.Add(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y));
                newVertices2.Add(new Vector2(spawnerController.mouseClickStart.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y));
                newVertices2.Add(new Vector2(spawnerController.mouseClickStart.x, spawnerController.mouseClickStart.y));
            }
            else
            {
                newVertices.Add(new Vector3(spawnerController.mouseClickStart.x, spawnerController.mouseClickStart.y, 0));
                newVertices.Add(new Vector3(spawnerController.mouseClickStart.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0));
                newVertices.Add(new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0));
                newVertices.Add(new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, spawnerController.mouseClickStart.y, 0));

                newVertices2.Add(new Vector2(spawnerController.mouseClickStart.x, spawnerController.mouseClickStart.y));
                newVertices2.Add(new Vector2(spawnerController.mouseClickStart.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y));
                newVertices2.Add(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y));
                newVertices2.Add(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, spawnerController.mouseClickStart.y));
            }
        }




        newUV.Add(new Vector2(tUnit * tStone.x, tUnit * tStone.y + tUnit));
        newUV.Add(new Vector2(tUnit * tStone.x + tUnit, tUnit * tStone.y + tUnit));
        newUV.Add(new Vector2(tUnit * tStone.x + tUnit, tUnit * tStone.y));
        newUV.Add(new Vector2(tUnit * tStone.x, tUnit * tStone.y));



        mesh.Clear();
        mesh.vertices = newVertices.ToArray();
        mesh.triangles = newTriangles.ToArray();
        // mesh.uv = newUV.ToArray();
        ;
        mesh.RecalculateNormals();

        



    }


    void LifeTime()
    {
        if (!spawnerController.mousePressed)
        {
            Destroy(gameObject);
        }
    }
}
