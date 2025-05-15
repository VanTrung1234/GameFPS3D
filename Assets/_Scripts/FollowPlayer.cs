using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    private bool canFollow = false;
    public float Speed = 2f;
    public float dislimit = 4f;
    private float randPosx = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        randPosx = Random.Range(-6, 6);
    }
    void Start()
    {
        Invoke("StartFollowing", 1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (canFollow)
        {
            this.Follow();
        }
    }

    void Follow()
    {
        Vector3 pos = player.position;
        pos.x = randPosx;
        Vector3 distance = pos - transform.position;
        
        if (distance.magnitude >= this.dislimit)
        {
            Vector3 targetPoint = pos - distance.normalized * this.dislimit;

            transform.position =
                Vector3.MoveTowards(transform.position, targetPoint, this.Speed * Time.fixedDeltaTime);
        }
    }
    void StartFollowing()
    {
        canFollow = true;
    }

}
