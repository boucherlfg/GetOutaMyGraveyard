using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    [SerializeField]
    private float floatingAmplitude = 0.25f;
    [SerializeField]
    private float floatingSpeed = 0.25f;

    private Vector2 lastPosition;
    private Vector2 lastVelocity;
    SpriteRenderer _spriteRenderer;

    private void Start()
    {
        lastPosition = transform.position;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        var velocity = (position - lastPosition) / Time.deltaTime;
        lastPosition = position;
        if (Mathf.Abs(velocity.x) >= 0.1f) lastVelocity = velocity;

        _spriteRenderer.flipX = lastVelocity.x > 0.1f;

        transform.localPosition = floatingAmplitude * Mathf.Cos(Time.time * 2 * Mathf.PI * floatingSpeed) * Vector2.up;
    }
}
