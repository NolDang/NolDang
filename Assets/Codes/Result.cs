using UnityEngine;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour {
    public GameObject[] titles;
    private bool isVictory; // 승리 여부를 저장

    // 패배 시 호출
    public void Lose() {
        titles[0].SetActive(true);
        isVictory = false; // 패배로 설정
    }

    // 승리 시 호출
    public void Win() {
        titles[1].SetActive(true);
        isVictory = true; // 승리로 설정
    }
    public void Story()
    {
        titles[2].SetActive(true);
        isVictory = true; // 승리로 설정
    }

    // 버튼 클릭 시 호출되는 메서드
    public void OnRetryButtonClick() {
        if (isVictory) {
            GoToNextScene(); // 승리 시 다음 씬으로 이동
        } else {
            RetryCurrentScene(); // 패배 시 현재 씬 재시작
        }
    }

    private void GoToNextScene() {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(nextSceneIndex);
        } else {
            Debug.LogWarning("다음 씬이 없습니다!");
        }
    }

    private void RetryCurrentScene() {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
    
}
