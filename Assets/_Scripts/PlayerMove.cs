using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    protected Rigidbody2D rb2;
    public Vector2 velocity = new Vector2(0f, 0f);
    public float pressHorizontal=0f;
    public float pressVertical=0f;
    public float SpeedUp = 0.5f;
    public float SpeedDown = 0.5f;
    public float SpeedMax = 20f;
    public float SpeedHorizontal = 3f;
    void Awake()
    {
        this.rb2 = GetComponent<Rigidbody2D>();
    }
   
    private void Update()
    {
        this.pressHorizontal = Input.GetAxis("Horizontal");
        this.pressVertical = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        Debug.Log("trungnnas");
        this.UpdateSpeed();
    }

    protected virtual void UpdateSpeed()
    {
        this.velocity.x = this.pressHorizontal*SpeedHorizontal;

        this.UpdateSpeedUp();
        this.UpdateSpeedDown();

        this.rb2.MovePosition(this.rb2.position + this.velocity * Time.fixedDeltaTime);
    }

    protected virtual void UpdateSpeedUp()
    {
        if (this.pressVertical > 0)
        {
            this.velocity.y += SpeedUp;
            if (this.velocity.y > SpeedMax) this.velocity.y = SpeedMax;

            if(transform.position.x>7 || transform.position.x < -7)
            {
                this.velocity.y -=1;
                if(this.velocity.y < 3f) this.velocity.y = 3f; 
            }
        }
    }
    
    protected virtual void UpdateSpeedDown()
    {
        if (this.pressVertical <= 0)
        {
            this.velocity.y -= SpeedDown;
            if (this.velocity.y < 0) this.velocity.y = 0;
        }
    }

}
