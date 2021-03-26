var ImageSaverPlugin = {
  ImageSaverSaveResult: function(data,size, name){
    var bytes = new Uint8Array(size);
    for (var i=0;i<size;i++){
      bytes[i] = HEAPU8[data + i];
    }
    var blob = new Blob([bytes],{type:'image/png'});
    var blobUrl = URL.createObjectURL(blob);
    var a = document.getElementById('ImageDownload')
    if (a==null){
      a = document.createElement("a");
      a.id = 'ImageDownload';
      document.body.appendChild(a);
      a.style = "display: none";
    }
    a.href = blobUrl;
    a.download = name;
    a.click();
    window.URL.revokeObjectURL(blobUrl);

  }
};

mergeInto(LibraryManager.library, ImageSaverPlugin);
