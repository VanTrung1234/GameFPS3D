using UnityEngine;

public class damageBom : MonoBehaviour
{
    public string bomTag = "Bom";
    protected EnemyCrlt enemyCrlt;
    private void Awake()
    {
        this.enemyCrlt = GetComponent<EnemyCrlt>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Kiểm tra nếu collider không phải là null
        if (collider == null)
        {
            Debug.LogError("Collider is null.");
            return;  // Dừng lại nếu collider là null
        }

        // Kiểm tra xem collider có gắn tag đúng không
        if (collider.CompareTag(bomTag))
        {
            // Nếu đối tượng va chạm có tag "Bom", thực hiện hành động tương ứng
            Debug.Log("Bom detected!");
            Destroy(gameObject);  // Hủy đối tượng hiện tại
        }
        else
        {
            // Nếu không phải bom, hủy cả collider và đối tượng hiện tại
            Destroy(collider.gameObject);  // Hủy đối tượng va chạm
            Destroy(gameObject);  // Hủy đối tượng hiện tại
        }
    }


}
