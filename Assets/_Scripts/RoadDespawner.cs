using UnityEngine;

public class RoadDespawner : MonoBehaviour
{
    protected float distance = 0;

    private void FixedUpdate()
    {
        this.UpdateRoad();
    }

    protected virtual void UpdateRoad()
    {
        this.distance = Vector2.Distance(PlayerCtrl.Instance.transform.position, transform.position);
        if (distance >40) this.DesSpawn();
    }

    protected virtual void DesSpawn()
    {
        Destroy(gameObject);
    }
}
