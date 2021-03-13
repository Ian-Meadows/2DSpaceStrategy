using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BasicObject : MonoBehaviour {

    public bool isSelected;

    public bool hasSpawnedSelection;

    public int size;

    public Vector3 moveToLocation;

    public PlayerController playerController;
    public PhysicsController physicsController;

    public Vector2 pos;

    public bool isTrigger;

    public PhysicsShape shape;

    public List<BasicObject> objectsInside;

    public bool isFriendly;

    

    public virtual void TriggerEntered(BasicObject basicObject)
    {

    }

    public virtual void TriggerExited(BasicObject basicObject)
    {

    }

    public virtual void Created(BasicObject owner, int damage, int speed, float lifeTime)
    {

    }

    public virtual void Started()
    {

    }

    public virtual void Destroyed()
    {

    }

    public virtual void DestroyObject(BasicObject owner)
    {

    }

    public virtual Vector2 getPos()
    {
        return pos;
    }

}
