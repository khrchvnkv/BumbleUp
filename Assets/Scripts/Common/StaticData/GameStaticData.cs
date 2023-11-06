using NaughtyAttributes;
using UnityEngine;

namespace Common.StaticData
{
    [CreateAssetMenu(fileName = "GameStaticData", menuName = "Static Data/GameStaticData")]
    public class GameStaticData : ScriptableObject
    {
        [field: SerializeField, Expandable] public BallStaticData BallStaticData { get; private set; }
        [field: SerializeField, Expandable] public ObstacleStaticData ObstacleStaticData { get; private set; }
        [field: SerializeField, Expandable] public WindowStaticData WindowStaticData { get; private set; }
    }
}