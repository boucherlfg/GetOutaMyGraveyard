using UnityEngine;

public class Decayable : MonoBehaviour
{
    [SerializeField]
    private bool destroyWhenDecayed = false;
    [SerializeField]
    private GameObject replacement;
    [SerializeField]
    private Sprite[] decaySteps;
    [SerializeField]
    private int decayIndex;
    [SerializeField]
    private float timeBeforeRestore = 30f;
    [SerializeField]
    private float timeBeforeDecay = 1f;
    private float decayCounter = 0;
    private float restoreCounter = 0;
    [SerializeField]
    private float range = 0.4f;
    private SpriteRenderer _renderer;

    public int DecayIndex => decayIndex;
    public int MaximumDecayIndex => decaySteps.Length - 1;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        ServiceManager.Instance.Get<OnWandActivated>().Subscribe(HandleWandActivated);
    }

    private void OnDestroy()
    {
        ServiceManager.Instance.Get<OnWandActivated>().Unsubscribe(HandleWandActivated);
    }

    private void HandleWandActivated(Vector2 position)
    {
        if (Vector2.Distance(position, transform.position) > range) return;

        Interact();
    }

    // Update is called once per frame
    void Update()
    {
        _renderer.sprite = decaySteps[decayIndex];
        if (decayIndex <= 0) return;

        restoreCounter += Time.deltaTime;
        if (restoreCounter <= timeBeforeRestore) return;
        
        restoreCounter = 0;
        decayIndex = Mathf.Max(0, decayIndex - 1);

    }

    public virtual void Interact()
    {
        decayCounter += Time.deltaTime;
        if (decayCounter <= timeBeforeDecay) return;
        decayCounter = 0;

        var lastDecay = decayIndex;
        decayIndex = Mathf.Min(decaySteps.Length - 1, decayIndex + 1);

        if (lastDecay == decayIndex)
        {
            if (!destroyWhenDecayed) return;

            if (replacement) Instantiate(replacement, transform.position, Quaternion.identity, transform.parent);
            Destroy(gameObject);
        }

        ServiceManager.Instance.Get<OnDecayIncreased>().Invoke();
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}