using Unity.Burst;
using Unity.Entities;

[BurstCompile]
public partial struct LifeTimeManagementSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<LifeTime>();
    }
    
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.TempJob);
        float deltaTime = SystemAPI.Time.DeltaTime;
        
        new LifeJob
        {
            ecb = ecb,
            DeltaTime = deltaTime
        }.Schedule();
        
        state.Dependency.Complete();
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}

[BurstCompile]
public partial struct LifeJob : IJobEntity
{
    public EntityCommandBuffer ecb;
    public float DeltaTime;
    
    [BurstCompile]
    public void Execute( Entity entity,ref LifeTime lifeTime)
    {
        lifeTime.value -= DeltaTime;
        if (lifeTime.value <= 0)
        {
            ecb.AddComponent<IsDestroying>(entity);
        }
    }
}
