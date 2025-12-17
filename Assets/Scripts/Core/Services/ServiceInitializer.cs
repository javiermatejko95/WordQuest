using UnityEngine;

public class ServiceInitializer : MonoBehaviour
{
    private void Awake()
    {
        foreach (var service in ServiceRegistry.Services)
        {
            service.Initialize();
        }
    }
}
