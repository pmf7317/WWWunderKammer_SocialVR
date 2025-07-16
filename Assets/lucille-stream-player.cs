using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class lucilleScript : MonoBehaviour
{
    public string videoURL = "https://carlagannis.com/wwwunderkammer-mainchamber-vids/Lucille.mp4";
    public RenderTexture renderTexture;
    public Renderer targetRenderer; 

    private VideoPlayer videoPlayer;

    void Start()
    {
        Debug.Log("VideoStreamPlayer starting...");

        // Create and configure the VideoPlayer component
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = videoURL;

        videoPlayer.renderMode = VideoRenderMode.RenderTexture;
        videoPlayer.targetTexture = renderTexture;

        videoPlayer.isLooping = true;
        videoPlayer.playOnAwake = false;

        videoPlayer.sendFrameReadyEvents = true; // Helps keep render texture active

        // Event hooks
        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.errorReceived += OnVideoError;
        videoPlayer.loopPointReached += OnVideoFinished;

        Debug.Log("Preparing video: " + videoURL);
        videoPlayer.Prepare();
    }

    void OnVideoPrepared(VideoPlayer vp)
    {
        Debug.Log("Video prepared! Starting playback.");

        // Reapply RenderTexture to force it onto the material
        if (targetRenderer != null && renderTexture != null)
        {
            targetRenderer.material.mainTexture = renderTexture;
        }

        vp.Play();
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("Video finished. Replaying.");
        vp.Play(); // Force restart in case looping fails
    }

    void OnVideoError(VideoPlayer vp, string message)
    {
        Debug.LogError("❌ Video Player Error: " + message);
    }
}
