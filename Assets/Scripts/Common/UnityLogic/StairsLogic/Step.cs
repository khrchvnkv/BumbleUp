using UnityEngine;

namespace Common.UnityLogic.StairsLogic
{
    public sealed class Step : MonoBehaviour
    {
        [field: SerializeField] public Transform BallPivot { get; private set; }
        [field: SerializeField] public Transform NextPivot { get; private set; }
    }
}