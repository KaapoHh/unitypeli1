using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CampfireRepair : MonoBehaviour
{
    [Header("Viittaukset")]
    public CampfireHealth campfireHealth;
    public Inventory inventory;

    [Header("Korjauksen asetukset")]
    public int repairCost = 10;      
    public int healAmount = 25;      
    public KeyCode repairKey = KeyCode.R;

    [Header("Valinnainen UI")]
    public Button repairButton;
    public TMP_Text repairInfoText;

    void Start()
    {
        if (campfireHealth == null)
        {
            campfireHealth = FindFirstObjectByType<CampfireHealth>();
        }

        if (inventory == null)
        {
            inventory = FindFirstObjectByType<Inventory>();
        }

        if (repairButton != null)
        {
            repairButton.onClick.AddListener(TryRepairCampfire);
        }

        UpdateRepairUI();
    }

    void Update()
    {
        if (repairKey != KeyCode.None && Input.GetKeyDown(repairKey))
        {
            TryRepairCampfire();
        }

        UpdateRepairUI();
    }

    public void TryRepairCampfire()
    {
        if (campfireHealth == null || inventory == null)
        {
            Debug.LogWarning("CampfireRepair: CampfireHealth tai Inventory puuttuu!");
            return;
        }

        if (campfireHealth.IsFullHealth())
        {
            Debug.Log("Nuotio on jo täydessä kunnossa!");
            return;
        }

        if (inventory.UseWood(repairCost))
        {
            campfireHealth.Heal(healAmount);
            UpdateRepairUI();
        }
    }

    void UpdateRepairUI()
    {
        if (repairInfoText != null)
        {
            repairInfoText.text = "Korjaa (" + repairCost + ") -> +" + healAmount + " HP";
        }

        if (repairButton != null && campfireHealth != null && inventory != null)
        {
            bool canAfford = inventory.woodCount >= repairCost;
            bool needsHeal = !campfireHealth.IsFullHealth();
            repairButton.interactable = canAfford && needsHeal;
        }
    }
}