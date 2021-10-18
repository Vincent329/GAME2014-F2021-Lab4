using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletBehaviour : BulletBehaviour, Bullet
{
    void Start()
    {
        bulletVelocity = new Vector3(0.0f, 0.1f, 0.0f);
        type = BulletType.PLAYER;
    }

    public int DealDamage()
    {
        return 15;
    }
    //protected virtual override void CheckBounds() 
    //{

    //}
}
