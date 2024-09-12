using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class SpawnerAuthoring : MonoBehaviour
{
    public GameObject Prefab;
    public float SpawnRate;
    public float lifeTime;
    public float timer;

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
                SpawnRate = authoring.SpawnRate,
                NextTimer = authoring.timer
            });
            
            AddComponent(entity, new EnemyLifeTime{value = authoring.lifeTime});
        }
    }
}

public struct EnemyTag : IComponentData { }

public struct EnemyMoveSpeed : IComponentData
{
    public float value;
}

public struct EnemyLifeTime : IComponentData
{
    public float value;
}

