using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletManager 
{
    /********************** SINGLETON SECTION ******************************/

    // Step 1. - Make the Constructor Private
    private BulletManager()
    {
        Initialize();
    }

    // Step 2. - Define private static instance member
    private static BulletManager m_instance;

    // Step 3. - Include a public static Creational Method named Instance
    public static BulletManager Instance()
    {
        return m_instance ??= new BulletManager();
    }

    /***********************************************************************/

    private int maxBullets;
    private GameObject bulletPrefab;
    private Transform bulletParent;
    private Queue<GameObject> bulletPool;

    void Initialize()
    {
        maxBullets = 50;
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        bulletPool = new Queue<GameObject>();
    }

    /// <summary>
    /// Pre-allocate all the GameObjects in the Pool
    /// </summary>
    public void BuildPool()
    {
        bulletParent = GameObject.FindWithTag("BulletParent").transform;
        for (var i = 0; i < maxBullets; i++)
        {
            var tempBullet = CreateBullet();
            bulletPool.Enqueue(tempBullet);
        }
    }

    public void DestroyPool()
    {
        for (var i = 0; i < bulletPool.Count; i++)
        {
            var tempBullet = bulletPool.Dequeue();
            MonoBehaviour.Destroy(tempBullet);
        }
        bulletPool.Clear();
    }

    private GameObject CreateBullet()
    {
        var tempBullet = MonoBehaviour.Instantiate(bulletPrefab, Vector2.zero, Quaternion.identity, bulletParent);
        tempBullet.SetActive(false);
        return tempBullet;
    }

    public GameObject GetBullet(Vector2 spawn_point)
    {
        var tempBullet = bulletPool.Count < 1 ? CreateBullet() : bulletPool.Dequeue();
        tempBullet.transform.position = spawn_point;
        tempBullet.SetActive(true);
        return tempBullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        var bulletController = bullet.GetComponent<BulletController>();
        bulletController.direction = Vector2.zero;
        bulletController.rigidbody2D.velocity = Vector2.zero;
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }

    public int GetPoolSize()
    {
        return bulletPool.Count;
    }
}
