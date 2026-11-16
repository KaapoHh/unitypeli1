using UnityEngine;
using UnityEngine.UI; 
public class SurvivalTimer : MonoBehaviour
{
    public float survivalTime = 0f;
    private bool isAlive = true;
    private Campfire campfire;

    public Text timerText; 

    void Start()
    {
        campfire = FindFirstObjectByType<Campfire>();
    }

    void Update()
    {
        if (isAlive)
        {
            survivalTime += Time.deltaTime;

            if (timerText != null)
            {
                timerText.text = "aika: " + Mathf.Floor(survivalTime) + "s";
            }

            if (campfire != null && campfire.currentFireTimer <= 0)
            {
                StopTimer();
            }
        }
    }

    void StopTimer()
    {
        isAlive = false;
        Debug.Log("peli ohi ja nuotio sammui. selvisit ajan: " + Mathf.Floor(survivalTime) + " sekuntia."); 
    }
}