using Unity.Entities;
using Unity.Transforms;

[UpdateInGroup(typeof(SimulationSystemGroup))]
[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct FireProjectileSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);
        
        foreach (var (projectilePrefab, transform, projectileScale, lifeTime) in SystemAPI.Query<ProjectilePrefab, LocalTransform, ProjectileScale, ProjectileLifeTime>().WithAll<FireProjectileTag>())
        {
            var newProjectile = ecb.Instantiate(projectilePrefab.value);
            var projectileTransform = LocalTransform.FromPositionRotationScale(transform.Position, transform.Rotation, projectileScale.value );
           
            ecb.SetComponent(newProjectile, projectileTransform);
            ecb.AddComponent(newProjectile, new LifeTime{value = lifeTime.value});
        }
        
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}    
