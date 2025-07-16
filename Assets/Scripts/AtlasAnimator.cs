using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class AtlasAnimator : MonoBehaviour
{
    public int cols = 8, rows = 8;
    public float fps = 12f;

    private Renderer rend;
    private int totalFrames;
    private int currentFrame;
    private float timer;

    void Start()
    {
        rend = GetComponent<Renderer>();
        totalFrames = cols * rows;
        // make sure texture starts at frame 0
        rend.material.SetTextureScale("_MainTex", new Vector2(1f / cols, 1f / rows));
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer < 1f / fps) return;
        timer -= 1f / fps;

        currentFrame = (currentFrame + 1) % totalFrames;

        float sizeX = 1f / cols;
        float sizeY = 1f / rows;
        float offsetX = (currentFrame % cols) * sizeX;
        // Unity's V‑coordinate is bottom‑left, so we invert Y
        float offsetY = 1f - sizeY - ((currentFrame / cols) * sizeY);

        rend.material.SetTextureOffset("_MainTex", new Vector2(offsetX, offsetY));
    }
}
