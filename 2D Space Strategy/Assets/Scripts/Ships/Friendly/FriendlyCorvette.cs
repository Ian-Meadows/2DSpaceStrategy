using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FriendlyCorvette : BasicVehicle
{


    

    // Use this for initialization
    void Start () {

        moveToLocation = transform.position;

        isSelected = false;

        size = 10;

        health = 300;

        radius = 100;

        moveSpeed = 8;

        playerController = FindObjectOfType<PlayerController>();
        physicsController = FindObjectOfType<PhysicsController>();



        physicsController.addObject(this);
        playerController.addFriendly(this);

        isTrigger = false;
        isFriendly = true;

        shape = new PhysicsShape();
        startShape = new PhysicsShape();

        List<PhysicsLine> lines = new List<PhysicsLine>();

        lines.Add(new PhysicsLine(0.89f, -4, 0.88f, 2.76f));

        lines.Add(new PhysicsLine(0.88f, 2.76f, 0, 4));

        lines.Add(new PhysicsLine(0, 4, -0.88f, 2.75f));

        lines.Add(new PhysicsLine(-0.88f, 2.75f, -0.87f, -4.01f));

        lines.Add(new PhysicsLine(-0.87f, -4.01f, 0.89f, -4));

        startShape.setUpPolygon(lines);
        shape.setUpPolygon(lines);

        createProjectiles(6, 100, 30, 6);

        projectileFirePoints.Add(new Vector2(0, 4));

        StartFire(1.5f);


    }
	
	// Update is called once per frame
	void Update () {

        if (playerController.battleHasStarted)
        {
            if (objectToAttack.Count <= 0)
            {
                Move();
            }
            else
            {
                attack(2);
            }
        }


        CheckForSelection();

        pos = transform.position;

        rotateShapePoints(transform.rotation.eulerAngles.z);

        checkHealth();

    }

    


    
}
