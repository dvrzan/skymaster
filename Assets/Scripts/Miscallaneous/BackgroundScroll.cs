using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float speed;

    private Vector2 initialOffset;
    private Renderer rendererComponent;

    void Start() {
        rendererComponent = GetComponent<Renderer>();
        initialOffset = rendererComponent.sharedMaterial.GetTextureOffset("_MainTex");
    }

    void Update()
    {
        float y = Mathf.Repeat(Time.time * speed, 1);
        Vector2 offset = new Vector2(initialOffset.x, y);

        rendererComponent.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }

    void OnDisable()
    {
        rendererComponent.sharedMaterial.SetTextureOffset("_MainTex", initialOffset);
    }
}
