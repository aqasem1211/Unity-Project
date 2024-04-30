using BestHTTP;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        CreateAvatarRequestRoot request = new CreateAvatarRequestRoot();
        request.Image = userImageURL;

        string jsonRequestBody = JsonConvert.SerializeObject(request);

        CallCreateAvatarAPI(jsonRequestBody);
    }

    private void CallCreateAvatarAPI(string jsonRequestBody)
    {
        var url = "https://api.28-app.com/api/Face/AvatarCreate";
        var request = new HTTPRequest(new Uri(url), HTTPMethods.Post, OnDownloadGenerated3DHeadResponse);
        request.SetHeader("accept", "*/*");
        request.SetHeader("Content-Type", "application/json; charset=UTF-8");
        request.RawData = System.Text.Encoding.UTF8.GetBytes(jsonRequestBody);
        request.Send();
    }

    private void OnDownloadGenerated3DHeadResponse(HTTPRequest originalRequest, HTTPResponse response)
    {
        Debug.Log("Request sent successfully");
        Debug.Log("Response: " + response.DataAsText);

        var _response = JsonConvert.DeserializeObject<Root>(response.DataAsText);
        SetBlendShapesFromRepresentation(_response);
        SetAvatarFaceMaterial(_response);
    }

    private void SetAvatarFaceMaterial(Root response)
    {
        var faceTexture = response.Data.Representation.Textures.HeadTexture;
        var faceMaterial = avatarSkinnedMeshRenderer.sharedMaterials.FirstOrDefault(mat => mat.name == "Face");

        StartCoroutine(LoadFaceTexture(faceTexture, faceMaterial));
    }

    private IEnumerator LoadFaceTexture(string url, Material material)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("www.error:" + www.error);
        }
        else
        {
            Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            material.mainTexture = myTexture;
        }
    }

    public void SetBlendShapesFromRepresentation(Root response)
    {
        if (avatarSkinnedMeshRenderer == null)
        {
            Debug.LogError("SkinnedMeshRenderer is not assigned!");
            return;
        }

        for (int i = 0; i < response.Data.Representation.Morphs.Labels.Count; i++)
        {
            string blendShapeName = response.Data.Representation.Morphs.Labels[i];
            float blendShapeValue = (float)response.Data.Representation.Morphs.Values[i];

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
