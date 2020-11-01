using System;
using System.Runtime.Serialization;

namespace CG.Sms
{
    /// <summary>
    /// This class represents an SMS service related exception.
    /// </summary>
    [Serializable]
    public class SmsServiceException : Exception
    {
        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="SmsServiceException"/>
        /// class.
        /// </summary>
        public SmsServiceException()
        {

        }

        // *******************************************************************

        /// <summary>
        /// This constructor creates a new instance of the <see cref="SmsServiceException"/>
        /// class.
        /// </summary>
        /// <param name="message">The message to use for the exception.</param>
        /// <param name="innerException">An optional inner exception reference.</param>
        public SmsServiceException(
            string message,
            Exception innerException
            ) : base(message, innerException)
        {

        }

        // *******************************************************************

        /// <summary>
        /// This constructor creates a new instance of the <see cref="SmsServiceException"/>
        /// class.
        /// </summary>
        /// <param name="message">The message to use for the exception.</param>
        public SmsServiceException(
            string message
            ) : base(message)
        {

        }

        // *******************************************************************

        /// <summary>
        /// This constructor creates a new instance of the <see cref="SmsServiceException"/>
        /// class.
        /// </summary>
        /// <param name="info">The serialization info to use for the exception.</param>
        /// <param name="context">The streaming context to use for the exception.</param>
        public SmsServiceException(
            SerializationInfo info,
            StreamingContext context
            ) : base(info, context)
        {

        }

        #endregion
    }
}
