using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{

    public float healthCurrent;
    public float healthMaximum;
    public float healthRegenPerTick;
    public float healthRegenTickRate;
    public float healthRegenTickTimeUntil;


    public Slider healthBar;



    // Start is called before the first frame update
    void Start()
    {
        healthBar.maxValue = healthMaximum;
        healthCurrent = healthMaximum;
        UpdateHealthBar();
    }

    public void Update()
    {
        healthBar.transform.rotation = Camera.main.transform.rotation;
    }

    private void OnDeath()
    {
        LevelManager.RemoveCastle(this);
    }

    public void ApplyDamage(float amount)
    {
        if (!SetHealth(healthCurrent - amount))
        {
            OnDeath();
        }
    }

    private bool SetHealth(float value)
    {
        if (value <= 0)
        {
            healthCurrent = 0;
            UpdateHealthBar();
            return false;
        }
        if (value > healthMaximum)
        {
            value = healthMaximum;
        }
        healthCurrent = value;
        UpdateHealthBar();
        return true;
    }


    private void UpdateHealthBar()
    {
        healthBar.value = healthCurrent;
    }
}