using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager instance;

    // The players gold balance
    private int balance;

    private void Awake()
    {
        instance = this;
    }

    public static void Give(int amount)
    {
        instance.SetBalance(instance.balance + amount);
    }

    public static bool Take(int amount)
    {
        if(instance.balance < amount)
        {
            return false;
        }
        instance.SetBalance(instance.balance - amount);
        return true;
    }

    private void SetBalance(int value)
    {
        balance = value;

        // Update the GUI
    }
}
