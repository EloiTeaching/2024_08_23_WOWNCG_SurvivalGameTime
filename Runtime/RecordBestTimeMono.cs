using UnityEngine;
using UnityEngine.Events;

public class RecordBestTimeMono : MonoBehaviour
{

    public float m_currentBestScore;

    public string m_currentTimeFormat = "{0:00.00}";
    public UnityEvent<string> m_onNewCurrentTimeInSecondsAsString;
    public UnityEvent<float> m_onNewCurrentTimeInSeconds;

  
    public void PushInNewCurrentTime(float currentTimeInSeconds) {

        if (currentTimeInSeconds > m_currentBestScore  ) { 
        
            m_currentBestScore = currentTimeInSeconds;
            m_onNewCurrentTimeInSeconds.Invoke(m_currentBestScore);
            m_onNewCurrentTimeInSecondsAsString.Invoke(
                string.Format(m_currentTimeFormat, m_currentBestScore)
                );
        }
    }
}
