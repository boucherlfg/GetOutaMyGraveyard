using System;
using System.Collections.Generic;
using System.Linq;

public class ServiceManager : Singleton<ServiceManager>
{
    private List<object> _services = new();
    public T Get<T>(Func<T> generator = null) where T : class
    {
        generator ??= DefaultCtor<T>;
        var srv = _services.Find(x => x is T);
        if (srv == null)
        {
            srv = generator();
            _services.Add(srv);
        }
        return srv as T;
    }

    private static T DefaultCtor<T>() where T : class => typeof(T).GetConstructor(new Type[] { }).Invoke(new object[] { }) as T;
}