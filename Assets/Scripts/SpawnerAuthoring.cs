using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class SpawnerAuthoring : MonoBehaviour
{
    public GameObject Prefab;
    public float SpawnRate;

    class SpawnerBaker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent(entity, new Spawner
            {
                Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                SpawnPosition = float2.zero,
                NextSpawnTime = 2,
                SpawnRate = authoring.SpawnRate
            });
        }
    }
}

public struct EnemyTag : IComponentData { }

public struct EnemyMoveSpeed : IComponentData
{
    public float value;
}

