using UnityEngine;

public class Despawned : MonoBehaviour
{
        public virtual void Despawn()
    {
        Destroy(gameObject);
    } 
}
