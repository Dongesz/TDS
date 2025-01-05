using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public GameObject prefab;
    public List<Transform> spawns = new List<Transform>();
    public int amount = 2;
    public int x = 6;

    public int randomindex;

    private void Start()
    {
        for (int i = 0; i < amount; i++)
        {
            randomindex = Random.Range(1, x);
            Instantiate(prefab, spawns[randomindex-1].transform.position, Quaternion.identity);
            spawns.RemoveAt(randomindex - 1);
            x -= 1;
        }
    }

}
