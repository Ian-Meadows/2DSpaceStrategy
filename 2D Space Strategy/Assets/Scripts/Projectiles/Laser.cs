using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Laser : BasicProjectile {


    
	// Update is called once per frame
	void Update () {

        if (isAlive)
        {
            Move();
            pos = transform.position;

            if (everyOther)
            {
                everyOther = false;
                checkCollision();
            }
            else
            {
                everyOther = true;
            }
            
        }
        

        
    }

    public override void Created(BasicObject owner, int damage, int speed, float lifeTime)
    {
        isSelected = false;

        isTrigger = true;

        this.owner = owner;

        this.damage = damage;
        moveSpeed = speed;

        this.lifeTime = lifeTime;



        isTrigger = false;


        shape = new PhysicsShape();
        startShape = new PhysicsShape();

        List<PhysicsLine> lines = new List<PhysicsLine>();

        lines.Add(new PhysicsLine(0.04f, 0.36f, -0.08f, 0.36f));

        lines.Add(new PhysicsLine(-0.08f, 0.36f, -0.08f, -0.24f));

        lines.Add(new PhysicsLine(-0.08f, -0.24f, 0.03f, -0.24f));

        lines.Add(new PhysicsLine(0.03f, -0.24f, 0.04f, 0.36f));

        startShape.setUpPolygon(lines);
        shape.setUpPolygon(lines);


        playerController = FindObjectOfType<PlayerController>();
        physicsController = FindObjectOfType<PhysicsController>();

    }












}
