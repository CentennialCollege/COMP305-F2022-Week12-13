using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttackAction : MonoBehaviour, Action
{
    public bool hasLOS;
    [Range(1, 100)] 
    public int fireDelay = 20;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Transform bulletParent;
    public Vector2 targetOffset;

    // Start is called before the first frame update
    void Awake()
    {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
    }

    // Update is called once per frame
    void Update()
    {
        hasLOS = transform.parent.GetComponentInChildren<PlayerDetection>().LOS;
    }

    void FixedUpdate()
    {
        if(hasLOS && Time.frameCount % fireDelay == 0)
        {
            Execute();
        }
    }

    public void Execute()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity, bulletParent);
        bullet.GetComponent<BulletController>().direction =
            transform.parent.GetComponentInChildren<PlayerDetection>().playerDirectionVector + targetOffset;
    }
}
