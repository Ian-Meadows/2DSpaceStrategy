using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour {

    List<BasicVehicle> enemies = new List<BasicVehicle>();
    List<BasicVehicle> targets = new List<BasicVehicle>();


    BasicVehicle FirstPriority;
    BasicVehicle SecondPriority;
    BasicVehicle ThirdPriority;



    List<BasicVehicle> targetFighters = new List<BasicVehicle>();
    List<BasicVehicle> targetCorvettes = new List<BasicVehicle>();
    List<BasicVehicle> targetAntiFighterCorvettes = new List<BasicVehicle>();

    List<BasicVehicle> fighters = new List<BasicVehicle>();
    List<BasicVehicle> corvettes = new List<BasicVehicle>();
    List<BasicVehicle> antiFighterCorvettes = new List<BasicVehicle>();


    PlayerController playerController;

    // Use this for initialization
    void Start () {

        playerController = FindObjectOfType<PlayerController>();

    }
	
	// Update is called once per frame
	void Update () {

        targets = playerController.getFriendlies();

        sortTargets();

        setTargets();
    }

    void setTargets()
    {

        


        if (playerController.battleHasStarted)
        {

            setPriorityTarget(FirstPriority, 1);
            setPriorityTarget(SecondPriority, 2);
            setPriorityTarget(ThirdPriority, 3);


            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].objectToAttack.Count <= 0)
                {
                    if (enemies[i] != null)
                    {
                        if (enemies[i].GetComponent<EnemyFighter>() != null)
                        {
                            if (targetCorvettes.Count > 0)
                            {
                                enemies[i].objectToAttack = targetCorvettes;
                            }
                            else if(targetFighters.Count > 0)
                            {
                                enemies[i].objectToAttack = targetFighters;
                            }
                            else
                            {
                                enemies[i].objectToAttack = targets;
                            }

                        }
                        if (enemies[i].GetComponent<EnemyCorvette>() != null)
                        {
                            if (targetAntiFighterCorvettes.Count > 0)
                            {
                                enemies[i].objectToAttack = targetAntiFighterCorvettes;
                            }
                            else
                            {
                                enemies[i].objectToAttack = targets;
                            }
                        }
                        if (enemies[i].GetComponent<EnemyAntiFighterCorvette>() != null)
                        {
                            if (targetFighters.Count > 0)
                            {
                                enemies[i].objectToAttack = targetFighters;
                            }
                            else
                            {
                                enemies[i].objectToAttack = targets;
                            }
                            
                        }


                        enemies[i].indexOfCurrentAttacker = -1;

                    }
                    
                }
            }
        }

        
    }


    void setPriorityTarget(BasicVehicle priority, int num)
    {
        if (priority != null)
        {
            if (priority.GetComponent<FriendlyFighter>() != null)
            {
                if (num - 1 < antiFighterCorvettes.Count)
                {
                    if (antiFighterCorvettes[num - 1] != null)
                    {
                        List<BasicVehicle> p = new List<BasicVehicle>();
                        p.Add(priority);
                        antiFighterCorvettes[num - 1].objectToAttack = p;
                    }
                }

            }
            if (priority.GetComponent<FriendlyCorvette>() != null)
            {
                List<BasicVehicle> p = new List<BasicVehicle>();
                p.Add(priority);
                for (int i = (num - 1) * 20; i < ((num - 1) * 20) + 20; i++)
                {
                    if (i < fighters.Count)
                    {
                        if (fighters[i] != null)
                        {

                            fighters[i].objectToAttack = p;
                        }
                    }
                }

            }
            if (priority.GetComponent<FriendlyAntiFighterCorvette>() != null)
            {
                if (num - 1 < corvettes.Count)
                {
                    if (corvettes[num - 1] != null)
                    {
                        List<BasicVehicle> p = new List<BasicVehicle>();
                        p.Add(priority);
                        corvettes[num - 1].objectToAttack = p;
                    }
                }
            }
        }
        
    }


    void sortTargets()
    {

        targetFighters.Clear();
        targetCorvettes.Clear();
        targetAntiFighterCorvettes.Clear();

        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] != null)
            {
                if (targets[i].GetComponent<FriendlyFighter>() != null)
                {
                    targetFighters.Add(targets[i]);
                }
                if (targets[i].GetComponent<FriendlyCorvette>() != null)
                {
                    targetCorvettes.Add(targets[i]);
                }
                if (targets[i].GetComponent<FriendlyAntiFighterCorvette>() != null)
                {
                    targetAntiFighterCorvettes.Add(targets[i]);
                }
            }
            
        }

        BasicVehicle best = null;

        //1st
        for (int i = 0; i < targets.Count; i++)
        {
            if (best == null)
            {
                best = targets[i];
            }
            else
            {
                if (best.kills < targets[i].kills)
                {
                    best = targets[i];
                }
            }

        }

        FirstPriority = best;
        best = null;

        //2nd

        for (int i = 0; i < targets.Count; i++)
        {
            if (best == null)
            {
                if (targets[i] != FirstPriority)
                {
                    best = targets[i];
                }
                
            }
            else
            {
                if (best.kills < targets[i].kills && targets[i] != FirstPriority)
                {
                    best = targets[i];
                }
            }

        }

        SecondPriority = best;
        best = null;


        //3rd
        for (int i = 0; i < targets.Count; i++)
        {
            if (best == null)
            {
                if (targets[i] != FirstPriority && targets[i] != SecondPriority)
                {
                    best = targets[i];
                }
                
            }
            else
            {
                if (best.kills < targets[i].kills && targets[i] != FirstPriority && targets[i] != SecondPriority)
                {
                    best = targets[i];
                }
            }

        }

        ThirdPriority = best;
        

    }

    public void addEnemy(BasicVehicle enemy)
    {
        enemies.Add(enemy);


        if (enemy.GetComponent<EnemyFighter>() != null)
        {
            fighters.Add(enemy);
        }
        if (enemy.GetComponent<EnemyCorvette>() != null)
        {
            corvettes.Add(enemy);
        }
        if (enemy.GetComponent<EnemyAntiFighterCorvette>() != null)
        {
            antiFighterCorvettes.Add(enemy);
        }

    }

    public void removeEnemy(BasicVehicle enemy)
    {
        enemies.Remove(enemy);
        if (enemy.GetComponent<EnemyFighter>() != null)
        {
            fighters.Remove(enemy);
        }
        if (enemy.GetComponent<EnemyCorvette>() != null)
        {
            corvettes.Remove(enemy);
        }
        if (enemy.GetComponent<EnemyAntiFighterCorvette>() != null)
        {
            antiFighterCorvettes.Remove(enemy);
        }
    }
}
