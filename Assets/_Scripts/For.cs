using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int maxMinion = 7;
        for(int i = 0; i < maxMinion; i++)
        {
            this.Spawn(i);
        }
    }
    void Spawn(int i)
    {
        Debug.Log("okkk");
    }

}
