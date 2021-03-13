using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicProjectile : BasicObject {

    public int moveSpeed;

    //bool debugMode = false;

    public PhysicsShape startShape;

    public float lifeTime;

    public int damage;

    public enum Type {Laser, Beam, Rocket};

    public Type type;

    public BasicObject target;

    public BasicObject owner;

    public bool isAlive;
    public bool everyOther;


    // Use this for initialization
    void Start()
    {

        if (Random.value > 0.5f)
        {
            everyOther = true;
        }
        else
        {
            everyOther = false;
        }

        if (type == Type.Beam)
        {

        }



    }

    public void Move()
    {
        float step = moveSpeed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, (transform.up * 20) + transform.position, step);
    }

    public void rotateShapePoints(float angle)
    {
        List<Vector2> points = getPoints(startShape);



        angle = Mathf.Deg2Rad * angle;



        for (int i = 0; i < points.Count; i++)
        {
            float x = (points[i].x * Mathf.Cos(angle)) - (points[i].y * Mathf.Sin(angle));
            float y = (points[i].x * Mathf.Sin(angle)) + (points[i].y * Mathf.Cos(angle));



            points[i] = new Vector2(x, y);
        }

        List<PhysicsLine> lines = new List<PhysicsLine>();

        for (int i = 0; i < points.Count; i++)
        {
            if (i + 1 < points.Count)
            {
                lines.Add(new PhysicsLine(points[i].x, points[i].y, points[i + 1].x, points[i + 1].y));
            }
            else
            {
                lines.Add(new PhysicsLine(points[i].x, points[i].y, points[0].x, points[0].y));
            }

        }
        shape.setUpPolygon(lines);




    }

    List<Vector2> getPoints(PhysicsShape s)
    {
        List<Vector2> points = new List<Vector2>();

        List<PhysicsLine> lines = s.lines;


        for (int i = 0; i < lines.Count; i++)
        {
            points.Add(new Vector2(lines[i].startX, lines[i].startY));
        }


        return points;
    }

    public void checkCollision()
    {
        if (isAlive)
        {
            if (target != null)
            {
                physicsController.singleObjectCheck(this, target);
            }
        }


    }

    public override void Started()
    {

        if (type != Type.Beam)
        {
            rotateShapePoints(transform.rotation.eulerAngles.z);
            Invoke("Death", lifeTime);
        }

        

        
    }

    void Death()
    {

        if (owner != null)
        {
            owner.DestroyObject(this);
        }
        else
        {
            Destroy(gameObject);
        }



    }


    public override void TriggerEntered(BasicObject basicObject)
    {


        if (basicObject.isFriendly != isFriendly)
        {
            if (basicObject.GetComponent<BasicVehicle>() != null)
            {
                basicObject.GetComponent<BasicVehicle>().health -= damage;

                if (basicObject.GetComponent<BasicVehicle>().health <= 0)
                {
                    if (owner != null)
                    {
                        owner.GetComponent<BasicVehicle>().kills++;
                    }
                    
                }

            }


            Death();
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
