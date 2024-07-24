using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Greens : MonoBehaviour
{
    [SerializeField]
    private Rect spawnZone;
    [SerializeField]
    private int graveCount = 3;
    [SerializeField]
    private GameObject[] gravePrefabs;

    private Rect SpawnZone => new((Vector2)transform.position + spawnZone.position, spawnZone.size);

    private void Start()
    {
        List<GameObject> graves = new();
        var grave = Instantiate(gravePrefabs.RandomChoice(), (Vector2)transform.position - SpawnZone.size * 0.5f, Quaternion.identity);
        for (int i = 0; i < 1000000; i++)
        {
            if (graves.Count >= graveCount) break;
            var source = graves.RandomChoice();
            break;
        }
    }

    private void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR
        UnityEditor.Handles.DrawSolidRectangleWithOutline(SpawnZone, new(0, 0, 0, 0), Color.white);
#endif
    }
}