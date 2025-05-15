using UnityEngine;

public class EnemyCrlt : MonoBehaviour
{
    public Despawned despawned;
    public EnemyHP hp;
    private void Awake()
    {
        this.despawned = GetComponent<Despawned>();
        hp = GetComponent<EnemyHP>();
    }
}
