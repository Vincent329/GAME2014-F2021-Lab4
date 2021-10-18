using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// Bullet Factory now a singleton
/// </summary>
[System.Serializable]
public class BulletManager
{
    // Step 1: Create a private static instance of the bullet manager
    private static BulletManager instance = null;

    // Step 2: private default constructor
    private BulletManager()
    {
        // setup the bullet manager
        Initialize();
    }

    // Step 3: Create a public static creational method for the class, ;like a portal
    public static BulletManager Instance()
    {
        if (instance == null)
        {
            instance = new BulletManager();
        }
        return instance;

        // now create the factory as BulletManager::Instance().createfactory();

    }

    // GOOD IDEA TO USE A LIST RATHER THAN AN ARRAY
    public List<Queue<GameObject>> bulletPools;

    // step 4, need another way to instantiate our bullet
    // make it a singleton? how do I reference the factory? Can't use a get component because it's Monobehaviour type

    // we need a replacement for start
    private void Initialize()
    {
        // deprecated
        //enemyBulletPool = new Queue<GameObject>(); // creates an empty enemy bullet Queue
        //playerBulletPool = new Queue<GameObject>(); // CREATE an empty player bullet Queue -> NEED THIS
        bulletPools = new List<Queue<GameObject>>();

        // instantiate  new queue collections based on number of bullet types
        for (int count = 0; count < (int)BulletType.NUM_OF_BULLET_TYPES; count++)
        {
            bulletPools.Add(new Queue<GameObject>()); // remember that it's a LIST, not an ARRAY
        }

        // find an object of type bullet factory in the hierarchy
        // now what are we gonna do
    }

    // change things to account for the bullet factory instance
    private void AddBullet(BulletType type = BulletType.ENEMY)
    {
        // call the singleton of the bullet factory
        var temp_bullet = BulletFactory.Instance().createBullet(type);
        bulletPools[(int)type].Enqueue(temp_bullet);
        // CHANGE: depending on the bullet type, we create the queues

    }

    /// <summary>
    /// This method removes a bullet prefab from the bullet pool
    /// and returns a reference to it.
    /// </summary>
    /// <param name="spawnPosition"></param>
    /// <returns></returns>
    public GameObject GetBullet(Vector2 spawnPosition, BulletType type = BulletType.ENEMY)
    {
        GameObject temp_bullet = null;

        if (bulletPools[(int)type].Count < 1)
        {
            AddBullet(type);
        }
        // get the bullet from the queue
        temp_bullet = bulletPools[(int)type].Dequeue();

        temp_bullet.transform.position = spawnPosition;
        temp_bullet.SetActive(true);
        
        return temp_bullet;
    }

    /// <summary>
    /// This method returns a bullet back into the bullet pool
    /// </summary>
    /// <param name="returnedBullet"></param>
    public void ReturnBullet(GameObject returnedBullet, BulletType type = BulletType.ENEMY)
    {
        returnedBullet.SetActive(false);

        // depending on the type of the bullet, return it back to its respective bullet pool
        bulletPools[(int)type].Enqueue(returnedBullet);
        
    }
}
