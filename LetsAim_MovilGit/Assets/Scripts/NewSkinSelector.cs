using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class NewSkinSelector : MonoBehaviour
{
    [Header("Referencias de UI")]
    public SkinData[] skins; // Asigna 11 elementos: 10 de pago y 1 default
    public Image previewImage;
    public TextMeshProUGUI skinNameText;
    public TextMeshProUGUI skinPriceText;
    public TextMeshProUGUI coinsText;
    public Button buyButton;
    public Button selectButton;

    private int currentSkinIndex = 0;
    private int playerCoins;

    void Start()
    {
        // Asigna el índice a cada SkinData y llama a Initialize para cada una.
        for (int i = 0; i < skins.Length; i++)
        {
            skins[i].skinIndex = i;
            skins[i].Initialize();
        }

        // Cargamos las monedas del jugador.
        playerCoins = PlayerPrefs.GetInt("PuntajeGuardado", 0);
        UpdateCoinsUI();

        // Se carga la skin seleccionada. Si el índice almacenado es erróneo, se usa la 0.
        currentSkinIndex = PlayerPrefs.GetInt("SelectedSkin", 0);
        if (currentSkinIndex < 0 || currentSkinIndex >= skins.Length)
            currentSkinIndex = 0;

        UpdatePreview();
    }

    public void NextSkin()
    {
        currentSkinIndex = (currentSkinIndex + 1) % skins.Length;
        UpdatePreview();
    }

    public void PreviousSkin()
    {
        currentSkinIndex = (currentSkinIndex - 1 + skins.Length) % skins.Length;
        UpdatePreview();
    }

    private void UpdatePreview()
    {
        SkinData currentSkin = skins[currentSkinIndex];
        
        // Actualizamos imagen y nombre.
        previewImage.sprite = currentSkin.previewSprite;
        skinNameText.text = currentSkin.skinName;
        
        // Si la skin está desbloqueada (o es por defecto), mostramos "Desbloqueada"; de lo contrario, se muestra el precio.
        if (currentSkin.unlocked)
            skinPriceText.text = "Desbloqueada";
        else
            skinPriceText.text = $"Precio: {currentSkin.price}";
        
        // Forzamos la actualización inmediata del texto (muy útil con TextMeshPro).
        skinPriceText.ForceMeshUpdate();

        // Configuramos la visibilidad de botones.
        buyButton.gameObject.SetActive(!currentSkin.unlocked);
        selectButton.gameObject.SetActive(currentSkin.unlocked);

        // Solo permite la compra si el jugador tiene suficientes monedas y la skin no está desbloqueada.
        if (!currentSkin.unlocked)
            buyButton.interactable = playerCoins >= currentSkin.price;
    }

    private void UpdateCoinsUI()
    {
        coinsText.text = $"Monedas: {playerCoins}";
    }

    public void BuySkin()
    {
        SkinData currentSkin = skins[currentSkinIndex];

        // Verifica que exista suficiente moneda y que la skin no esté ya desbloqueada.
        if (!currentSkin.unlocked && playerCoins >= currentSkin.price)
        {
            playerCoins -= currentSkin.price;
            PlayerPrefs.SetInt("PuntajeGuardado", playerCoins);
            AchievementManager.Instance.PrimerSkin();
            // Desbloquea la skin actualizando el estado en memoria y, para skins de pago, en PlayerPrefs.
            currentSkin.unlocked = true;
            PlayerPrefs.Save();

            UpdateCoinsUI();
            UpdatePreview();
        }
    }

    public void SelectSkin()
    {
        // Permite seleccionar la skin solo si está desbloqueada.
        if (skins[currentSkinIndex].unlocked)
        {
            PlayerPrefs.SetInt("SelectedSkin", currentSkinIndex);
            PlayerPrefs.Save();
            SceneManager.LoadScene(5);
        }
    }
}

[System.Serializable]
public class SkinData
{
    public string skinName;
    public Sprite previewSprite;
    public GameObject skinPrefab;
    public int price;
    [Tooltip("Marca esta opción para la skin por defecto, que siempre estará desbloqueada.")]
    public bool isDefaultSkin = false;

    [System.NonSerialized]
    public int skinIndex;

    // Variable privada para almacenar el estado de desbloqueo en memoria.
    [System.NonSerialized]
    private bool _unlocked;

    /// <summary>
    /// Inicializa el estado de la skin:
    /// - Si es default, se fuerza a estar desbloqueada (y se actualiza PlayerPrefs para evitar inconsistencias).
    /// - Si no es default, se lee el estado desde PlayerPrefs.
    /// </summary>
    public void Initialize()
    {
        if (isDefaultSkin)
        {
            _unlocked = true;
            PlayerPrefs.SetInt($"Skin_{skinIndex}_Unlocked", 1);
        }
        else
        {
            _unlocked = PlayerPrefs.GetInt($"Skin_{skinIndex}_Unlocked", 0) == 1;
        }
    }

    public bool unlocked
    {
        get
        {
            if (isDefaultSkin)
                return true;
            return _unlocked;
        }
        set
        {
            // Solo se permite modificar para skins de pago.
            if (!isDefaultSkin)
            {
                _unlocked = value;
                PlayerPrefs.SetInt($"Skin_{skinIndex}_Unlocked", value ? 1 : 0);
            }
        }
    }
}
