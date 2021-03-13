using UnityEngine;
using System.Collections;

public class Beam : BasicProjectile
{




    LineRenderer line;

    float time = 0;


    public Vector2 OriginalPoint;


	// Update is called once per frame
	void Update () {


        if (owner != null)
        {
            BasicVehicle basicVehicle = owner.GetComponent<BasicVehicle>();




            Vector2 point = basicVehicle.rotateVector(owner.transform.rotation.eulerAngles.z, OriginalPoint);


            if (basicVehicle.canFire)
            {

                if (basicVehicle.indexOfCurrentAttacker > basicVehicle.objectToAttack.Count - 1 || basicVehicle.indexOfCurrentAttacker < 0)
                {
                    target = null;
                }
                else
                {
                    target = basicVehicle.objectToAttack[basicVehicle.indexOfCurrentAttacker];
                }


                if (target != null && owner != null)
                {
                    line.SetPosition(0, new Vector3(owner.transform.position.x + point.x, owner.transform.position.y + point.y, 1));
                    line.SetPosition(1, new Vector3(target.transform.position.x, target.transform.position.y, 1));
                }
                else
                {
                    line.SetPosition(0, new Vector3(0, 0, -100));
                    line.SetPosition(1, new Vector3(0, 0, -100));
                }


                if (target != null)
                {
                    time += Time.deltaTime;
                    if (lifeTime <= time)
                    {
                        target.GetComponent<BasicVehicle>().health -= damage;

                        if (target.GetComponent<BasicVehicle>().health <= 0)
                        {
                            owner.GetComponent<BasicVehicle>().kills++;
                        }

                        time = 0;
                    }
                }


            }
            else
            {



                line.SetPosition(0, new Vector3(0, 0, -100));
                line.SetPosition(1, new Vector3(0, 0, -100));
            }
        }
        else
        {
            Destroy(gameObject);
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

        line = GetComponent<LineRenderer>();

        isTrigger = false;


        




        playerController = FindObjectOfType<PlayerController>();
        physicsController = FindObjectOfType<PhysicsController>();

    }
}
