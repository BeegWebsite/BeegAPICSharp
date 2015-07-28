# BeegAPICSharp
Beeg API for CSharp (C#)

## Why ?

Because Beeg is a fucking awesome website.

## How to use ?

#### • BeegVideo

```csharp
//Declare new BeegVideo
BeegVideo video = new BeegVideo("7241201");

//Load all information
video.load();

//Show the ID of the video
Console.WriteLine("Video ID : " + video.getID());

//Show the title
Console.WriteLine("Title : " + video.getTitle());

//Show the description
Console.WriteLine("Description : " + video.getDescription());

//Show the published date
Console.WriteLine("Published date : " + video.getPublishedDate());

//Show the casting
Console.WriteLine("Casting : " + video.getCasting());

//Show the video URL on good (480p) quality
Console.WriteLine("Video URL : " + video.getURL(BeegVideo.BeegQuality.Good));

//Show the thumbnail URL
Console.WriteLine("Thumbnail URL : " + video.getThumbnailURL());

//Get the thumbnail URL as a bitmap and save it
video.getThumbnail().Save("image.png");

//Show the best quality available (fast/good/best or null if error)
Console.WriteLine("Best quality (BeegQuality) : " + video.getBestQuality());

//Show the best quality available (240/480/720 or -1 if error)
Console.WriteLine("Best quality (pixels) : " + video.getBestQualityInPixels());
```

#### • BeegVideoDownloader

```csharp
//Declare new BeegVideoDownloader
BeegVideoDownloader videoDownloader = new BeegVideoDownloader("7241201");
//Or
BeegVideoDownloader videoDownloader = new BeegVideoDownloader(new BeegVideo("7241201"));

//Add events to invoke
videoDownloader.downloadProgress += videoDownloader_downloadProgress;
videoDownloader.downloadFinish += videoDownloader_downloadFinish;

//Start the download
videoDownloader.download("C:/downloads", videoDownloader.getVideo().getBestQuality());

//Download progress event
static void videoDownloader_downloadProgress(object sender, System.Net.DownloadProgressChangedEventArgs e)
{
    Console.WriteLine("Download progess : " + e.ProgressPercentage + "%");
}

//Download complete event
static void videoDownloader_downloadFinish(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
{
    Console.WriteLine("Download complete");
}
```
