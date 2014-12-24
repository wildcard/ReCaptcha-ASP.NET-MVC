using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reCaptcha
{
    public enum ErrorCodes
    {
        /// <summary>
        /// No Errors
        /// </summary>
        None = 0,

        /// <summary>
        /// The secret parameter is missing.
        /// </summary>
        [Description("missing-input-secret")]
        MissingInputSecret = 1,

        /// <summary>
        /// The secret parameter is invalid or malformed.
        /// </summary>
        [Description("invalid-input-secret")]
        InvalidInputSecret = 2,

        /// <summary>
        /// The response parameter is missing. 
        /// </summary>
        [Description("missing-input-response")]
        MissingInputResponse = 4,

        /// <summary>
        /// The response parameter is invalid or malformed. 
        /// </summary>
        [Description("invalid-input-response")]
        InvalidInputResponse = 8
    }

}
