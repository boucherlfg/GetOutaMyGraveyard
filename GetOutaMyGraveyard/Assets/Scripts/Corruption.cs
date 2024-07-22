using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corruption : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ServiceManager.Instance.Get<OnDirtCorrupted>().Invoke();
    }
}
