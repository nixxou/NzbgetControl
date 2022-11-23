# NzbGetControl

NzbGet Wrapper for windows to make a simple command line nzb downloader and add contextual menu on nzb file for quick download.

It also has a feature to put temporary files into a dynamically generated ramdisk using ImDisk.

NzbGet will be auto-close after the download completion.


## Usage/Examples


```
NzbgetControl.exe --host news.mynewsserver.com --port 119 --user myusername --password mypass --connections 10 MyFile.nzb

Other command lines options :
--ssl : Enable ssl
--out <value> : Force extraction on a specific directory

--ramdisk : Create a ramdisk and use it to put temporary files on it
You need to run the app with admin rights and have enought free memory or it will fallback to non-ramdisk download
Ramdisk will be auto deleted after download

--cleanram : If you use a ramdisk, the app will clean your memory to free some of it before launching.

--saveini : Parameters will be save in ini file, to allow you to run the app with just the nzb file as parameters.

--register : Register windows contextual menu for nzb files

--unregister : Unregister windows contextual menu for nzb files
```
## Screenshots

A GUI is provided to configure without command line and download/install dependencies :
![GUI](https://user-images.githubusercontent.com/45721836/203541671-ebf21363-98f3-42ec-8646-79e802833aad.png)
![Command prompt](https://user-images.githubusercontent.com/45721836/203541677-cb98d56c-953c-4ad2-b774-7cc84ee9e10e.png)
![Contextual menu](https://user-images.githubusercontent.com/45721836/203541681-bc507f30-bdad-4b58-bbae-872187909540.png)
