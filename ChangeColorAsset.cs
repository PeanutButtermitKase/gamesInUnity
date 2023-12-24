using UnityEngine;

public class ChangeColorAsset : MonoBehaviour
{
    private Renderer rend;
    public Color Color;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            rend.material.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            rend.material.color = Color.green;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            rend.material.color = Color.blue;
        }
    }
}
