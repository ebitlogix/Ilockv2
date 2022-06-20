namespace ILock.Core.Data.Models
{
    /// <summary>
    /// The api response base.
    /// </summary>
    public class ApiResponseBase<T> 
        where T : class
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public T Data { get; set; }
    }
}
