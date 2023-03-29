using UnityEngine;

namespace GameEntities
{
    public abstract class EnemyAi : MonoBehaviour
    {
        public virtual void OnUpdate(EnemyController character){ }
    }
}