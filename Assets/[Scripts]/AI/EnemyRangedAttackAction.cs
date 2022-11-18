using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttackAction : MonoBehaviour, Action
{
    public bool hasLOS;
    [Range(1, 100)] 
    public int fireDelay = 20;

    // Start is called before the first frame update
    void Start()
    {

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
        Debug.Log("Fire!");
    }
}
