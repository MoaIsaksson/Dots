using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Random = UnityEngine.Random;

public partial struct SpawnerSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        foreach (var spawner in SystemAPI.Query<RefRW<Spawner>>())
        {
            spawner.ValueRW.EnemyCount = 0;
        }
    }

    public void OnDestroy(ref SystemState state) { }

    
    //Used a different Destroying version based on lifetime
    /*private void Destroy(EntityCommandBuffer ecb, ref SystemState state)
    {
        foreach ((EnemyTag _, Entity entity) in SystemAPI.Query<EnemyTag>().WithEntityAccess())
        {
            ecb.DestroyEntity(entity);
        }
    }*/

    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);
        foreach (var (spawner, enemyLifeTime) in SystemAPI.Query<RefRW<Spawner>, EnemyLifeTime>())
        {
            if (spawner.ValueRO.EnemyCount < 20)
            {
                if (!(spawner.ValueRO.NextSpawnTime < SystemAPI.Time.ElapsedTime)) continue;
                Entity newEntity = state.EntityManager.Instantiate(spawner.ValueRO.Prefab);
                float3 pos = new float3(spawner.ValueRO.SpawnPosition.x + Random.Range(-15, 15), spawner.ValueRO.SpawnPosition.y + Random.Range(6, 8), 0);
                state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(pos));
                spawner.ValueRW.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.SpawnRate;
                spawner.ValueRW.Timer = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.NextTimer;

                ecb.AddComponent<EnemyTag>(newEntity);
                ecb.AddComponent(newEntity, new LifeTime { value = enemyLifeTime.value });

                spawner.ValueRW.EnemyCount++;
            }
            
            else
            {
                if (spawner.ValueRO.Timer < SystemAPI.Time.ElapsedTime)
                {
                    spawner.ValueRW.EnemyCount = 0;
                }
            }
            
        }
        ecb.Playback(state.EntityManager);
    }
}