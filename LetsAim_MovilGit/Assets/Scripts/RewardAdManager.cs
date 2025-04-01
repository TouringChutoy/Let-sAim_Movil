using Yodo1.MAS;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class RewardAdManager : MonoBehaviour
{
    [Header("Configuración")]
    public int rewardAmount = 20; // Cantidad de monedas a otorgar
    
    [Header("Referencias UI")]
    public Button showAdButton; // Botón que activará el anuncio
    public TextMeshProUGUI coinsText; // Texto para mostrar las monedas
    
    private int retryAttempt = 0;
    private bool adReady = false;
    private int playerCoins = 0;

    private void Start() 
    {
        // Cargar las monedas guardadas
        playerCoins = PlayerPrefs.GetInt("PuntajeGuardado", 0);
        UpdateCoinsUI();
        
        // Configurar el botón
        if (showAdButton != null)
        {
            showAdButton.onClick.AddListener(ShowRewardAd);
            showAdButton.interactable = false;
        }

        SetupEventCallbacks();
        LoadRewardAd();
    }

    private void SetupEventCallbacks()
    {
        Yodo1U3dRewardAd.GetInstance().OnAdLoadedEvent += OnRewardAdLoadedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdLoadFailedEvent += OnRewardAdLoadFailedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdOpenedEvent += OnRewardAdOpenedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdOpenFailedEvent += OnRewardAdOpenFailedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdClosedEvent += OnRewardAdClosedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdEarnedEvent += OnRewardAdEarnedEvent;
    }

    private void LoadRewardAd() 
    {
        Yodo1U3dRewardAd.GetInstance().LoadAd();
        adReady = false;
        
        if (showAdButton != null)
        {
            showAdButton.interactable = false;
        }
    }

    public void ShowRewardAd()
    {
        if (adReady)
        {
            Yodo1U3dRewardAd.GetInstance().ShowAd();
            
            if (showAdButton != null)
            {
                showAdButton.interactable = false;
            }
        }
        else
        {
            Debug.Log("El anuncio no está listo todavía");
            LoadRewardAd();
        }
    }

    private void OnRewardAdLoadedEvent(Yodo1U3dRewardAd ad)
    {
        Debug.Log("Anuncio de recompensa cargado");
        retryAttempt = 0;
        adReady = true;
        
        if (showAdButton != null)
        {
            showAdButton.interactable = true;
        }
    }

    private void OnRewardAdLoadFailedEvent(Yodo1U3dRewardAd ad, Yodo1U3dAdError adError)
    {
        Debug.LogError("Error al cargar anuncio: " + adError.ToString());
        
        retryAttempt++;
        double retryDelay = Math.Pow(2, Math.Min(6, retryAttempt));
        Invoke("LoadRewardAd", (float)retryDelay);
    }

    private void OnRewardAdOpenedEvent(Yodo1U3dRewardAd ad)
    {
        Debug.Log("Anuncio abierto");
        // Pausar el juego si es necesario
        Time.timeScale = 0f;
    }

    private void OnRewardAdOpenFailedEvent(Yodo1U3dRewardAd ad, Yodo1U3dAdError adError)
    {
        Debug.LogError("Error al abrir anuncio: " + adError.ToString());
        LoadRewardAd();
        Time.timeScale = 1f;
    }

    private void OnRewardAdClosedEvent(Yodo1U3dRewardAd ad)
    {
        Debug.Log("Anuncio cerrado");
        Time.timeScale = 1f;
        LoadRewardAd();
    }

    private void OnRewardAdEarnedEvent(Yodo1U3dRewardAd ad)
    {
        Debug.Log("Recompensa obtenida!");
        
        // Añadir monedas al jugador
        AddCoins(rewardAmount);
        
        // Mostrar feedback visual
        if (coinsText != null)
        {
            StartCoroutine(ShowRewardFeedback());
        }
    }

    private void AddCoins(int amount)
    {
        playerCoins += amount;
        PlayerPrefs.SetInt("PuntajeGuardado", playerCoins);
        PlayerPrefs.Save();
        UpdateCoinsUI();
    }

    private void UpdateCoinsUI()
    {
        if (coinsText != null)
        {
            coinsText.text = playerCoins.ToString();
        }
    }

    private System.Collections.IEnumerator ShowRewardFeedback()
    {
        string originalText = coinsText.text;
        coinsText.text = $"+{rewardAmount} monedas!";
        
        yield return new WaitForSeconds(2f);
        
        coinsText.text = playerCoins.ToString();
    }

    private void OnDestroy()
    {
        // Limpiar eventos
        Yodo1U3dRewardAd.GetInstance().OnAdLoadedEvent -= OnRewardAdLoadedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdLoadFailedEvent -= OnRewardAdLoadFailedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdOpenedEvent -= OnRewardAdOpenedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdOpenFailedEvent -= OnRewardAdOpenFailedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdClosedEvent -= OnRewardAdClosedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdEarnedEvent -= OnRewardAdEarnedEvent;
    }

    // Método para reiniciar las monedas (útil para testing)
    public void ResetCoins()
    {
        playerCoins = 0;
        PlayerPrefs.SetInt("PuntajeGuardado", playerCoins);
        PlayerPrefs.Save();
        UpdateCoinsUI();
    }
}