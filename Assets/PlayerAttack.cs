using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 1.5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TryChopTree();
        }
    }

    void TryChopTree()
    {
        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");

        foreach (GameObject tree in trees)
        {
            float distance = Vector2.Distance(transform.position, tree.transform.position);
            if (distance <= attackRange)
            {
                SpriteRenderer sr = tree.GetComponent<SpriteRenderer>();
                if (sr != null && !sr.enabled) continue;

                Inventory playerInventory = GetComponent<Inventory>();
                if (playerInventory != null)
                {
                    playerInventory.AddWood(1);
                }

                TreeRespawner respawner = tree.GetComponent<TreeRespawner>();
                if (respawner != null)
                {
                    respawner.ChopDown();
                }
                else
                {
                    Destroy(tree);
                }

                break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}