using UnityEngine;
using TMPro;

public class WeaponSystem : MonoBehaviour
{
    public enum WeaponSlot
    {
        BasicAttack = 1,
        Pistol = 2
    }

    [Header("Aseet")]
    public WeaponSlot currentWeapon = WeaponSlot.BasicAttack;
    public bool pistolUnlocked = false;

    [Header("Perusisku (1)")]
    public int basicDamage = 10;
    public float basicCooldown = 0.4f;
    public float basicClickRadius = 0.9f;

    [Header("Pistooli (2)")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public int pistolDamage = 25;
    public float bulletSpeed = 16f;
    public float pistolCooldown = 0.25f;

    private float nextAttackTime = 0f;

    [Header("UI")]
    public TMP_Text weaponText;
    void Start()
    {
        if (firePoint == null)
        {
            firePoint = transform;
        }

        LogCurrentWeapon();
    }

    void Update()
    {
        HandleWeaponSwitch();
        HandleAttackInput();
    }

    void UpdateWeaponUI()
    {
        if (weaponText == null) return;

        bool isPistol = currentWeapon.ToString().ToLower().Contains("pistol");
        weaponText.text = isPistol ? "Ase: Pistooli (2)" : "Ase: Perusisku (1)";
    }

    void HandleWeaponSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            currentWeapon = WeaponSlot.BasicAttack;
            LogCurrentWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            if (pistolUnlocked)
            {
                currentWeapon = WeaponSlot.Pistol;
                LogCurrentWeapon();
            }
            else
            {
                Debug.Log("Pistoolia (2) ei ole vielä avattu!");
            }
        }
    }

    public void UnlockPistol(bool autoEquip = true)
    {
        if (!pistolUnlocked)
        {
            pistolUnlocked = true;
            Debug.Log("ASE PUDOTETTU: Pistooli avattu! (1 = Perusisku, 2 = Pistooli)");
        }

        if (autoEquip)
        {
            currentWeapon = WeaponSlot.Pistol;
            LogCurrentWeapon();
        }
    }

    void LogCurrentWeapon()
    {
        if (currentWeapon == WeaponSlot.Pistol && pistolUnlocked)
        {
            Debug.Log("Käytössä oleva ase: Pistooli (2)");
        }
        else
        {
            Debug.Log("Käytössä oleva ase: Perusisku (1)");
        }
    }

    void HandleAttackInput()
    {
        bool isPistol = (currentWeapon == WeaponSlot.Pistol && pistolUnlocked);
        float cooldown = isPistol ? pistolCooldown : basicCooldown;

        if (Time.time < nextAttackTime) return;
        if (!Input.GetMouseButtonDown(0)) return;

        nextAttackTime = Time.time + cooldown;

        if (isPistol)
        {
            ShootPistol();
        }
        else
        {
            PerformBasicAttack();
        }
    }

    void ShootPistol()
    {
        if (bulletPrefab == null)
        {
            Debug.LogWarning("Yritit ampua pistoolilla, mutta Bullet Prefab puuttuu Inspectorista!");
            return;
        }

        Vector2 spawnPos = firePoint != null ? (Vector2)firePoint.position : (Vector2)transform.position;
        Vector2 shootDir = GetShootDirection(spawnPos);

        GameObject bulletObj = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.Initialize(shootDir, pistolDamage, bulletSpeed);
        }

        Debug.Log($"Ammuin pistoolilla! (Vahinko: {pistolDamage})");
    }

    Vector2 GetShootDirection(Vector2 origin)
    {
        Camera cam = Camera.main;
        if (cam != null)
        {
            Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
            mouseWorld.z = 0f;
            Vector2 dir = (Vector2)mouseWorld - origin;
            if (dir.sqrMagnitude > 0.0001f)
            {
                return dir.normalized;
            }
        }

        return Vector2.right;
    }

    void PerformBasicAttack()
    {
        EnemyBase target = FindTargetEnemyByClick(basicClickRadius);
        if (target != null)
        {
            target.TakeDamage(basicDamage);
            Debug.Log($"Iskin perusiskulla vihollista! (Vahinko: {basicDamage})");
        }
        else
        {
            Debug.Log("Iskin perusiskulla (ei vihollista osuma-alueella).");
        }
    }

    EnemyBase FindTargetEnemyByClick(float radius)
    {
        Camera cam = Camera.main;
        if (cam == null) return null;

        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        Collider2D directHit = Physics2D.OverlapPoint(mouseWorld);
        if (directHit != null)
        {
            EnemyBase enemy = directHit.GetComponent<EnemyBase>();
            if (enemy == null) enemy = directHit.GetComponentInParent<EnemyBase>();
            if (enemy != null) return enemy;
        }

        Collider2D[] nearby = Physics2D.OverlapCircleAll(mouseWorld, radius);
        EnemyBase closest = null;
        float bestDist = Mathf.Infinity;

        foreach (Collider2D col in nearby)
        {
            EnemyBase e = col.GetComponent<EnemyBase>();
            if (e == null) e = col.GetComponentInParent<EnemyBase>();
            if (e != null)
            {
                float d = Vector2.Distance(mouseWorld, e.transform.position);
                if (d < bestDist)
                {
                    bestDist = d;
                    closest = e;
                }
            }
        }
        return closest;
    }
}