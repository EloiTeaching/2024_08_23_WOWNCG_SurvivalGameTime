using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeCountMono : MonoBehaviour
{

    public float m_secondsPast;
    //0.265464

    public UnityEvent<string> m_onSecondsChangedAsString;
    public UnityEvent<float> m_onSecondsChangedAsFloat;


    void Update()
    {
        m_secondsPast += Time.deltaTime;
        int secondsAsTrimmed =(int) m_secondsPast;
        //1 5 8 1565 
        m_onSecondsChangedAsString.Invoke(secondsAsTrimmed.ToString());
        m_onSecondsChangedAsFloat.Invoke(m_secondsPast);
    }


    [ContextMenu("Reset time to zero")]
    public void ResetTimeToZero() { 
    
        m_secondsPast = 0;
    }

}
