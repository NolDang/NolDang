using UnityEngine;
using UnityEngine.UI;

public class DoubleSpeed : MonoBehaviour
{
    public static DoubleSpeed instance; // 싱글톤 인스턴스

    public Text speedText; // 배속을 표시할 텍스트
    private float[] speeds = { 1f,1.5f, 2f, 3f }; // 배속 단계
    private int currentSpeedIndex = 0; // 현재 배속 단계의 인덱스

    void Awake()
    {
        // 싱글톤 초기화
        if (instance == null)
        {
            instance = this; // 현재 객체를 싱글톤 인스턴스로 설정
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 삭제되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 중복 생성 방지
        }
    }

    void Start()
    {
        UpdateSpeedText();
        ApplySpeed();
    }

    public void OnSpeedButtonClick()
    {
        // 다음 배속 단계로 변경
        currentSpeedIndex = (currentSpeedIndex + 1) % speeds.Length;

        // 배속 적용
        ApplySpeed();

        // 텍스트 업데이트
        UpdateSpeedText();
    }

    public void ApplySpeed()
    {
        // 게임의 배속 설정
        Time.timeScale = speeds[currentSpeedIndex];
    }

    private void UpdateSpeedText()
    {
        // 텍스트에 현재 배속 반영
        if (speedText != null)
        {
            speedText.text = $" {speeds[currentSpeedIndex]}x";
        }
    }

    public void SetSpeed(int index)
    {
        // 외부에서 특정 배속으로 설정할 때 사용
        currentSpeedIndex = Mathf.Clamp(index, 0, speeds.Length - 1);
        ApplySpeed();
        UpdateSpeedText();
    }
}


