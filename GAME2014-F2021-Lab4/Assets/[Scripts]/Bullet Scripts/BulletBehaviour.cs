using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public BulletType type; // theoretically this can disappear. Can save some code by not caring,
                            // because we're making a new bullet from bulletbehaviour, bulletbehaviour will be abstract
                            // have 2 types of bullet behaviours, player and enemy.


    
    [Header("Bullet Movement")]
   
    public Vector3 bulletVelocity; // keep this
    public Bounds bulletBounds; // player may potentially fire in different directions

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        CheckBounds();
    }

    private void Move()
    {
        // need to discern direction depending whether or not the bullet belongs to a player or enemy
        // depending on our movement
       
       transform.position += bulletVelocity;
          
       
    }

    /// <summary>
    ///  this can be changed
    ///  for this instance, chances are that we are not gonna check the bullet's boundaries the same way between player and enmey 
    /// </summary>
    private void CheckBounds()
    {
        if (transform.position.y < bulletBounds.max || transform.position.y > bulletBounds.min)
        {
            // returning the bullet via instance
            BulletManager.Instance().ReturnBullet(this.gameObject, type);
        }
        // checked the top bounds
    }
}
