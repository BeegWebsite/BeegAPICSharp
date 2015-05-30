# BeegAPICSharp
Beeg API for CSharp (C#)

## Why ?

Because Beeg is a fucking awesome website.

## How to use ?

```
//Declare new BeegVideo
BeegVideo video = new BeegVideo("7241201");

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
Console.WriteLine("Thumbnail URL : " + video.getURLThumbnail());

//Get the thumbnail URL as a bitmap
Bitmap thumbnail = video.getThumbnail();

//Save the thumbnail's bitmap
thumbnail.Save("image.png");
```
