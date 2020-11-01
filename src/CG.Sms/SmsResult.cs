using System;

namespace CG.Sms
{
    /// <summary>
    /// This class represents the result of an SMS operation.
    /// </summary>
    public class SmsResult
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains a strategy specific value that represents
        /// this SMS operation.
        /// </summary>
        public string SmsId { get; set; }

        #endregion
    }
}
