using UnityEngine;

namespace Game
{
    public class GameScoreManager : MonoBehaviour
    {
        public static GameScoreManager Instance { get; private set; }
    
        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }
    
        public int CalculateScore(int distance, float timeInSeconds)
        {
            double distanceScore = System.Math.Pow(distance, 2.5);
            double speedBonus = distance / (timeInSeconds + 1f);

            double totalScore = (distanceScore + speedBonus) * 0.01;

            return Mathf.RoundToInt(Mathf.Max((int)totalScore, 0));
        }
    
        public void SaveBestScore(int score)
        {
            long bestScore = LoadBestScore();
            if (score > bestScore)
            {
                PlayerPrefs.SetInt("BestScore", score);
                PlayerPrefs.Save();
            }
        }
        public int LoadBestScore()
        {
            return PlayerPrefs.GetInt("BestScore", 0);
        }
    }
}
