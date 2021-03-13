using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    

    public List<BasicObject> Selected = new List<BasicObject>();

    public List<BasicVehicle> attackersSelected = new List<BasicVehicle>();

    List<BasicVehicle> friendlies = new List<BasicVehicle>();

    public Selection selection;
    public AttackSelector attackSelector;


    public enum MousePressed {Left, Right, None};

    public MousePressed mousePressed = MousePressed.None;

    public Vector2 mouseClickStart;

    public bool hasSelection;

    public Selector selector;

    public bool battleHasStarted;

    public bool hasSpawnedBuildingLocation;

    

    // Use this for initialization
    void Start () {

        
        hasSelection = false;

        battleHasStarted = false;



    }
	
	// Update is called once per frame
	void Update () {

        input();

        


	}

    public void clearSelection()
    {
        for (int i = 0; i < Selected.Count; i++)
        {
            if (Selected[i] != null)
            {
                Selected[i].isSelected = false;
                
            }
        }

        Selected.Clear();
        
        hasSelection = false;
        
    }

    public void clearAttackSelection()
    {
        for (int i = 0; i < attackersSelected.Count; i++)
        {
            if (attackersSelected[i] != null)
            {
                attackersSelected[i].isSelected = false;
                attackersSelected[i] = null;
            }
        }

        attackersSelected.Clear();

        
    }

    void input()
    {
        if (hasSelection && Input.GetMouseButtonDown(1) && mousePressed == MousePressed.None)
        {

            mousePressed = MousePressed.Right;

            mouseClickStart = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);



            Instantiate(attackSelector);

        }

        if (hasSelection && Input.GetMouseButtonUp(1) && mousePressed == MousePressed.Right)
        {
            
            mousePressed = MousePressed.None;

            

        }



        if (Input.GetMouseButtonDown(0))
        {
            mousePressed = MousePressed.Left;

            mouseClickStart = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);

            

            Instantiate(selector);

        }

        if (Input.GetMouseButtonUp(0))
        {
            mousePressed = MousePressed.None;



        }

        

    }

    


    
    public void SetUnits()
    {

        if (hasSelection)
        {
            if (attackersSelected.Count > 0)
            {
                for (int i = 0; i < Selected.Count; i++)
                {
                    if (Selected[i] != null)
                    {

                        Selected[i].GetComponent<BasicVehicle>().indexOfCurrentAttacker = -1;

                        Selected[i].GetComponent<BasicVehicle>().objectToAttack = new List<BasicVehicle>(attackersSelected);


                    }
                }
            }
            else
            {
                moveUnits();
            }
        }
    }

    void moveUnits()
    {
        moveShips();



    }

    

    void moveShips()
    {
        int totalSize = 0;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);

        int maxSize = 0;
        int totalUnits = 0;

        for (int i = 0; i < Selected.Count; i++)
        {
            if (Selected[i] != null)
            {

                if (maxSize < Selected[i].size)
                {
                    maxSize = Selected[i].size;
                }

                totalUnits++;




            }
        }

        totalSize = maxSize * totalUnits;

        float squareRootSize = Mathf.Sqrt(totalSize * 2);

        Vector2 start = new Vector2((squareRootSize / 2), (squareRootSize / 2));

        float x = start.x;
        float y = start.y;


        for (int i = 0; i < Selected.Count; i++)
        {
            if (Selected[i] != null)
            {
                Selected[i].GetComponent<BasicVehicle>().objectToAttack.Clear();
                Selected[i].moveToLocation = new Vector3(x + mousePos.x, y + mousePos.y, 0);

                
                x -= maxSize;

                if (x < -start.x)
                {
                    y -= maxSize;
                    x = start.x;
                }

            }
        }
    }


    public void addFriendly(BasicVehicle friendly)
    {
        friendlies.Add(friendly);
    }

    public void removeFriendly(BasicVehicle friendly)
    {
        friendlies.Remove(friendly);
    }

    public List<BasicVehicle> getFriendlies()
    {
        return friendlies;
    }

    public void StartBattle(GameObject button)
    {
        Destroy(button);
        battleHasStarted = true;
    }
}
