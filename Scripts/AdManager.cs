using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        Advertisement.Initialize("3918949", false);

        while (!Advertisement.IsReady())
            yield return null;

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show("Banner");
    }

 
}
