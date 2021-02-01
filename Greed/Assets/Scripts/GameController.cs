using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    static GameController instance;

    [SerializeField] WeaponDurability[] items;
    [SerializeField] PlayerController player;
    [SerializeField] CountDownTimer timer;
    [SerializeField] BoxCollider theWin;
    [SerializeField] GameObject disabledEnemies;

    private void Awake() {
        if (!instance) {
            instance = this;
        } else Destroy(gameObject);

        instance.theWin.enabled = false;
    }

    // Update is called once per frame
    void Update() {

    }

    public static void ItemDamaged(BaseWeapon weapon, int damage) {
        for (int i = 0; i < instance.items.Length; i++) {
            if (instance.items[i].storedWeapon == weapon) {
                instance.items[i].TakeDamage(damage);
            }
        }
    }

    public static void ItemDestroyed() {
        instance.player.WeaponBreak();
    }

    public static void InsertItem(BaseWeapon weapon, int index) {
        instance.items[index].InsertItem(weapon);
    }

    public static void TakeDamage(int damage) {
        instance.player.TakeDamage(damage);
    }

    public static void Death() {
        SoundManager.PlayGameOver();
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(3);
    }

    public static void StartTimer() {
        instance.timer.StartTimer();
        instance.disabledEnemies.SetActive(true);
        instance.theWin.enabled = true;
    }

    public static void TimerEnd() {
        SoundManager.PlayGameOver();
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(3);
    }

    public static void WinGame() {
        instance.CalculateScore();
        SoundManager.PlayVictory();
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(2);
    }

    public void CalculateScore()
    {
        int score = 0;
        foreach(var item in items)
        {
            score += item.storedWeapon.damage * (item.currentDurability / item.storedWeapon.durabilityLostPerHit);
        }
        SoundManager.score = score;
    }
}
