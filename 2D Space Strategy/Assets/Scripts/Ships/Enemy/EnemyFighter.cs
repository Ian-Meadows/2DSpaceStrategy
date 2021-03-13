using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyFighter : BasicVehicle {


    

    // Use this for initialization
    void Start () {

        moveToLocation = transform.position;

        isSelected = false;

        size = 3;

        health = 10;

        radius = 10;

        moveSpeed = 10;

        playerController = FindObjectOfType<PlayerController>();
        physicsController = FindObjectOfType<PhysicsController>();

        

        physicsController.addObject(this);
        FindObjectOfType<EnemyController>().addEnemy(this);


        isTrigger = false;
        isFriendly = false;

        shape = new PhysicsShape();
        startShape = new PhysicsShape();

        List<PhysicsLine> lines = new List<PhysicsLine>();

        lines.Add(new PhysicsLine(0.88f, 0.52f, 0.01f, 1));

        lines.Add(new PhysicsLine(0.01f, 1, -0.88f, 0.49f));

        lines.Add(new PhysicsLine(-0.88f, 0.49f, -0.76f, -1));

        lines.Add(new PhysicsLine(-0.76f, -1, 0.77f, -1.01f));

        lines.Add(new PhysicsLine(0.77f, -1.01f, 0.88f, 0.52f));

        startShape.setUpPolygon(lines);
        shape.setUpPolygon(lines);

        createProjectiles(2, 1, 12, 2);

        projectileFirePoints.Add(new Vector2(0, 1));

        StartFire(1);

    }
	
	// Update is called once per frame
	void Update () {

        if (objectToAttack.Count <= 0)
        {
            Move();
        }
        else
        {
            attack(1);
        }



        pos = transform.position;

        rotateShapePoints(transform.rotation.eulerAngles.z);

        checkHealth();

    }

    


}
