using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [Header("Bullet Movement")]
    [Range(0.0f, 0.5f)]
    public float speed;
    public Bounds bulletBounds;
    public BulletDirection direction;

    private BulletManager bulletManager;
    private Vector3 bulletVelocity;

    // Start is called before the first frame update
    void Start()
    {
        bulletManager = GameObject.FindObjectOfType<BulletManager>();

        switch (direction)
        {
            case BulletDirection.DOWN:
                bulletVelocity = new Vector3(0.0f, -speed, 0.0f);
                break;
            case BulletDirection.UP:
                bulletVelocity = new Vector3(0.0f, speed, 0.0f);
                break;
            case BulletDirection.RIGHT:
                bulletVelocity = new Vector3(speed, 0.0f, 0.0F);
                break;
            case BulletDirection.LEFT:
                bulletVelocity = new Vector3(-speed, 0.0f, 0.0F);
                break;
        }
    }

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

    private void CheckBounds()
    {
        if (transform.position.y < bulletBounds.max || transform.position.y > bulletBounds.min)
        {
            bulletManager.ReturnBullet(this.gameObject);
        }
        // checked the top bounds
    }
}
