using UnityEngine;

public class GobalReferences : MonoBehaviour
{
    public static GobalReferences instance;
    public GameObject bulletEffectPrefab;
    public GameObject nadeExposionEffect;
    public GameObject smokeExposionEffect;
    public GameObject bloodSprayEffect;
    public int waveNumber;
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
}
