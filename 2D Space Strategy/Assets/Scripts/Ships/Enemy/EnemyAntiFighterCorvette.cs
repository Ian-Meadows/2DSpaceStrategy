using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAntiFighterCorvette : BasicVehicle {

	// Use this for initialization
	void Start () {

        moveToLocation = transform.position;

        isSelected = false;

        size = 10;

        health = 200;

        radius = 25;

        moveSpeed = 8;

        playerController = FindObjectOfType<PlayerController>();
        physicsController = FindObjectOfType<PhysicsController>();



        physicsController.addObject(this);
        FindObjectOfType<EnemyController>().addEnemy(this);


        isTrigger = false;
        isFriendly = false;

        shape = new PhysicsShape();
        startShape = new PhysicsShape();

        shape = new PhysicsShape();
        startShape = new PhysicsShape();

        List<PhysicsLine> lines = new List<PhysicsLine>();

        lines.Add(new PhysicsLine(0, 4, 1, 3.6f));
        lines.Add(new PhysicsLine(1, 3.6f, 1.3f, -4));
        lines.Add(new PhysicsLine(1.3f, -4, -1.3f, -4));
        lines.Add(new PhysicsLine(-1.3f, -4, -1, 3.6f));
        lines.Add(new PhysicsLine(-1, 3.6f, 0, 4));

        startShape.setUpPolygon(lines);
        shape.setUpPolygon(lines);

        projectileFirePoints.Add(new Vector2(0, 4));

        createProjectiles(1, 10, 35, 1);






        StartFire(0.5f);

    }
	
	// Update is called once per frame
	void Update () {


        if (objectToAttack.Count <= 0)
        {
            Move();
        }
        else
        {
            attack(2);
        }



        pos = transform.position;

        rotateShapePoints(transform.rotation.eulerAngles.z);

        checkHealth();

    }
}
