using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class APIClient : MonoBehaviour
{
    private string apiUrl = "http://192.168.42.10:5005/student/servicios/30000080498";
    
    public TMP_InputField inputField;

    void Start()
    {
        StartCoroutine(GetRequest(apiUrl));
    }

    public void SendGetRequest()
    {
        StartCoroutine(GetRequest(apiUrl));
    }
    
    public void SendPostRequest()
    {
        string jsonData = "{\"key\":\"" + inputField.text + "\"}";
        StartCoroutine(PostRequest(apiUrl, jsonData));
    }
    
    public void SendPutRequest()
    {
        string jsonData = "{\"updateKey\":\"" + inputField.text + "\"}";
        StartCoroutine(PutRequest(apiUrl, jsonData));
    }

    IEnumerator GetRequest(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error en la solicitud: " + webRequest.error);
            }
            else
            {
                Debug.Log("Respuesta recibida: " + webRequest.downloadHandler.text);
            }
        }
    }

    IEnumerator PostRequest(string url, string jsonData)
    {
        using (UnityWebRequest webRequest = new UnityWebRequest(url, "POST"))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
            webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error en la solicitud POST: " + webRequest.error);
            }
            else
            {
                Debug.Log("Respuesta recibida: " + webRequest.downloadHandler.text);
            }
        }
    }
    
    IEnumerator PutRequest(string url, string jsonData)
    {
        using (UnityWebRequest webRequest = new UnityWebRequest(url, "PUT"))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
            webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error en la solicitud PUT: " + webRequest.error);
            }
            else
            {
                Debug.Log("Respuesta recibida: " + webRequest.downloadHandler.text);
            }
        }
    }
}
