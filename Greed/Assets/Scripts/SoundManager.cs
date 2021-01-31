using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    static SoundManager instance;

    // Sound effects
    [SerializeField] AudioSource swordSwing;
    [SerializeField] AudioSource spearSwing;
    [SerializeField] AudioSource axeSwing;
    [SerializeField] AudioSource walking;
    [SerializeField] AudioSource running; // maybe?
    [SerializeField] AudioSource playerHit;
    [SerializeField] AudioSource playerDeath;
    [SerializeField] AudioSource clockSlow;
    [SerializeField] AudioSource clockFast;
    [SerializeField] AudioSource ghost;
    [SerializeField] AudioSource ghostDeath;
    [SerializeField] AudioSource enemyHit;

    // Music
    [SerializeField] AudioSource menuMusic;
    [SerializeField] AudioSource roamingMusic;
    [SerializeField] AudioSource fightMusic;
    [SerializeField] AudioSource victoryMusic;
    [SerializeField] AudioSource gameOverMusic;

    bool outOfCombatTimer;

    private void Awake() {
        if (!instance) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else Destroy(gameObject);

        PlayMenu();
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public static void PlayAttack(int weaponType) {
        switch (weaponType) {
            case 0:
                if (!instance.swordSwing.isPlaying) instance.swordSwing.Play();
                break;
            case 1:
                if (!instance.spearSwing.isPlaying) instance.spearSwing.Play();
                break;
            case 2:
                if (!instance.axeSwing.isPlaying) instance.axeSwing.Play();
                break;
        }
    }

    public static void PlayWalk() {
        if (!instance.walking.isPlaying) instance.walking.Play();
    }

    public static void PlayRun() {
        if (!instance.running.isPlaying) instance.running.Play();
    }

    public static void PlayHurt() {
        instance.playerHit.Play();
    }

    public static void PlayDeath() {
        instance.playerDeath.Play();
    }

    public static void PlayClockSlow() {
        instance.clockSlow.Play();
    }

    public static void PlayClockFast() {
        if (!instance.clockFast.isPlaying) {
            instance.clockSlow.Stop();
            instance.clockFast.Play();
        }
    }

    public static void PlayGhost() {
        instance.ghost.Play();
    }

    public static void PlayGhostDeath() {
        instance.ghostDeath.Play();
    }

    public static void PlayEnemyHit() {
        instance.enemyHit.Play();
    }

    void PlayMenu() {
        menuMusic.volume = 0;
        menuMusic.Play();
        StartCoroutine(MenuFade());
    }

    public static void PlayRoaming() {
        instance.menuMusic.Stop();
        instance.fightMusic.Stop();
        instance.roamingMusic.Play();
    }

    public static void PlayFight() {
        instance.outOfCombatTimer = false;
        instance.StopCoroutine(instance.OutOfCombatTimer());
        instance.roamingMusic.Stop();
        instance.fightMusic.Play();
    }

    public static void PlayVictory() {
        instance.fightMusic.Stop();
        instance.roamingMusic.Stop();
        instance.victoryMusic.Play();
    }

    public static void PlayGameOver() {
        instance.fightMusic.Stop();
        instance.roamingMusic.Stop();
        instance.gameOverMusic.Play();
    }

    public static void OutOfCombat() {
        if (!instance.outOfCombatTimer) instance.StartCoroutine(instance.OutOfCombatTimer());
    }

    IEnumerator MenuFade() {
        while(menuMusic.volume <= 0.25f) {
            menuMusic.volume += 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator OutOfCombatTimer() {
        instance.outOfCombatTimer = true;
        for (int i = 5; i > 0; i--) {
            yield return new WaitForSeconds(1f);
        }
        PlayRoaming();
    }
}
