using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static void Save(SpawnerController controller)
    {

        LevelSave levelSave = new LevelSave();
        for (int i = 0; i < controller.shipLocations.Count; i++)
        {
            Ship newShip = getNewShip(controller.shipLocations[i]);

            levelSave.ships.Add(newShip);

        }


        string saveLocation = "C:/Users/Ian/Unity Level Saves";


        if (!Directory.Exists(saveLocation))
        {
            Directory.CreateDirectory(saveLocation);
        }


        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Create((saveLocation + "/Level.bytes"));



        formatter.Serialize(saveFile, levelSave);

        saveFile.Close();



    }

    public static void load()
    {

    }

    


    static Ship getNewShip(ObjectVisual ship)
    {
        Ship newShip = new Ship();

        newShip.x = (int)ship.transform.position.x;
        newShip.y = (int)ship.transform.position.y;


        newShip.isEnemy = ship.isEnemy;

        newShip.ShipType = getShipTypeNumber(ship);


        return newShip;
    }


    static int getShipTypeNumber(ObjectVisual ship)
    {

        

        if (ship.shipeType == SpawnerController.ShipType.Fighter)
        {
            return 1;
        }
        if (ship.shipeType == SpawnerController.ShipType.Corvette)
        {
            return 2;
        }
        if (ship.shipeType == SpawnerController.ShipType.AntiFighterCorvette)
        {
            return 3;
        }
        return -1;
    }

}





[Serializable]
public class LevelSave
{
    public List<Ship> ships = new List<Ship>();
}

[Serializable]
public class Ship
{

    

    public bool isEnemy;
    public int ShipType;

    public int x;
    public int y;
}