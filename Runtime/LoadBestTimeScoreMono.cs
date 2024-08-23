using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class LoadBestTimeScoreMono : MonoBehaviour {

    public string m_guidIdForBestScore;
    public float m_currentBestScoreToSave;

    public UnityEvent<float> m_onBestScoreImported;

    private void Reset()
    {
        ResetNewGuid();
    }

    [ContextMenu("Reset New GUID")]
    public void ResetNewGuid() {

        m_guidIdForBestScore = Guid.NewGuid().ToString();
    }

    public string GetSaveFilePath() {

        return System.IO.Path.Combine(Application.persistentDataPath, m_guidIdForBestScore);
    }

    private void Awake()
    {
        ShowWhereScoreIsStore();
    }

    [ContextMenu("Show in debug log storage path")]
    public void ShowWhereScoreIsStore()
    {

        Debug.Log("Best score is store: " + GetSaveFilePath());
    }
    [ContextMenu("Show in explore storage path")]
    public void ShowInExploreWhereScoreIsStore()
    {
        Application.OpenURL(GetSaveFilePath());
    }


    public void SetScoreToSaveWhenClose(float bestScoreToSave) {
        m_currentBestScoreToSave = bestScoreToSave;
    }
    public void OnEnable()
    {
        LoadScoreFromFile();

    }

    public void OnDisable()
    {
        SaveScoreToFile();
    }
    public void OnDestroy()
    {
        SaveScoreToFile();
    }
    public void OnApplicationQuit()
    {
        SaveScoreToFile();
    }
    public void OnApplicationFocus(bool focus)
    {
        if (focus == false) {
            SaveScoreToFile();
        }
    }


    [ContextMenu("Load score")]
    public void LoadScoreFromFile()
    {
        string path = GetSaveFilePath();
        if (!File.Exists(path))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, "0");
        }
        string value = File.ReadAllText(path);

        if (float.TryParse(value, out float score))
        {
            m_currentBestScoreToSave = score;
            m_onBestScoreImported.Invoke(score);
        }
        else
        {
            m_currentBestScoreToSave = score;
            m_onBestScoreImported.Invoke(0);
        }
    }


    [ContextMenu("Save current score in inspector")]
    public void SaveScoreToFile()
    {
        SaveScoreToFile(m_currentBestScoreToSave);
    }

        public void SaveScoreToFile(float bestScoreToSave)
    {
        string path = GetSaveFilePath();
        if (!File.Exists(path))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, "0");
        }
        File.WriteAllText(path, bestScoreToSave.ToString());
    }
}