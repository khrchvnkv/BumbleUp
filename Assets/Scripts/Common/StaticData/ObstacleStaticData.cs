using UnityEngine;

namespace Common.StaticData
{
    [CreateAssetMenu(fileName = "ObstacleStaticData", menuName = "Static Data/ObstacleStaticData")]
    public sealed class ObstacleStaticData : ScriptableObject
    {
        [field: SerializeField, Min(0.1f)] public float TimeBtwSpawn { get; private set; }
        [field: SerializeField, Min(0.1f)] public float JumpDuration { get; private set; }
    }
}