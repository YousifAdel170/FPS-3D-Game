using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class PlayerHealth : MonoBehaviour
{

    private float health;
    private float lerpTimer;
    [Header("Health Bar")]
    public float maxHealth = 100f;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;
    public TextMeshProUGUI healthText;

    [Header("Damage Overlay")]
    public Image overlay;   // Our Damage Overlay Game object
    public float duration;  // how long the image stays fully opaque
    public float fadeSpeed; // how quickly the image will fade
    private float durationTimer;    // Timer to check against the duration

    [Header("You Lost")]
    public TextMeshProUGUI youLostText; // Reference to the "You Lost" TextMeshPro text



    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
        youLostText.gameObject.SetActive(false); // Initially hide the "You Lost" text
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(health);
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
        if (health < 30)
            return;
        if (overlay.color.a > 0)
        {
            durationTimer += Time.deltaTime;
            if (durationTimer > duration)
            {
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }
    }

    public void UpdateHealthUI()
    {
        Debug.Log(health);
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;    // to be between 0 & 1 range
        // set back health bar to red
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        // set back health bar to green 
        if (fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
        healthText.text = health + "/" + maxHealth;

    }
    public void TakeDamage(float damage)
    {
        health -= damage;   
        lerpTimer = 0f;
        durationTimer = 0;  // each time take damage the time reset
        if (health < 50)
        {
            overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
        }
        if(health <= 0)
        {
            youLostText.gameObject.SetActive(true);
            Invoke("RestartGame", 2f);
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void RestartGame()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
        if (health > 50)
        {
            overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
        }
    }
}
