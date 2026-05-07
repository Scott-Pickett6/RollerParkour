using UnityEngine;

namespace Spawners
{
    public interface ISpawner
    {
        GameObject Spawn(Vector3 position, Quaternion rotation);
    }
}