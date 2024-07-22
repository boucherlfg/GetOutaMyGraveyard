using System;
using UnityEngine;

public abstract class BaseEvent 
{
    private Action Changed;
    public void Invoke()
    {
        Changed?.Invoke();
    }
    public void Subscribe(Action action) { Changed += action; }
    public void Unsubscribe(Action action) { Changed -= action; }
}

public abstract class BaseEvent<T>
{
    private Action<T> Changed;
    public void Invoke(T value)
    {
        Changed?.Invoke(value);
    }
    public void Subscribe(Action<T> action) { Changed += action; }
    public void Unsubscribe(Action<T> action) { Changed -= action; }
}

public class OnDecayIncreased : BaseEvent { }
public class OnWandActivated : BaseEvent<Vector2>{ }
public class OnDirtCorrupted : BaseEvent { }