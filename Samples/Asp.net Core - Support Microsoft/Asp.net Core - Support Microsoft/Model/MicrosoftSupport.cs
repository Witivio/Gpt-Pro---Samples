namespace Asp.net_Core___Support_Microsoft.Model
{
    public class MicrosoftSupport
    {
        /// <summary>
        /// Microsoft support response title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Encoded link to Microsoft Support response
        /// </summary>
        public string LinkEncoded { get; set; }

        /// <summary>
        /// Link to Microsoft Support response
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Description of the solution to be provided
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Application in relation to the solution provided
        /// </summary>
        public string AppliesTo { get; set; }
    }
}
