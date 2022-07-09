using System;
using AI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Logic
{
    public static class Utility
    {
        public static Vector3 SetRandomPosition(Vector3 min, Vector3 max)
        {
            return new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), 0.0f);
        }
    }
    
    public class Game : MonoBehaviour
    {
        public static Action OnLose;
        public static Action PlusScore;
        public static Action SpawnOther;
        
        [Header("Object")]
        public GameObject sword;
        [SerializeField] private Vector2 startPosition;
        
        [Header("UI")]
        public Text score;
        private int currentScore;
        public GameObject restartPanel;
        public Button restartButton;

        [Header("Enemy")]
        public GameObject player;
        public GameObject enemy;
        [SerializeField] private Vector3 minPosition, maxPosition;

        private Vector3 GetRandomVector => Utility.SetRandomPosition(minPosition, maxPosition);
        private void Awake()
        {
            Time.timeScale = 1;
            
            OnLose += Lose;
            PlusScore += IncreaseScore;
            SpawnOther += InstantiateEnemy;
        }
        
        private void OnDestroy()
        {
            OnLose -= Lose;
            PlusScore -= IncreaseScore;
            SpawnOther -= InstantiateEnemy;
        }

        private void Start()
        {
            InstantiateSword();
            currentScore = 0;
            score.text = currentScore.ToString();

            InstantiateEnemy();
            InstantiateEnemy();
            InstantiateEnemy();
        }

        private void InstantiateSword()
        {
            var swr = Instantiate(sword);
            swr.transform.position = startPosition;
        }

        private void IncreaseScore()
        {
            currentScore++;
            score.text = currentScore.ToString();
        }

        private void Lose()
        {
            restartPanel.SetActive(true);
            Time.timeScale = 0;
            restartButton.onClick.AddListener(() => SceneManager.LoadScene(0) );
        }
        
        // Instantiate enemy
        public void InstantiateEnemy()
        {
            var ghost = Instantiate(enemy);
            ghost.transform.position = GetRandomVector;
            ghost.GetComponent<Ghost>().player = player;
        }
    }
}
