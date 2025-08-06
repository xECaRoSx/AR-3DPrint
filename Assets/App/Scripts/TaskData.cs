using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "NewTaskData", menuName = "ARLaser/Task Data")]
public class TaskData : ScriptableObject
{
    public string taskName;
    public List<StepData> steps;

    [System.Serializable]
    public class StepData
    {
        public string stepName;
        [TextArea(4, 5)]
        public string description;
        public List<AudioClip> voiceovers;

        public List<GameObject> guideObject;
        public Sprite image;
        public VideoClip video;
        [TextArea(2, 3)]
        public string warning;

        public float duration = 10f;

#if UNITY_EDITOR
        public void AutoSetDuration(float buffer = 3f)
        {
            if (voiceovers != null && voiceovers.Count > 0)
            {
                float maxLength = 0f;
                foreach (var clip in voiceovers)
                {
                    if (clip != null && clip.length > maxLength)
                        maxLength = clip.length;
                }
                duration = maxLength + buffer;
            }
        }
#endif
    }

#if UNITY_EDITOR
    public void AutoSetDurations(float buffer = 2.5f)
    {
        if (steps == null) return;

        foreach (var step in steps)
        {
            step.AutoSetDuration(buffer);
        }
    }

    private void OnValidate()
    {
        AutoSetDurations(3f);
    }
#endif
}