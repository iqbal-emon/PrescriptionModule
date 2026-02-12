using System.Text.Json.Serialization;

namespace AuthenticationSystem.Dtos.RequestDto
{
    public class FirebaseVerifyRequestDto
    {
        [JsonPropertyName("firebaseToken")]
        public string? FirebaseToken { get; set; }
        
        [JsonPropertyName("idToken")]
        public string? IdToken { get; set; }
    }
}

