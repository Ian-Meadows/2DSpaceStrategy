using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicVehicle : BasicObject {

    public List<BasicVehicle> objectToAttack = new List<BasicVehicle>();

    public int indexOfCurrentAttacker = -1;

    public int moveSpeed;

    bool debugMode = false;

    public PhysicsShape startShape;

    public int health;

    public bool canFire = false;

    public float radius;
    public bool isInLine = false;

    public List<Vector2> projectileFirePoints = new List<Vector2>();
    public BasicProjectile projectilePrefab;
    public int projectileDamage;
    public List<BasicProjectile> projectiles = new List<BasicProjectile>();

    public float fireRate;

    public int kills = 0;

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

        if (debugMode)
        {
            draw();
        }
        

    }

    void draw()
    {

        for (int i = 0; i < shape.lines.Count; i++)
        {
            Debug.DrawLine(new Vector3(shape.lines[i].startX, shape.lines[i].startY, 0), new Vector3(shape.lines[i].endX, shape.lines[i].endY, 0), Color.green);
        }

        
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

    public void Move()
    {

        canFire = false;

        if (transform.position != moveToLocation)
        {
            Vector3 vectorToTarget = moveToLocation - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;

            if (angle < 0)
            {
                angle += 360;
            }
            if (angle > 360)
            {
                angle -= 360;
            }




            if (transform.rotation.eulerAngles.z < angle + 0.5 && transform.rotation.eulerAngles.z > angle - 0.5)
            {
                float step = moveSpeed * Time.deltaTime;

                transform.position = Vector2.MoveTowards(transform.position, moveToLocation, step);

                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 2);
            }
            else
            {

                

                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 2);
            }



        }
    }

    public void checkHealth()
    {
        if (health <= 0)
        {




            Destroy(gameObject);
        }
    }

    public void attack(int attackType)
    {

        if (attackType == 1)
        {
            strafingAttack();
        }
        if (attackType == 2)
        {
            normalAttack();
        }
        if (attackType == 3)
        {
            broadSideAttack();
        }

    }

    void broadSideAttack()
    {

    }

    void normalAttack()
    {
        moveToLocation = transform.position;

        if (indexOfCurrentAttacker < 0 || indexOfCurrentAttacker > objectToAttack.Count - 1)
        {
            indexOfCurrentAttacker = Random.Range(0, objectToAttack.Count);
        }

        if (objectToAttack[indexOfCurrentAttacker] == null)
        {
            objectToAttack.RemoveAt(indexOfCurrentAttacker);
            indexOfCurrentAttacker = -1;
            canFire = false;
        }
        else
        {



            Vector3 vectorToTarget = objectToAttack[indexOfCurrentAttacker].transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;

            if (angle < 0)
            {
                angle += 360;
            }
            if (angle > 360)
            {
                angle -= 360;
            }

            if (Mathf.Pow(objectToAttack[indexOfCurrentAttacker].transform.position.x - transform.position.x, 2) + Mathf.Pow(objectToAttack[indexOfCurrentAttacker].transform.position.y - transform.position.y, 2) > Mathf.Pow(radius, 2))
            {
                canFire = false;

                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, transform.position + (transform.up * 20), step);

                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 2);
            }
            else
            {
                canFire = true;


                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 2);

            }

            if (objectToAttack.Count <= 0)
            {
                canFire = false;
            }



        }
    }

    void strafingAttack()
    {
        moveToLocation = transform.position;

        if (indexOfCurrentAttacker < 0 || indexOfCurrentAttacker > objectToAttack.Count - 1)
        {
            indexOfCurrentAttacker = Random.Range(0, objectToAttack.Count);
        }

        if (objectToAttack[indexOfCurrentAttacker] == null)
        {
            objectToAttack.RemoveAt(indexOfCurrentAttacker);
            indexOfCurrentAttacker = -1;
            canFire = false;
        }
        else
        {



            Vector3 vectorToTarget = objectToAttack[indexOfCurrentAttacker].transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;

            if (angle < 0)
            {
                angle += 360;
            }
            if (angle > 360)
            {
                angle -= 360;
            }

            if (Mathf.Pow(objectToAttack[indexOfCurrentAttacker].transform.position.x - transform.position.x, 2) + Mathf.Pow(objectToAttack[indexOfCurrentAttacker].transform.position.y - transform.position.y, 2) > Mathf.Pow(radius, 2))
            {
                canFire = false;






                if (transform.rotation.eulerAngles.z < angle + 0.5 && transform.rotation.eulerAngles.z > angle - 0.5)
                {
                    float step = moveSpeed * Time.deltaTime;

                    transform.position = Vector2.MoveTowards(transform.position, objectToAttack[indexOfCurrentAttacker].transform.position, step);

                    Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                    transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 2);
                }
                else
                {

                    float step = moveSpeed * Time.deltaTime;
                    transform.position = Vector2.MoveTowards(transform.position, transform.position + (transform.up * 20), step);

                    Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                    transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 2);
                }



            }
            else
            {

                canFire = true;

                if (Mathf.Pow(objectToAttack[indexOfCurrentAttacker].transform.position.x - transform.position.x, 2) +
                    Mathf.Pow(objectToAttack[indexOfCurrentAttacker].transform.position.y - transform.position.y, 2) < Mathf.Pow(radius * .8f, 2))
                {
                    if (transform.rotation.eulerAngles.z < angle + 1 && transform.rotation.eulerAngles.z > angle - 1 && !isInLine)
                    {

                        isInLine = true;

                    }
                    if (!isInLine)
                    {

                        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 4);
                    }

                    float step = moveSpeed * Time.deltaTime;
                    transform.position = Vector2.MoveTowards(transform.position, transform.position + (transform.up * 20), step / 2);
                }
                else
                {

                    isInLine = false;
                    float step = moveSpeed * Time.deltaTime;
                    transform.position = Vector2.MoveTowards(transform.position, transform.position + (transform.up * 20), step / 2);

                    Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                    transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 4);
                }



            }

            if (objectToAttack.Count <= 0)
            {
                canFire = false;
            }
        }
    }

    public void CheckForSelection()
    {


        if (!hasSpawnedSelection && isSelected)
        {
            hasSpawnedSelection = true;
            Selection newSelection = (Selection)Instantiate(playerController.selection, transform.position, Quaternion.identity);
            newSelection.transform.parent = transform;
        }

        if (hasSpawnedSelection && !isSelected)
        {
            hasSpawnedSelection = false;
        }


    }

    public void StartFire(float fireRate)
    {
        this.fireRate = fireRate;
        
        InvokeRepeating("fire", Random.Range(0.0f, 2.0f), fireRate);
    }

    public Vector2 rotateVector(float degrees, Vector2 point)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);
        

        return new Vector2((cos * point.x) - (sin * point.y), (cos * point.y) + (sin * point.x));
    }

    void fire()
    {

        

        if (canFire)
        {
            
            if (projectilePrefab.type != BasicProjectile.Type.Beam)
            {





                foreach (Vector2 points in projectileFirePoints)
                {
                    Vector2 point = rotateVector(transform.rotation.eulerAngles.z, points);

                    BasicProjectile newProjectile = spawnProjectile(new Vector3(transform.position.x + point.x, transform.position.y + point.y, 0),
                        transform.rotation);


                    if (newProjectile != null)
                    {
                        newProjectile.isFriendly = isFriendly;


                        if (indexOfCurrentAttacker > objectToAttack.Count - 1 || indexOfCurrentAttacker < 0)
                        {
                            newProjectile.target = null;
                        }
                        else
                        {
                            newProjectile.target = objectToAttack[indexOfCurrentAttacker];
                        }
                    }

                }


            }
            


        }
        
    }

    BasicProjectile spawnProjectile(Vector3 position, Quaternion rotation)
    {
        
        BasicProjectile projectile = projectiles[0];
        if (projectile.isAlive)
        {
            return null;
        }
        else
        {
            projectile.transform.position = position;
            projectile.transform.rotation = rotation;
            projectile.isAlive = true;


            projectiles.Remove(projectile);
            projectiles.Add(projectile);

            projectile.Started();



            return projectile;

        }


    }

    public override void DestroyObject(BasicObject obj)
    {
        BasicProjectile projectile = obj.GetComponent<BasicProjectile>();

        if (projectile != null)
        {
            projectile.transform.position = new Vector3(0, 0, -100);
            projectile.isAlive = false;

            projectiles.Remove(projectile);
            projectiles.Insert(0, projectile);
        }

    }

    public void createProjectiles(int total, int damage, int speed, float lifeTime)
    {

        if (projectilePrefab.type == BasicProjectile.Type.Beam)
        {
            for (int i = 0; i < projectileFirePoints.Count; i++)
            {
                BasicProjectile newProjectile = Instantiate(projectilePrefab);

                newProjectile.isAlive = false;

                newProjectile.Created(this, damage, speed, lifeTime);
                newProjectile.transform.position = new Vector3(0, 0, -100);

                if (isFriendly)
                {
                    newProjectile.GetComponent<Beam>().OriginalPoint = rotateVector(transform.rotation.eulerAngles.z, projectileFirePoints[i]);
                }
                else
                {
                    newProjectile.GetComponent<Beam>().OriginalPoint = rotateVector(transform.rotation.eulerAngles.z - 180, projectileFirePoints[i]);
                }
                


                projectiles.Add(newProjectile);
            }
        }
        else
        {
            for (int i = 0; i < total; i++)
            {
                BasicProjectile newProjectile = Instantiate(projectilePrefab);

                newProjectile.isAlive = false;

                newProjectile.Created(this, damage, speed, lifeTime);
                newProjectile.transform.position = new Vector3(0, 0, -100);


                if (projectilePrefab.type == BasicProjectile.Type.Beam)
                {

                    newProjectile.GetComponent<Beam>().OriginalPoint = rotateVector(transform.rotation.eulerAngles.z, projectileFirePoints[i]);
                }


                projectiles.Add(newProjectile);
            }
        }

        
    }
}
