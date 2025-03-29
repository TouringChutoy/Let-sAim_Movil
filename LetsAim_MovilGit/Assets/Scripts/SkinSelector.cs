using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SkinSelector : MonoBehaviour
{
    public GameObject[] skinPrefabs; // Arreglo de prefabs de skins disponibles
    public Image previewImage; // UI Image para mostrar la preview
    public TextMeshProUGUI skinNameText; // Texto para mostrar el nombre
    
    private int currentSkinIndex = 0;
    
    void Start()
    {
        // Cargar la skin seleccionada previamente
        currentSkinIndex = PlayerPrefs.GetInt("SelectedSkin", 0);
        UpdatePreview();
    }
    
    public void NextSkin()
    {
        currentSkinIndex++;
        if(currentSkinIndex >= skinPrefabs.Length)
        {
            currentSkinIndex = 0;
        }
        UpdatePreview();
    }
    
    public void PreviousSkin()
    {
        currentSkinIndex--;
        if(currentSkinIndex < 0)
        {
            currentSkinIndex = skinPrefabs.Length - 1;
        }
        UpdatePreview();
    }
    
    private void UpdatePreview()
    {
        // Mostrar sprite de preview (asumiendo que el prefab tiene un SpriteRenderer)
        SpriteRenderer prefabRenderer = skinPrefabs[currentSkinIndex].GetComponent<SpriteRenderer>();
        if(prefabRenderer != null)
        {
            previewImage.sprite = prefabRenderer.sprite;
        }
        
        // Mostrar nombre del prefab
        skinNameText.text = skinPrefabs[currentSkinIndex].name;
    }
    
    public void ConfirmSelection()
    {
        // Guardar la selección
        PlayerPrefs.SetInt("SelectedSkin", currentSkinIndex);
        PlayerPrefs.Save();
        
        // Cargar la escena del juego (ajusta el nombre según tu escena)
        SceneManager.LoadScene(5);
    }
}