using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public class MapCreator : MonoBehaviour {

    
    public EnemyFighter enemyFighter;
    public EnemyCorvette enemyCorvette;
    public EnemyAntiFighterCorvette enemyAntiFighterCorvette;

    public FriendlyFighter friendlyFighter;
    public FriendlyCorvette friendlyCorvette;
    public FriendlyAntiFighterCorvette friendlyAntiFighterCorvette;

    int friendlyships = 10;
    int enemyShips = 10;

    int enemyDistance = 75;

    public TextAsset level;
    List<Ship> ships;

    bool hasLoadedLevel = false;

    void LevelData()
    {
        BinaryFormatter bf = new BinaryFormatter();



        MemoryStream s = new MemoryStream(level.bytes);

        LevelSave data = (LevelSave)bf.Deserialize(s);

         ships = data.ships;


        s.Close();
    }

    // Use this for initialization
    void Start () {

        



    }
	
	// Update is called once per frame
	void Update () {

        if (level != null && !hasLoadedLevel)
        {
            hasLoadedLevel = true;

            LevelData();



            spawnMap();

        }

	}

    void spawnMap()
    {
        for (int i = 0; i < ships.Count; i++)
        {
            //fighter
            if (ships[i].ShipType == 1)
            {
                if (ships[i].isEnemy)
                {
                    Instantiate(enemyFighter, new Vector3(ships[i].x, ships[i].y, 0), new Quaternion(0, 0, 180, 0));
                }
                else
                {
                    Instantiate(friendlyFighter, new Vector3(ships[i].x, ships[i].y, 0), Quaternion.identity);
                }
            }

            //corvette
            if (ships[i].ShipType == 2)
            {
                if (ships[i].isEnemy)
                {
                    Instantiate(enemyCorvette, new Vector3(ships[i].x, ships[i].y, 0), new Quaternion(0, 0, 180, 0));
                }
                else
                {
                    Instantiate(friendlyCorvette, new Vector3(ships[i].x, ships[i].y, 0), Quaternion.identity);
                }
            }

            //anti fighter corvette
            if (ships[i].ShipType == 3)
            {
                if (ships[i].isEnemy)
                {
                    Instantiate(enemyAntiFighterCorvette, new Vector3(ships[i].x, ships[i].y, 0), new Quaternion(0, 0, 180, 0));
                }
                else
                {
                    Instantiate(friendlyAntiFighterCorvette, new Vector3(ships[i].x, ships[i].y, 0), Quaternion.identity);
                }
            }

        }
    }


    

}
