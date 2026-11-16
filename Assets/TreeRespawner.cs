using System.Collections;
using UnityEngine;

public class TreeRespawner : MonoBehaviour
{
    public float respawnTime = 15f; 
    private SpriteRenderer spriteRenderer;
    private Collider2D treeCollider;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        treeCollider = GetComponent<Collider2D>();
    }

    public void ChopDown()
    {
        StartCoroutine(RespawnRoutine());
    }

    IEnumerator RespawnRoutine()
    {
        if (spriteRenderer != null) spriteRenderer.enabled = false;
        if (treeCollider != null) treeCollider.enabled = false;

        Debug.Log("kasvaa takaisin " + respawnTime + " sekunnin p‰‰st‰");

        yield return new WaitForSeconds(respawnTime);

        if (spriteRenderer != null) spriteRenderer.enabled = true;
        if (treeCollider != null) treeCollider.enabled = true;

    }
}