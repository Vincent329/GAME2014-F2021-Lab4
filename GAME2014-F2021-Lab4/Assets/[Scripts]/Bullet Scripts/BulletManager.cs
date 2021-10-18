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

    // step 4, need another way to instantiate our bullet
    // make it a singleton? how do I reference the factory? Can't use a get component because it's Monobehaviour type
    public Queue<GameObject> enemyBulletPool;
    public Queue<GameObject> playerBulletPool;

    public int enemyBulletNumber;
    public int playerBulletNumber;

    // private reference to the bullet factory
    private BulletFactory factory;

    // we need a replacement for start
    private void Initialize()
    {
        enemyBulletPool = new Queue<GameObject>(); // creates an empty enemy bullet Queue
        playerBulletPool = new Queue<GameObject>(); // CREATE an empty player bullet Queue -> NEED THIS

        // find an object of type bullet factory in the hierarchy
        factory = GameObject.FindObjectOfType<BulletFactory>(); // get he component of the bullet factory
    }

    private void AddBullet(BulletType type = BulletType.ENEMY)
    {
        var temp_bullet = factory.createBullet(type);
        switch (type)
        {
            case (BulletType.ENEMY):
                enemyBulletPool.Enqueue(temp_bullet);
                enemyBulletNumber++;
            break;
            case (BulletType.PLAYER):
                playerBulletPool.Enqueue(temp_bullet);
                playerBulletNumber++;
                break;
        } 
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
        switch (type)
        {
            case (BulletType.ENEMY):
                if (enemyBulletPool.Count < 1)
                {
                    AddBullet(); // this only adds enemy bullets, because it looks at a very specific bullet prefab
                }
                temp_bullet = enemyBulletPool.Dequeue();
                temp_bullet.transform.position = spawnPosition;
                temp_bullet.SetActive(true);
                break;
            case (BulletType.PLAYER):
                if (playerBulletPool.Count < 1)
                {
                    AddBullet(BulletType.PLAYER); // this only adds PLAYER bullets, because it looks at a very specific bullet prefab
                }
                temp_bullet = playerBulletPool.Dequeue();
                temp_bullet.transform.position = spawnPosition;
                temp_bullet.SetActive(true);
                break;
        }
        
        return temp_bullet;
    }

    /// <summary>
    /// This method returns a bullet back into the bullet pool
    /// </summary>
    /// <param name="returnedBullet"></param>
    public void ReturnBullet(GameObject returnedBullet, BulletType type = BulletType.ENEMY)
    {
        returnedBullet.SetActive(false);

        switch (type)
        {
            case (BulletType.ENEMY):
                enemyBulletPool.Enqueue(returnedBullet);
                break;
            case (BulletType.PLAYER):
                playerBulletPool.Enqueue(returnedBullet);
                break;
        }
        
    }
}
