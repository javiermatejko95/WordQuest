using System.Collections.Generic;

public static class ServiceRegistry
{
    public static readonly List<IInitializableService> Services = new()
    {
        new SaveService(),
    };
}