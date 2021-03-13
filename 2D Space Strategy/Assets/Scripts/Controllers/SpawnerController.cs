using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SpawnerController : MonoBehaviour {

    bool isEnemy;

    public enum ShipType{AntiFighterCorvette, Fighter, Corvette, none};

    ShipType shipeType = new ShipType();

    public bool mouseIsAvalible = true;

    public ObjectVisual objectVisualPrefab;

    public Vector2 mouseClickStart;

    public bool mousePressed;

    public AreaSelector areaSelectorPrefab;

    public List<ObjectVisual> shipLocations = new List<ObjectVisual>();


    // Use this for initialization
    void Start () {
        shipeType = ShipType.none;
        mouseIsAvalible = true;

        mousePressed = false;

    }
	
	// Update is called once per frame
	void Update () {
        input();
    }


    void input()
    {
        if (Input.GetMouseButtonDown(0) && mouseIsAvalible)
        {
            mousePressed = true;

            
            mouseClickStart = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);

            Instantiate(areaSelectorPrefab);

        }

        if (Input.GetMouseButtonUp(0))
        {
            mousePressed = false;
            if (mouseIsAvalible)
            {
                spawnObjects(mouseClickStart, Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10));
            }
            
        }
    }


    public void IsEnemy(Toggle toggle)
    {
        isEnemy = toggle.isOn;
        
    }

    public void ChangeOfType(int type)
    {
        if (type == 1)
        {
            shipeType = ShipType.Fighter;
        }
        else if (type == 2)
        {
            shipeType = ShipType.Corvette;
        }
        else if (type == 3)
        {
            shipeType = ShipType.AntiFighterCorvette;
        }

        
    }

    public void mouseAvalibility(bool mouseIsAvalible)
    {
        this.mouseIsAvalible = mouseIsAvalible;
    }

    void spawnObjects(Vector2 start, Vector2 end)
    {

        int size = getShipSize(shipeType);

        ObjectVisual newVisual = null;

        int startX = 0;
        int startY = 0;
        int endX = 0;
        int endY = 0;

        //x
        if (start.x > end.x)
        {
            startX = (int)end.x;
            endX = (int)start.x;
        }
        else
        {
            startX = (int)start.x;
            endX = (int)end.x;
        }

        //y
        if (start.y > end.y)
        {
            startY = (int)end.y;
            endY = (int)start.y;
        }
        else
        {
            startY = (int)start.y;
            endY = (int)end.y;
        }


        if (size > 0)
        {
            for (int x = startX; x < endX; x+= size)
            {
                for (int y = startY; y < endY; y+= size)
                {
                    if (isEnemy)
                    {
                        newVisual = (ObjectVisual)Instantiate(objectVisualPrefab, new Vector3(x, y, 0), new Quaternion(0, 0, 180, 0));
                    }
                    else
                    {
                        newVisual = (ObjectVisual)Instantiate(objectVisualPrefab, new Vector3(x, y, 0), Quaternion.identity);
                    }

                    newVisual.isEnemy = isEnemy;
                    newVisual.shipeType = shipeType;
                    shipLocations.Add(newVisual);

                }
            }
        }
        
    }

    int getShipSize(ShipType type)
    {



        if (type == ShipType.none)
        {
            return -1;
        }
        if (type == ShipType.Fighter)
        {
            return 2;
        }
        if (type == ShipType.Corvette)
        {
            return 10;
        }
        if (type == ShipType.AntiFighterCorvette)
        {
            return 10;
        }

        return -1;

    }

    public void save()
    {
        SaveLevel.Save(this);
    }
}
