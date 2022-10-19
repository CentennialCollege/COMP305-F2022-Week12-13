using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HealthBarController : MonoBehaviour
{
    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = transform.GetChild(0).GetComponent<Slider>();
        ResetHealthBar();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        healthBar.value -= damage;
        if (healthBar.value < 0)
        {
            healthBar.value = 0;
        }
    }

    public void ResetHealthBar()
    {
        healthBar.value = 100;
    }
}
