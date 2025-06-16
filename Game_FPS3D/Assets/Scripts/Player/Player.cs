// Các using statement của bạn đã đầy đủ
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // --- PHẦN 1: LOGIC SINGLETON "BẤT TỬ" (PHẦN MỚI) ---
    public static Player instance;

    // --- CÁC BIẾN CŨ CỦA BẠN ---
    public int HP = 50;
    public int mau;
    public GameObject bloodySreen;
    public TextMeshProUGUI playerHealthUI;
    public GameObject gameOverUI;
    public GameObject WeaponOver;
    public bool isDead;

    public void takeDamage(int damage)
    {
        if (isDead) return; // Nếu đã chết thì không nhận thêm sát thương

        HP -= damage;
        mau -= damage; // Biến 'mau' có vẻ giống 'HP', bạn có thể xem xét dùng 1 biến
        print(damage);

        if (HP <= 0)
        {
            HP = 0; // Tránh máu bị âm
            print("player dead");
            PlayerDead();
            isDead = true;
        }
        else
        {
            print("player hit");
            playerHealthUI.text = $"Health:{HP}";
            StartCoroutine(BloodyScreenEffect());
            // SoundManage.instance.playerSource.PlayOneShot(SoundManage.instance.playerHurt); // Bật lại nếu bạn đã có SoundManage
        }
    }

    private void PlayerDead()
    {
        // SoundManage.instance.playerSource.PlayOneShot(SoundManage.instance.playerDead);
        GetComponent<MouseMovement>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;

        // Animator nên được bật lại một cách cẩn thận, có thể nó không nên bị tắt ở HidePlayerInMenu
        if (GetComponentInChildren<Animator>() != null) GetComponentInChildren<Animator>().enabled = true;

        playerHealthUI.gameObject.SetActive(false);
        if (WeaponOver != null) WeaponOver.gameObject.SetActive(false);

        if (GetComponent<SrceenBlackOut>() != null) GetComponent<SrceenBlackOut>().StartFade(); // Giả sử script này tồn tại
        StartCoroutine(GameOverUI());
    }

    private IEnumerator GameOverUI()
    {
        yield return new WaitForSeconds(1f);
        if (gameOverUI != null) gameOverUI.gameObject.SetActive(true);

        // Logic lưu điểm
        // int wave = GobalReferences.instance.waveNumber;
        // if (wave > SaveLoadManager.instance.LoadScore())
        // {
        //     SaveLoadManager.instance.SaveScore(wave - 1);
        // }
        StartCoroutine(ReturnMainMenu());
    }

    private IEnumerator ReturnMainMenu()
    {
        yield return new WaitForSeconds(5f);
        // !!! THAY "Menu" BẰNG TÊN SCENE MENU CỦA BẠN !!!
        SceneManager.LoadScene("Menu");
    }

    private IEnumerator BloodyScreenEffect()
    {
        if (bloodySreen == null) yield break; // Dừng lại nếu không có bloody screen

        if (bloodySreen.activeInHierarchy == false)
        {
            bloodySreen.SetActive(true);
        }

        var image = bloodySreen.GetComponentInChildren<Image>();
        Color startColor = image.color;
        startColor.a = 1f;
        image.color = startColor;

        float duration = 3f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (bloodySreen.activeInHierarchy)
        {
            bloodySreen.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead) return;

        int st = 0;
        if (other.CompareTag("ZombieHand"))
        {
            ZombieHand hand = other.gameObject.GetComponentInChildren<ZombieHand>();
            if (hand != null && hand.damage != 0)
            {
                st = hand.damage;
                takeDamage(st);
            }
        }
    }
}