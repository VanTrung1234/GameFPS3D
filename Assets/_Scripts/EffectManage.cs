using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class EffectManage : MonoBehaviour
{
    static public EffectManage Instance;
    public List<GameObject> effect;

    private void Awake()
    {
        EffectManage.Instance = this;
        this.LoadEffect();
        
    }

    protected virtual void LoadEffect()
    {
        this.effect = new List<GameObject>();
        foreach (Transform child in transform)
        {
            this.effect.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }
    }

    public virtual void SpawnVFX(string effectName, Vector3 position, Quaternion rot)
    {
        GameObject effect = this.Get(effectName);
        if (effect == null)
        {
            Debug.LogWarning($"Effect with name '{effectName}' not found!");
            return;
        }

        GameObject newEffect = Instantiate(effect, position, rot);
        newEffect.SetActive(true);
    }

    // Tìm hiệu ứng theo tên trong danh sách
    protected virtual GameObject Get(string effectName)
    {
        foreach (GameObject child in this.effect)
        {
            if (child.name == effectName)
                return child;
        }
        return null;
    }
}
