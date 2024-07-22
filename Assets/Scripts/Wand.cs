using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour
{
    private Vector2 lastPosition;
    private Vector2 lastVelocity;
    private ParticleSystem particle;
    private float emission = 25;

    [SerializeField]
    private float speed = 3;
    private Camera _mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
        lastPosition = transform.position;
        lastVelocity = Vector2.right;
        particle = GetComponentInChildren<ParticleSystem>();
        this.emission = particle.emission.rateOverTime.constant;
    }

    // Update is called once per frame
    void Update()
    {
        var mouse = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Vector2.Lerp(transform.position, mouse, speed * Time.deltaTime);
        Vector2 position = transform.position;
        var velocity = (position - lastPosition) / Time.deltaTime;
        lastPosition = position;
        if (velocity.x * velocity.x > 0.1f) lastVelocity = velocity;

        transform.localScale = new Vector3(lastVelocity.x < -0.1f ? -0.5f : 0.5f, 0.5f, 1);

        if (Input.GetMouseButton(0))
        {
            transform.rotation = Quaternion.Euler(0, 0, -45 * Mathf.Sign(lastVelocity.x));
            var emission = particle.emission;
            emission.rateOverTime = this.emission;
            var wandActivated = ServiceManager.Instance.Get<OnWandActivated>();
            wandActivated?.Invoke((Vector2)mouse);
        }
        else
        {
            transform.rotation = Quaternion.identity;
            var emission = particle.emission;
            emission.rateOverTime = 0;
        }
    }
}
