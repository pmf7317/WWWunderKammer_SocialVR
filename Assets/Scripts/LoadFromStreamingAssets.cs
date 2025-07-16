using UnityEngine;
using UnityEngine.Video;
using System.IO;

[RequireComponent(typeof(VideoPlayer))]
public class LoadFromStreamingAssets : MonoBehaviour
{
    void Start()
    {
        var vp = GetComponent<VideoPlayer>();
        vp.source = VideoSource.Url;

        // Build the platform‑correct URL to the StreamingAssets file:
        string filename = "myVideo.ogv";
        string path = Path.Combine(Application.streamingAssetsPath, filename);

        // On most platforms you need the file:// protocol; WebGL serves streamingAssets directly.
#if UNITY_WEBGL && !UNITY_EDITOR
        vp.url = path;
#else
        vp.url = "file:///" + path;
#endif

        vp.Play();
    }
}
