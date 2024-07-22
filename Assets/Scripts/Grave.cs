using System.Linq;
using UnityEngine;

public class Grave : MonoBehaviour
{
    [SerializeField]
    private float range = 3;

    [SerializeField]
    private float timeBeforeZombie = 5;
    private float zombieCounter;
    private bool canSpawnZombie;

    private GameObject zombie;
    public GameObject zombiePrefab;

    // Start is called before the first frame update
    void Start()
    {
        ServiceManager.Instance.Get<OnDirtCorrupted>().Subscribe(HandleCorruption);
    }

    private void OnDestroy()
    {
        ServiceManager.Instance.Get<OnDirtCorrupted>().Unsubscribe(HandleCorruption);
    }

    private void HandleCorruption()
    {
        if (!FindObjectsOfType<Decayable>().Any(x => Vector2.Distance(transform.position, x.transform.position) < range))
        {
            ServiceManager.Instance.Get<OnDirtCorrupted>().Unsubscribe(HandleCorruption);
            canSpawnZombie = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
    private void Update()
    {
        if (zombie) return;
        if (!canSpawnZombie) return;
        zombieCounter += Time.deltaTime;
        if (zombieCounter < timeBeforeZombie) return;

        var direction = Random.insideUnitCircle;
        var hit = Physics2D.Raycast(transform.position, direction, direction.magnitude);
        if (hit) return;

        zombie = Instantiate(zombiePrefab, transform.position + (Vector3)direction, Quaternion.identity);
    }
}
