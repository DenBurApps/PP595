using UnityEngine;

public static class PrefsScoreSaver
{
    public static void SaveScore(string key, int _score)
    {
        if (PlayerPrefs.HasKey(key))
        {
            int currentScore = PlayerPrefs.GetInt(key);

            if (currentScore < _score)
            {
                PlayerPrefs.SetInt(key, _score);
            }
        }
        else
        {
            PlayerPrefs.SetInt(key, _score);
        }
    }
}
