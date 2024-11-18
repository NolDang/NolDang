using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour {
    RectTransform rect;
    Item[] items;

    private float previousTimeScale; // 레벨업 전에 저장된 배속 값

    void Awake() {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void Show() {
        // 현재 배속 값을 저장
        previousTimeScale = Time.timeScale;

        // 배속을 0으로 설정 (일시정지)
        Time.timeScale = 0f;

        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
        AudioManager.instance.EffectBgm(true);
    }

    public void Hide() {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        AudioManager.instance.EffectBgm(false);

        // 배속을 복원
        RestoreTimeScale();
    }

    public void Select(int index) {
        items[index].OnClick();
    }

    void Next() {
        // 1. 모든 아이템 비활성화
        foreach (Item item in items) {
            item.gameObject.SetActive(false);
        }

        // 2. 그 중에서 랜덤 3개 아이템 활성화
        int[] ran = new int[3];
        while (true) {
            ran[0] = Random.Range(0, items.Length);
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);

            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
                break;
        }

        for (int index = 0; index < ran.Length; index++) {
            Item ranItem = items[ran[index]];

            // 3. 만렙 아이템의 경우, 소비 아이템으로 대체
            if (ranItem.level == ranItem.data.damages.Length) {
                items[4].gameObject.SetActive(true);
            }
            else {
                ranItem.gameObject.SetActive(true);
            }
        }
    }

    // 배속 복원 함수
    private void RestoreTimeScale() {
        if (DoubleSpeed.instance != null) {
            // DoubleSpeed 싱글톤을 통해 현재 배속 복원
            DoubleSpeed.instance.ApplySpeed();
        } else {
            // 싱글톤이 없을 경우 이전 배속 복원
            Time.timeScale = previousTimeScale;
        }
    }
}
