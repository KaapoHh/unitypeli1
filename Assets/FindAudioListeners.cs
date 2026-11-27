using UnityEngine;

public class FindAudioListeners : MonoBehaviour
{
    void Start()
    {
        AudioListener[] listeners = FindObjectsByType<AudioListener>(FindObjectsSortMode.None);
        Debug.Log($"AudioListener-komponentteja löytyi {listeners.Length} kpl:");
        foreach (AudioListener listener in listeners)
        {
            Debug.Log($"- {listener.gameObject.name} (aktiivinen: {listener.gameObject.activeInHierarchy}, enabled: {listener.enabled})");
        }
    }
}