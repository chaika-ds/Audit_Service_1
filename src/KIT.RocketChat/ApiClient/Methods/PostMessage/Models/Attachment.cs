using Newtonsoft.Json;

namespace KIT.RocketChat.ApiClient.Methods.PostMessage.Models;

/// <summary>
///     Attachment file
/// </summary>
public class Attachment
{
    /// <summary>
    ///     The color you want the order on the left side to be, any value background-css supports.
    /// </summary>
    [JsonProperty("color")]
    public string? Color { get; set; }

    /// <summary>
    ///     The text to display for this attachment, it is different than the message's text.
    /// </summary>
    [JsonProperty("text")]
    public string? Text { get; set; }

    /// <summary>
    ///     Displays the time next to the text portion.
    /// </summary>
    [JsonProperty("ts")]
    public DateTime? TimeStamp { get; set; }

    /// <summary>
    ///     An image that displays to the left of the text, looks better when this is relatively small.
    /// </summary>
    [JsonProperty("thumb_url")]
    public string? ThumbUrl { get; set; }

    /// <summary>
    ///     Only applicable if the ts is provided, as it makes the time clickable to this link.
    /// </summary>
    [JsonProperty("message_link")]
    public string? MessageLink { get; set; }

    /// <summary>
    ///     Causes the image, audio, and video sections to be hiding when collapsed is true.
    /// </summary>
    [JsonProperty("collapsed")]
    public bool Collapsed { get; set; }

    /// <summary>
    ///     Name of the author.
    /// </summary>
    [JsonProperty("author_name")]
    public string? AuthorName { get; set; }

    /// <summary>
    ///     Providing this makes the author name clickable and points to this link.
    /// </summary>
    [JsonProperty("author_link")]
    public string? AuthorLink { get; set; }

    /// <summary>
    ///     Displays a tiny icon to the left of the Author's name.
    /// </summary>
    [JsonProperty("author_icon")]
    public string? AuthorIcon { get; set; }

    /// <summary>
    ///     Title to display for this attachment, displays under the author.
    /// </summary>
    [JsonProperty("title")]
    public string? Title { get; set; }

    /// <summary>
    ///     Providing this makes the title clickable, pointing to this link.
    /// </summary>
    [JsonProperty("title_link")]
    public string? TitleLink { get; set; }

    /// <summary>
    ///     When this is true, a download icon appears and clicking this saves the link to file.
    /// </summary>
    [JsonProperty("title_link_download")]
    public bool? TitleLinkDownload { get; set; }

    /// <summary>
    ///     The image to display, will be "big" and easy to see.
    /// </summary>
    [JsonProperty("image_url")]
    public string? ImageUrl { get; set; }

    /// <summary>
    ///     Audio file to play, only supports what html audio does.
    /// </summary>
    [JsonProperty("audio_url")]
    public string? AudioUrl { get; set; }

    /// <summary>
    ///     Video file to play, only supports what html video does.
    /// </summary>
    [JsonProperty("video_url")]
    public string? VideoUrl { get; set; }

    /// <summary>
    ///     An array of Attachment Field Objects.
    /// </summary>
    [JsonProperty("fields")]
    public IEnumerable<AttachmentField>? Fields { get; set; }
}