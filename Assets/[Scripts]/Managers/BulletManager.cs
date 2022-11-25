using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletManager : MonoBehaviour
{
    [Header("Pool Properties")] 
    public int maxBullets;
    public GameObject bulletPrefab;
    public Transform bulletParent;

    private Queue<GameObject> bulletPool;

    // Start is called before the first frame update
    void Awake()
    {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        bulletPool = new Queue<GameObject>();
    }

    void Start()
    {
        BuildPool();
    }

    /// <summary>
    /// Pre-allocate all the GameObjects in the Pool
    /// </summary>
    private void BuildPool()
    {
        for (var i = 0; i < maxBullets; i++)
        {
            var tempBullet = CreateBullet();
            bulletPool.Enqueue(tempBullet);
        }
    }

    private GameObject CreateBullet()
    {
        var tempBullet = Instantiate(bulletPrefab, Vector2.zero, Quaternion.identity, bulletParent);
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
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}
