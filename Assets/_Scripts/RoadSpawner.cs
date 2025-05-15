using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public GameObject roadPrefab;
    protected GameObject roadCurrent;
    protected GameObject roadSpawnPos;
    protected float distance = 0;

    private void Awake()
    {

        this.roadSpawnPos = GameObject.Find("RoadSpawnPos");
        this.roadCurrent = this.roadPrefab;
    }
    private void FixedUpdate()
    {
        this.UpdateRoad();
    }

    protected virtual void UpdateRoad()
    {
        this.distance = Vector2.Distance(PlayerCtrl.Instance.transform.position, this.roadCurrent.transform.position);
        if (distance > 20) this.Spawn();
    }

    protected virtual  void Spawn()
    {
        Vector3 pos = this.roadSpawnPos.transform.position;
        pos.x = 0;
        this.roadCurrent = Instantiate(this.roadPrefab,pos , this.roadPrefab.transform.rotation);
        roadCurrent.transform.parent = transform;
    }
}
