using UnityEngine;  // Sử dụng thư viện UnityEngine để truy cập các tính năng cơ bản của Unity

public class MouseMovement : MonoBehaviour  // Khai báo lớp MouseMovement kế thừa MonoBehaviour, lớp này sẽ điều khiển chuyển động của đối tượng
{
    public float mouseSensitivity = 500f;  // Độ nhạy chuột, giúp điều chỉnh mức độ di chuyển của đối tượng khi chuột di chuyển

    float xRotation = 0f;  // Biến lưu trữ góc xoay của đối tượng theo trục X (lên xuống)
    float yRotation = 0f;  // Biến lưu trữ góc xoay của đối tượng theo trục Y (trái phải)

    public float topClamp = -90f;
    public float bottomClamp = 90f;
    void Start()  // Phương thức Start() được gọi khi đối tượng được khởi tạo
    {
        Cursor.lockState = CursorLockMode.Locked;  // Khóa con trỏ chuột vào trung tâm màn hình để không bị di chuyển ngoài khu vực này
    }

    void Update()  // Phương thức Update() được gọi mỗi khung hình, điều khiển sự thay đổi trong mỗi lần di chuyển chuột
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;  // Lấy giá trị di chuyển chuột theo trục X và điều chỉnh với độ nhạy và Time.deltaTime
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;  // Lấy giá trị di chuyển chuột theo trục Y và điều chỉnh với độ nhạy và Time.deltaTime

        xRotation -= mouseY;  // Cập nhật góc xoay theo trục X (lên/xuống) bằng cách trừ đi giá trị di chuyển chuột theo Y

        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);  // Giới hạn góc xoay theo trục X trong phạm vi từ -90 đến 90 độ để tránh xoay quá mức

        yRotation += mouseX;  // Cập nhật góc xoay theo trục Y (trái/phải) bằng cách cộng thêm giá trị di chuyển chuột theo X

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);  // Áp dụng các giá trị góc xoay cho đối tượng, giữ trục Z cố định là 0
    }
}
