using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    static GameController instance;

    [SerializeField] WeaponDurability[] items;
    [SerializeField] PlayerController player;
    [SerializeField] CountDownTimer timer;

    private void Awake() {
        if (!instance) {
            instance = this;
        } else Destroy(gameObject);
    }

    // Update is called once per frame
    void Update() {

    }

    public static void ItemDamaged(BaseWeapon weapon) {
        for (int i = 0; i < instance.items.Length; i++) {
            if (instance.items[i].storedWeapon == weapon) {
                instance.items[i].TakeDamage(10);
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
        //gameover
        SoundManager.PlayGameOver();
    }

    public static void StartTimer() {
        instance.timer.StartTimer();
    }

    public static void TimerEnd() {
        //gameover
        SoundManager.PlayGameOver();
    }
}
