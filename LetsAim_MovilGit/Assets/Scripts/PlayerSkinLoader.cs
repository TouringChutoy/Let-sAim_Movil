using UnityEngine;

public class PlayerSkinLoader : MonoBehaviour
{
    public GameObject[] skinPrefabs; // Mismo arreglo de prefabs que en el selector

    void Start()
    {
        // Obtener el índice de la skin guardada
        int selectedSkinIndex = PlayerPrefs.GetInt("SelectedSkin", 0);

        // Asegurarse que el índice es válido
        if (selectedSkinIndex < 0 || selectedSkinIndex >= skinPrefabs.Length)
        {
            selectedSkinIndex = 0;
        }

        // Instanciar la skin seleccionada como hijo del player
        GameObject selectedSkin = Instantiate(skinPrefabs[selectedSkinIndex], transform.position, Quaternion.identity);
        selectedSkin.transform.SetParent(transform); // Hacerla hijo del player
    }
}