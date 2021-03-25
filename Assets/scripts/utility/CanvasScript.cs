using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class CanvasScript : MonoBehaviour {


    public FileLoader loader;
    [DllImport("__Internal")]
    private static extern void ImageUploaderCaptureClick();

    IEnumerator LoadTexture (string url) {
        WWW image = new WWW (url);
        yield return image;
        Texture2D texture = new Texture2D (1, 1);
        image.LoadImageIntoTexture (texture);
        loader.LoadImage(texture);
    }

    void FileSelected (string url) {
        StartCoroutine(LoadTexture (url));
    }

    public void OnButtonPointerDown () {
        ImageUploaderCaptureClick ();
    }
}
