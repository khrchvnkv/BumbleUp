using UnityEngine;

namespace Common.StaticData
{
    [CreateAssetMenu(fileName = "BallStaticData", menuName = "Static Data/BallStaticData")]
    public sealed class BallStaticData : ScriptableObject
    {
        [field:SerializeField] public float SideJumpHeight { get; private set; }
        [field:SerializeField] public float HorizontalOffset { get; private set; }
        [field:SerializeField] public float JumpDuration { get; private set; }
        [field:SerializeField] public float FallingSpeed { get; private set; }
        
        
        [field:Space, Header("Animations Duration")]
        [field: SerializeField] public float DurationAfterCollision { get; private set; }
    }
}