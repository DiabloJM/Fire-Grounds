using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScript : MonoBehaviour
{
    //Variables publicas
    [Header("Barra de Carga y Porcentaje")]
    public Image loadingBarFill;
    public Text percentageText;

    [Header("ID de escena a Cargar")]
    public int scene;

    //Variables Privadas
    private int percentage;  //Porcentaje de carga

    private void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);

        while(!operation.isDone)
        {
            //Se aumenta la cantidad de carga de la barra
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            //Ponemos este avance en la barra
            loadingBarFill.fillAmount = progressValue;
            //Pasamos el fillAmount a porcentaje y luego lo mostramos en texto
            percentage = (int)(progressValue * 100f);
            percentageText.text = percentage.ToString();

            yield return null;
        }
    }
}
