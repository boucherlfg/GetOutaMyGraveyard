using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;
public static class Ext
{
    public static T RandomChoice<T>(this IEnumerable<T> list)
    {
        var index = Random.Range(0, list.Count());
        return list.ElementAt(index);
    }
}