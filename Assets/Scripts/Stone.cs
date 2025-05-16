using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public GameObject prefab;
    public List<Transform> spawns = new List<Transform>();
    public int amount = 2;

    private bool hasSpawned = false;

    public void InitializeSpawns(List<Transform> newSpawns)
    {
        spawns = new List<Transform>(newSpawns);
        SpawnStones(); // Csak akkor hívjuk, ha biztosan van spawn lista
    }

    public void SpawnStones()
    {
        if (spawns == null || spawns.Count == 0 || hasSpawned) return;

        List<Transform> copy = new List<Transform>(spawns);
        int count = copy.Count;
        amount = Mathf.Min(amount, count); // Ne próbáljunk több követ spawnolni, mint ahány pozíciónk van

        for (int i = 0; i < amount; i++)
        {
            int randomindex = Random.Range(0, count);
            Transform pos = copy[randomindex];
            copy.RemoveAt(randomindex);
            count--;

            GameObject stoneObj = Instantiate(prefab, pos.position, Quaternion.identity);

            Plot plot = pos.GetComponent<Plot>();
            if (plot != null)
            {
                plot.SetTower(stoneObj); // Jelzi a plotnak, hogy foglalt
            }
        }

        hasSpawned = true;
    }
}
