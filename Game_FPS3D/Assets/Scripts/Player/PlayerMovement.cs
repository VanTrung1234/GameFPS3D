using Unity.Mathematics;  // Import thư viện toán học Unity, để sử dụng các hàm như sqrt (căn bậc 2).
using UnityEngine;        // Import UnityEngine để làm việc với các chức năng Unity, như việc điều khiển nhân vật.
using UnityEngine.InputSystem;  // Import hệ thống nhập liệu mới của Unity để xử lý các sự kiện đầu vào (input) như phím bấm.

public class PlayerMovement : MonoBehaviour  // Định nghĩa lớp PlayerMovement kế thừa từ MonoBehaviour.
{
    private CharacterController controller;  // Khai báo đối tượng CharacterController để điều khiển di chuyển của nhân vật.

    public float Speed = 12f;  // Tốc độ di chuyển của nhân vật, có thể chỉnh sửa trong Unity Inspector.
    public float gravity = -9.81f * 2;  // Trọng lực, giá trị âm để kéo nhân vật xuống.
    public float jumpHeight = 3f;  // Chiều cao nhảy của nhân vật.

    public Transform groundCheck;  // Đối tượng để kiểm tra xem nhân vật có đứng trên mặt đất không.
    public float groundDistance = 1f;  // Khoảng cách để kiểm tra từ groundCheck xuống mặt đất.
    public LayerMask groundMask;  // Lớp mặt đất (LayerMask), giúp kiểm tra xem nhân vật có đứng trên mặt đất không.

    Vector3 velocity;  // Biến lưu trữ tốc độ di chuyển theo các trục (x, y, z).

    bool isGround;  // Biến kiểm tra xem nhân vật có đang đứng trên mặt đất không.
    bool isMoving;  // Biến kiểm tra xem nhân vật có đang di chuyển hay không.

    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);  // Vị trí trước đó của nhân vật, để kiểm tra nếu nhân vật có di chuyển hay không.

    // Start is called once before the first execution of Update after the MonoBehaviour is created.
    void Start()
    {
        controller = GetComponent<CharacterController>();  // Lấy Component CharacterController gắn với đối tượng này.
    }

    // Update is called once per frame.
    void Update()
    {
        // Kiểm tra xem nhân vật có đang đứng trên mặt đất không.
        isGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Nếu nhân vật đang đứng trên mặt đất và đang rơi (velocity.y < 0), đặt velocity.y thành -2 để nhân vật không rơi quá nhanh.
        if (isGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Lấy đầu vào từ phím điều khiển (Horizontal và Vertical).
        float x = Input.GetAxis("Horizontal");  // Điều khiển chuyển động theo trục X (trái phải).
        float z = Input.GetAxis("Vertical");    // Điều khiển chuyển động theo trục Z (lên xuống).

        // Tạo vector di chuyển từ đầu vào của người chơi.
        Vector3 move = transform.right * x + transform.forward * z;

        // Di chuyển nhân vật theo vector `move` với tốc độ `Speed` và thời gian giữa các khung hình (deltaTime).
        controller.Move(move * Speed * Time.deltaTime);

        // Nếu người chơi nhấn phím nhảy (Jump) và đang đứng trên mặt đất, tính toán lực nhảy.
        if (Input.GetButtonDown("Jump") && isGround)
        {
            
            velocity.y = math.sqrt(jumpHeight * -2f * gravity);  // Tính lực nhảy, công thức này đảm bảo độ cao nhảy hợp lý.
        }

        // Thêm trọng lực vào velocity.y (trọng lực giảm tốc độ bay lên và kéo nhân vật xuống khi không còn nhảy).
        velocity.y += gravity * Time.deltaTime;

        // Di chuyển nhân vật với trọng lực.
        controller.Move(velocity * Time.deltaTime);

        // Kiểm tra xem nhân vật có di chuyển hay không, so sánh vị trí hiện tại với vị trí trước đó.
        if (lastPosition != gameObject.transform.position && isGround == true)
        {
            isMoving = true;  // Nếu vị trí thay đổi và nhân vật đang đứng trên mặt đất, gán là đang di chuyển.
        }
        else
        {
            isMoving = false;  // Nếu không di chuyển, gán là không di chuyển.
        }

        // Lưu lại vị trí hiện tại của nhân vật để so sánh trong lần cập nhật tiếp theo.
        lastPosition = gameObject.transform.position;
    }
}
