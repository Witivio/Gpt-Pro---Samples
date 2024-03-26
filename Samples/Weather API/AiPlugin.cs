using System.Text.Json.Serialization;

namespace Witivio.WeatherApi
{
    /// <summary>
    /// Represents the root object of the AI plugin.
    /// </summary>
    public class AiPlugin
    {
        /// <summary>
        /// Gets or sets the schema version.
        /// </summary>
        [JsonPropertyName("schema_version")]
        public string SchemaVersion => "v1";

        /// <summary>
        /// Gets or sets the human-readable name.
        /// </summary>
        [JsonPropertyName("name_for_human")]
        public string NameForHuman { get; set; }

        /// <summary>
        /// Gets or sets the model name.
        /// </summary>
        [JsonPropertyName("name_for_model")]
        public string NameForModel { get; set; }

        /// <summary>
        /// Gets or sets the human-readable description.
        /// </summary>
        [JsonPropertyName("description_for_human")]
        public string DescriptionForHuman { get; set; }

        /// <summary>
        /// Gets or sets the model description.
        /// </summary>
        [JsonPropertyName("description_for_model")]
        public string DescriptionForModel { get; set; }

        /// <summary>
        /// Gets or sets the authentication information.
        /// </summary>
        [JsonPropertyName("auth")]
        public Auth Auth => new Auth();

        /// <summary>
        /// Gets or sets the API information.
        /// </summary>
        [JsonPropertyName("api")]
        public Api Api { get; set; }

        /// <summary>
        /// Gets or sets the URL of the logo.
        /// </summary>
        [JsonPropertyName("logo_url")]
        public string LogoUrl { get; set; }

        /// <summary>
        /// Gets or sets the contact email.
        /// </summary>
        [JsonPropertyName("contact_email")]
        public string ContactEmail => "contact@example.com";

        /// <summary>
        /// Gets or sets the URL of the legal information.
        /// </summary>
        [JsonPropertyName("legal_info_url")]
        public string LegalInfoUrl => "https://example.com/legal";

        /// <summary>
        /// Gets or sets the contact information.
        /// </summary>
        [JsonPropertyName("contact_info")]
        public string ContactInfo => "support@example.com";

        /// <summary>
        /// Gets or sets the legal information.
        /// </summary>
        [JsonPropertyName("legal_info")]
        public string LegalInfo => "Legal information";
    }

    /// <summary>
    /// Represents the authentication information.
    /// </summary>
    public class Auth
    {
        /// <summary>
        /// Gets or sets the type of authentication.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type => "none";
    }

    /// <summary>
    /// Represents the API information.
    /// </summary>
    public class Api
    {
        /// <summary>
        /// Gets or sets the type of API.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type => "openapi";

        /// <summary>
        /// Gets or sets the URL of the API.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}