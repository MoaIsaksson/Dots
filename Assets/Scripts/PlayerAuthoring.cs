using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour
{
  public float moveSpeed;
  public GameObject projectilePrefab;
  public float projectileScale;
  public float ProjectileLifeTime;

  private class PlayerAuthoringBaker : Baker<PlayerAuthoring>
  {
    public override void Bake(PlayerAuthoring authoring)
    {
      Entity playerEntity = GetEntity(TransformUsageFlags.Dynamic);
      
      AddComponent<PlayerTag>(playerEntity);
      AddComponent<PlayerMoveInput>(playerEntity);
      
      AddComponent(playerEntity, new PlayerMoveSpeed
      {
        value = authoring.moveSpeed,
      });
      
      AddComponent<FireProjectileTag>(playerEntity);
      SetComponentEnabled<FireProjectileTag>(playerEntity, false);
      
      AddComponent(playerEntity, new ProjectilePrefab
      {
        value = GetEntity(authoring.projectilePrefab, TransformUsageFlags.Dynamic)
      });
      
      AddComponent(playerEntity, new ProjectileScale{value = authoring.projectileScale});
      AddComponent(playerEntity, new ProjectileLifeTime{value = authoring.ProjectileLifeTime });
    }
  }
}

public struct PlayerMoveInput : IComponentData
{
  public float2 value;
}

public struct PlayerMoveSpeed : IComponentData
{
  public float value;
}

public struct PlayerTag : IComponentData{ }

public struct ProjectilePrefab : IComponentData
{
  public Entity value;
}

public struct ProjectileMoveSpeed : IComponentData
{
  public float value;
}

public struct ProjectileScale : IComponentData
{
  public float value;
}

public struct FireProjectileTag : IComponentData, IEnableableComponent { }

public struct ProjectileLifeTime : IComponentData
{
  public float value;
}

public struct LifeTime : IComponentData
{
  public float value;
}

public struct IsDestroying : IComponentData { }