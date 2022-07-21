using Newtonsoft.Json;

namespace KIT.RocketChat.ApiClient.Methods.Login.Models;

/// <summary>
///     User email address
/// </summary>
public class Email
{
    public Email(string address, bool isVerified)
    {
        Address = address;
        IsVerified = isVerified;
    }

    /// <summary>
    ///     User email
    /// </summary>
    [JsonProperty("address")] 
    public string Address { get; set; }

    /// <summary>
    ///     Email address is verified
    /// </summary>
    [JsonProperty("verified")] 
    public bool IsVerified { get; set; }
}