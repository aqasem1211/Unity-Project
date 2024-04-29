using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AvatarCreator : MonoBehaviour
{
    public TMP_InputField userImageURLInputField;
    public Button createAvatar;
    public SkinnedMeshRenderer avatarSkinnedMeshRenderer;
    void Start()
    {
        // Allow insecure connections (not recommended for production)
        ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;
    }
    public void CreateAvatar()
    {
        var userImageURL = userImageURLInputField.text;
        if (string.IsNullOrEmpty(userImageURL))
            return;

        StartCoroutine(DownloadImageRoutine(userImageURL));
    }

    private IEnumerator DownloadImageRoutine(string imageUrl)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(www);
                byte[] imageBytes = texture.EncodeToPNG();
                string base64String = Convert.ToBase64String(imageBytes);

                CreateAvatarRequestRoot request = new CreateAvatarRequestRoot();
                request.Image = base64String;

                string jsonRequestBody = JsonConvert.SerializeObject(request);

                StartCoroutine(CallCreateAvatarAPI(jsonRequestBody));
            }
        }
    }

    private IEnumerator CallCreateAvatarAPI(string jsonRequestBody)
    {
        string apiUrl = "http://46.4.77.151/doubt-the-urge/avatar/create";

        using (UnityWebRequest www = new UnityWebRequest(apiUrl, "POST"))
        {
            www.SetRequestHeader("Content-Type", "application/json");

            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequestBody);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Request sent successfully");
                Debug.Log("Response: " + www.downloadHandler.text);

                var _response = JsonConvert.DeserializeObject<CreateAvatarResponseRoot>(www.downloadHandler.text);

                SetBlendShapesFromRepresentation(_response);
            }
        }
    }
    public void SetBlendShapesFromRepresentation(CreateAvatarResponseRoot response)
    {
        if (avatarSkinnedMeshRenderer == null)
        {
            Debug.LogError("SkinnedMeshRenderer is not assigned!");
            return;
        }

        for (int i = 0; i < response.Representation.Morphs.Labels.Count; i++)
        {
            string blendShapeName = response.Representation.Morphs.Labels[i];
            float blendShapeValue = (float)response.Representation.Morphs.Values[i];

            // Find blend shape index by name
            int blendShapeIndex = avatarSkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendShapeName);
            if (blendShapeIndex != -1)
            {
                // Set blend shape value
                avatarSkinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex, blendShapeValue);
            }
            else
            {
                Debug.LogWarning("Blend shape not found: " + blendShapeName);
            }
        }
    }
    public static bool MyRemoteCertificateValidationCallback(System.Object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
    {
        // Return true to allow insecure connections
        return true;
    }
}
