using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackSelector : BasicObject
{

    private int timeTillDeath;

    private bool hasSelection = false;

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
    void Start()
    {

        objectsInside = new List<BasicObject>();

        render = GetComponent<Renderer>();
        render.sortingOrder = -2;
        playerController = FindObjectOfType<PlayerController>();

        mesh = GetComponent<MeshFilter>().mesh;

        physicsController = FindObjectOfType<PhysicsController>();

        shape = new PhysicsShape();

        pos = new Vector2(0, 0);

        

        playerController.clearAttackSelection();

        physicsController.addObject(this);

        isTrigger = true;

        square();

        physicsController.checkArea(this);
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime();
        square();

        physicsController.checkArea(this);



    }





    public override void TriggerEntered(BasicObject basicObject)
    {
        if ((basicObject != null) && basicObject.gameObject != null)
        {


            //ships
            if (basicObject.GetComponent<BasicVehicle>() != null)
            {
                if (!basicObject.isFriendly)
                {
                    playerController.attackersSelected.Add(basicObject.GetComponent<BasicVehicle>());
                    basicObject.isSelected = true;

                    

                    

                    objectsInside.Add(basicObject);
                }
                
            }

        }
    }

    public override void TriggerExited(BasicObject basicObject)
    {
        if ((basicObject != null) && basicObject.gameObject != null)
        {
            if (!basicObject.isFriendly)
            {
                playerController.attackersSelected.Remove(basicObject.GetComponent<BasicVehicle>());
                basicObject.isSelected = false;
                objectsInside.Remove(basicObject);
            }

        }
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


        if ((playerController.mouseClickStart.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x == 0) || (playerController.mouseClickStart.y - Camera.main.ScreenToWorldPoint(Input.mousePosition).y == 0))
        {

            newVertices.Add(new Vector3(playerController.mouseClickStart.x, playerController.mouseClickStart.y, 0));
            newVertices.Add(new Vector3(playerController.mouseClickStart.x, playerController.mouseClickStart.y + .01f, 0));
            newVertices.Add(new Vector3(playerController.mouseClickStart.x + .01f, playerController.mouseClickStart.y + .01f, 0));
            newVertices.Add(new Vector3(playerController.mouseClickStart.x + .01f, playerController.mouseClickStart.y, 0));

            newVertices2.Add(new Vector2(playerController.mouseClickStart.x - 0.25f, playerController.mouseClickStart.y - 0.25f));
            newVertices2.Add(new Vector2(playerController.mouseClickStart.x - 0.25f, playerController.mouseClickStart.y + 0.25f));
            newVertices2.Add(new Vector2(playerController.mouseClickStart.x + 0.25f, playerController.mouseClickStart.y + 0.25f));
            newVertices2.Add(new Vector2(playerController.mouseClickStart.x + 0.25f, playerController.mouseClickStart.y - 0.25f));
        }
        else
        {

            if ((Camera.main.ScreenToWorldPoint(Input.mousePosition).x < playerController.mouseClickStart.x && Camera.main.ScreenToWorldPoint(Input.mousePosition).y > playerController.mouseClickStart.y) || (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > playerController.mouseClickStart.x && Camera.main.ScreenToWorldPoint(Input.mousePosition).y < playerController.mouseClickStart.y))
            {
                newVertices.Add(new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, playerController.mouseClickStart.y, 0));
                newVertices.Add(new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0));
                newVertices.Add(new Vector3(playerController.mouseClickStart.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0));
                newVertices.Add(new Vector3(playerController.mouseClickStart.x, playerController.mouseClickStart.y, 0));

                newVertices2.Add(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, playerController.mouseClickStart.y));
                newVertices2.Add(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y));
                newVertices2.Add(new Vector2(playerController.mouseClickStart.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y));
                newVertices2.Add(new Vector2(playerController.mouseClickStart.x, playerController.mouseClickStart.y));
            }
            else
            {
                newVertices.Add(new Vector3(playerController.mouseClickStart.x, playerController.mouseClickStart.y, 0));
                newVertices.Add(new Vector3(playerController.mouseClickStart.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0));
                newVertices.Add(new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0));
                newVertices.Add(new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, playerController.mouseClickStart.y, 0));

                newVertices2.Add(new Vector2(playerController.mouseClickStart.x, playerController.mouseClickStart.y));
                newVertices2.Add(new Vector2(playerController.mouseClickStart.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y));
                newVertices2.Add(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y));
                newVertices2.Add(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, playerController.mouseClickStart.y));
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

        List<PhysicsLine> lines = new List<PhysicsLine>();





        lines.Add(new PhysicsLine(newVertices2[0].x, newVertices2[0].y, newVertices2[1].x, newVertices2[1].y));
        lines.Add(new PhysicsLine(newVertices2[1].x, newVertices2[1].y, newVertices2[2].x, newVertices2[2].y));
        lines.Add(new PhysicsLine(newVertices2[2].x, newVertices2[2].y, newVertices2[3].x, newVertices2[3].y));
        lines.Add(new PhysicsLine(newVertices2[3].x, newVertices2[3].y, newVertices2[0].x, newVertices2[0].y));

        shape.setUpPolygon(lines);



    }

    void lifeTime()
    {
        
        if (playerController.mousePressed == PlayerController.MousePressed.None)
        {
            physicsController.removeObject(this);

            playerController.SetUnits();

            

            Destroy(gameObject);
        }



    }

    public override Vector2 getPos()
    {
        float width = newVertices2[0].x + newVertices2[2].x;
        float height = newVertices2[0].y + newVertices2[2].y;


        return new Vector2(width / 2, height / 2);
    }
}

