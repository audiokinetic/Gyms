/*******************************************************************************
The content of this file includes portions of the AUDIOKINETIC Wwise Technology
released in source code form as part of the SDK installer package.

Commercial License Usage

Licensees holding valid commercial licenses to the AUDIOKINETIC Wwise Technology
may use this file in accordance with the end user license agreement provided 
with the software or, alternatively, in accordance with the terms contained in a
written agreement between you and Audiokinetic Inc.

Apache License Usage

Alternatively, this file may be used under the Apache License, Version 2.0 (the 
"Apache License"); you may not use this file except in compliance with the 
Apache License. You may obtain a copy of the Apache License at 
http://www.apache.org/licenses/LICENSE-2.0.

Unless required by applicable law or agreed to in writing, software distributed
under the Apache License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES
OR CONDITIONS OF ANY KIND, either express or implied. See the Apache License for
the specific language governing permissions and limitations under the License.
*******************************************************************************/

using System;
using UnityEngine;
using UnityEngine.Video;

public class AdvancedVideoAudioSync_Interact : OnOffManager
{
    [SerializeField]
    VideoPlayer videoPlayer;
    public VideoPlayer VideoPlayer { get { return videoPlayer; } }

    [SerializeField]
    AkEvent _event;
    public AkEvent _Event { get { return _event; } }

    [SerializeField]
    TextMesh audioLatencyText;

    public double AudioLatencyMs { get; private set; }
    public int CurrentAudioPositionMs { get; private set; }

    const float DefaultVideoPlaybackSpeed = 1.0f;
    const float VideoPlaybackSpeedCorrectionFactor = 0.2f;
    const float TextUpdateInterval = 0.1f;
    public const int MaxLatencyMs = 50;

    float timeSinceLastTextUpdate = 0.0f;

    public override void OffAction()
    {
        _event.Stop(0);
        videoPlayer.Stop();
        videoPlayer.loopPointReached -= EndReached;
    }

    public override void OnAction()
    {
        _event.HandleEvent(gameObject);
        videoPlayer.Play();
        videoPlayer.loopPointReached += EndReached;
    }

    void EndReached(VideoPlayer source)
    {
        Interact();
    }

    void Update()
    {
        if (!IsOn) return;

        timeSinceLastTextUpdate += Time.deltaTime;

        if (AkUnitySoundEngine.GetSourcePlayPosition(_event.playingId, out int out_audioPositionMs) == AKRESULT.AK_Success)
        {
            CurrentAudioPositionMs = out_audioPositionMs;
            UpdateAudioLatency();

            if (timeSinceLastTextUpdate >= TextUpdateInterval)
            {
                UpdateAudioLatencyText();
                timeSinceLastTextUpdate = 0.0f;
            }

            AdjustVideoPlaybackSpeed();
        }
    }

    void UpdateAudioLatency()
    {
        double videoTimeMs = videoPlayer.time * 1000.0;
        AudioLatencyMs = videoTimeMs - CurrentAudioPositionMs;
    }

    void UpdateAudioLatencyText()
    {
        audioLatencyText.text = $"Audio Latency: {AudioLatencyMs:F2}ms";
    }

    void AdjustVideoPlaybackSpeed()
    {
        if (Math.Abs(AudioLatencyMs) > MaxLatencyMs)
        {
            videoPlayer.playbackSpeed = AudioLatencyMs > 0
                ? DefaultVideoPlaybackSpeed - VideoPlaybackSpeedCorrectionFactor
                : DefaultVideoPlaybackSpeed + VideoPlaybackSpeedCorrectionFactor;
        }
        else if (videoPlayer.isPlaying && videoPlayer.playbackSpeed != DefaultVideoPlaybackSpeed)
        {
            videoPlayer.playbackSpeed = DefaultVideoPlaybackSpeed;
        }
    }
}