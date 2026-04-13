using System.Collections;
using UnityEngine;

public class TrapSpawner : MonoBehaviour
{
    [SerializeField] private GameObject trapPrefab;

    [SerializeField] private float spawnInterval = 0.1f;

    [SerializeField] private float minX = -10f;
    [SerializeField] private float maxX = 10f;

    [SerializeField] private float minZ = -10f;
    [SerializeField] private float maxZ = 10f;

    [SerializeField] private float groundY = 1f;

    void Start()
    {
        Debug.Log("Spawner started");
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnTrap();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnTrap()
    {
        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);

        Vector3 spawnPos = new Vector3(x, groundY, z);

        GameObject trap = Instantiate(trapPrefab, spawnPos, Quaternion.identity);

        // Debug.Log("TRAP SPAWNED at: " + spawnPos);
        // Debug.Log("ActiveSelf: " + trap.activeSelf);
        // Debug.Log("Position: " + trap.transform.position);

        FollowingTrap ft = trap.GetComponent<FollowingTrap>();

        if (ft != null)
        {
            ft.toMove = 1;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                ft.playerToFollow = player.transform;

            GameObject ground = GameObject.FindGameObjectWithTag("Ground");
            if (ground != null)
                ft.groundTransform = ground.transform;

            

            Renderer[] renderers = trap.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in renderers)
            {
                r.enabled = true;
            }
        }
    }
}