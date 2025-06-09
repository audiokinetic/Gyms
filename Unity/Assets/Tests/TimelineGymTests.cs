using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

namespace Tests
{
    public class TimelineGymTests: GymTests
    {
#if AK_WWISE_ADDRESSABLES && UNITY_ADDRESSABLES
        public IEnumerator LoadAllTimeLineEvent(PlayableDirector timeline)
        {
            using var enumerator = timeline.playableAsset.outputs.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var value = enumerator.Current;
                var srcObj = value.sourceObject as AkTimelineEventTrack;
                if (srcObj == null) continue;
                var eventRefs = srcObj.GetEventReferences();
                foreach (var eventRef in eventRefs)
                {
#if UNITY_WEBGL
                    yield return eventRef.CompleteLoadBank();
#else
                    yield return new WaitUntil(() =>eventRef.CompleteLoadBank().IsCompleted);
#endif
                }
            }
        }
       
#endif
    }
}