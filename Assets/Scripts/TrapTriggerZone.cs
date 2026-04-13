using UnityEngine;

public class TrapTriggerZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        GameObject[] traps = GameObject.FindGameObjectsWithTag("FollowingTrap");

        Debug.Log("Player entered trap trigger zone. Activating traps...");
        Debug.Log($"Found {traps.Length} traps to activate.");

        foreach (GameObject trap in traps)
        {
            FollowingTrap ft = trap.GetComponent<FollowingTrap>();
            if (ft != null)
            {
                ft.enabled = true; // activate script
                ft.toMove = 1;     // allow movement
            }
            Renderer[] renderers = trap.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in renderers)
            {
                r.enabled = true; // make visible
            }
        }

        gameObject.SetActive(false);
    }
}