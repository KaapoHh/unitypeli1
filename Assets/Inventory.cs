using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int woodCount = 0;
    public int coins = 0;

    public void AddWood(int amount)
    {
        woodCount += amount;
        Debug.Log("Puita kerätty! Yhteensä repussa: " + woodCount);
    }

    public bool UseWood(int amount)
    {
        if (woodCount >= amount)
        {
            woodCount -= amount;
            Debug.Log("käytettiin " + amount + " puuta ja jäljellä: " + woodCount);
            return true;
        }   
        else
        {
            Debug.Log("ei tarpeeksi puita");
            return false; 
        }
    }
    public void AddCoins(int amount)
    {
        if (amount <= 0) return;
        coins += amount;
        Debug.Log("Coineja saatu! Yhteensä: " + coins);
    }

    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            return true;
        }
        return false;
    }
}