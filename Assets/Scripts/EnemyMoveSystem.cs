using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

public partial struct EnemyMoveSystem : ISystem
{
   [BurstCompile]
   public void OnUpdate(ref SystemState state)
   {
      float deltaTime = SystemAPI.Time.DeltaTime;

      foreach (var (transform, enemyMoveSpeed) in SystemAPI.Query<RefRW<LocalTransform>, EnemyMoveSpeed>())
      {
         transform.ValueRW.Position += -transform.ValueRO.Up() * enemyMoveSpeed.value * deltaTime ;
      }
   }
}
