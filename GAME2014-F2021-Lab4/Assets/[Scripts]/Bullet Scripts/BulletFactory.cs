using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Going to make the bullet factory a singleton now, abstract?
/// </summary>
[System.Serializable]
public class BulletFactory
{
    // Step 1. create a private static instance
    private static BulletFactory instance = null;


    // Will now make this a reference to prefabs
    public GameObject enemyBullet;
    public GameObject playerBullet;

    // Game Controller reference
    private GameController gameController;

    // Make the constructor private
    // upon creation of bullet factory, create the bullet factory
    private BulletFactory()
    {
        Initialize();
    }

    // Step 3: make a public static creation method for class access
    public static BulletFactory Instance()
    {
        if (instance == null)
        {
            instance = new BulletFactory();
        }
        return instance;
    }
    private void Initialize()
    {

        // Step 4: create a resources folder
        // Step 5: move prefabs into a resources folder in order to be accessed programmatically
        // create references to prefabs

        enemyBullet = Resources.Load("Prefabs/EnemyBullet") as GameObject;
        playerBullet = Resources.Load("Prefabs/PlayerBullet") as GameObject;

        gameController = GameObject.FindObjectOfType<GameController>();
    }

    public GameObject createBullet(BulletType type = BulletType.ENEMY)
    {
        // now since all bullets are of type bullet, they all inherit from the interface
        
        GameObject temp_bullet = null;
        switch (type)
        {

            // being very explicit with the instantiate function
            case BulletType.ENEMY:
                temp_bullet = MonoBehaviour.Instantiate(enemyBullet);
                break;
            case BulletType.PLAYER:
                temp_bullet = MonoBehaviour.Instantiate(playerBullet);

                break;
        }
        // get the parent transform of the bullet, be the game controller's game object transform
        temp_bullet.transform.parent = gameController.gameObject.transform;
        temp_bullet.SetActive(false);

        return temp_bullet;
    }


}

// note, positives and negatives of singletons
// positive is one stop shop
// negative is that it does 2 things at once, which breaks some principles