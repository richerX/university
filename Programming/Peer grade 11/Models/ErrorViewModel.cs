using System;

namespace Peergrade.Models
{
    public class ErrorViewModel
    {
        /// <summary>
        /// Поля класса.
        /// </summary>
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
