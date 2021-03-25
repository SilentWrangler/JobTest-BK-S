using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class FileLoader : MonoBehaviour
{
  public Image input;
  public Image output;
  public TMP_Dropdown fileChoice;
  public Texture2D[] example;
  public Button convert, save, load;
  public TextMeshProUGUI pathLabel;
  Texture2D inp;
  Texture2D outp;
  string fname;


//Values from Wikipedia
  const float lumRed = 0.2126f;
  const float lumGreen = 0.7152f;
  const float lumBlue = 0.0722f;


    // Start is called before the first frame update
    void Start()
    {
      #if !UNITY_EDITOR && UNITY_WEBGL
        // disable WebGLInput.captureAllKeyboardInput so elements in web page can handle keabord inputs
        WebGLInput.captureAllKeyboardInput = false;
        fileChoice.gameObject.SetActive(false);
        pathLabel.gameObject.SetActive(false);
        load.gameObject.SetActive(true);
        return;
      #endif



      if (!PlayerPrefs.HasKey("example")){
        for (int i=0; i<example.Length;i++){
          string path = Application.persistentDataPath + "/example"+i.ToString()+".png";
          SaveImage(example[i], path);
        }
        PlayerPrefs.SetInt("example", 1);
      }
      DirectoryInfo directoryInfo = new DirectoryInfo (Application.persistentDataPath);
      FileInfo[] fileInfo = directoryInfo.GetFiles ("*.png", SearchOption.AllDirectories);
      fileChoice.options.Clear ();
      foreach (FileInfo file in fileInfo) {
        TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData (file.Name);
        fileChoice.options.Add (optionData);
        fileChoice.value = 1;
      }
      pathLabel.text = Application.platform.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PathToBuffer(){
      GUIUtility.systemCopyBuffer = pathLabel.text;
    }

    public void LoadImage(Texture2D tex){
      Sprite s = Sprite.Create(tex, new Rect(0,0,tex.width,tex.height), new Vector2(0.5f,0.5f));

      input.sprite = s;
      inp = tex;
      convert.interactable = true;
      save.interactable = false;
    }
    public void LoadImage(int option){
      string path = fileChoice.options[option].text;
      Texture2D tex = LoadTexture(Application.persistentDataPath + "/"+path);
      Sprite s = Sprite.Create(tex, new Rect(0,0,tex.width,tex.height), new Vector2(0.5f,0.5f));

      input.sprite = s;
      inp = tex;
      fname = "/grayscale-"+path;
      convert.interactable = true;
      save.interactable = false;
    }

    public Texture2D LoadTexture(string path){
        byte[] bts = System.IO.File.ReadAllBytes(path);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(bts);
        return tex;
    }


    public void SaveImage(Texture2D tex, string path){
      System.IO.File.WriteAllBytes (path, tex.EncodeToPNG());
    }

    public void ConvertImage(){
      convert.interactable = false;
      outp = new Texture2D(inp.width, inp.height, inp.format, false);
      Color[] pixels = inp.GetPixels();
      for (int i=0;i<pixels.Length;i++) {
        pixels[i].r = pixels[i].grayscale;
        pixels[i].g = pixels[i].grayscale;
        pixels[i].b = pixels[i].grayscale;
      }
      outp.SetPixels(pixels);
      outp.Apply();
      Sprite s = Sprite.Create(outp, new Rect(0,0,outp.width,outp.height), new Vector2(0.5f,0.5f));
      output.sprite = s;
      save.interactable = true;
    }

    public void saveConvert(){
      string path = Application.persistentDataPath + fname;
      SaveImage(outp, path);
      save.interactable = false;
    }
}
