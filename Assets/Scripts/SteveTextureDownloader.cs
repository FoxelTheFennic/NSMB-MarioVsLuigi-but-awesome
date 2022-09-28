using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SteveTextureDownloader : MonoBehaviourPun
{
    string textureURLPrefix = "https://mc-heads.net/skin/";
    public SkinnedMeshRenderer skinnedMeshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetTexture());
    }

    IEnumerator GetTexture()
    {
        if (photonView.Owner.CustomProperties.TryGetValue(Enums.NetPlayerProperties.SteveSpriteURL, out var player))
        {
            if (player != null)
            {
                UnityWebRequest www = UnityWebRequestTexture.GetTexture(textureURLPrefix + player);
                yield return www.SendWebRequest();

                Texture myTexture = DownloadHandlerTexture.GetContent(www);
                if (myTexture != null)
                {
                    myTexture.filterMode = FilterMode.Point;
                    skinnedMeshRenderer.material.mainTexture = myTexture;
                }
            } else
            {
                Debug.Log("steve url null for " + photonView.Owner.NickName);
            }
        } else
        {
            Debug.Log("steve url not existing for " + photonView.Owner.NickName);
        }

    }
}
