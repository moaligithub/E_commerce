using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Core.Settings
{
    /// <summary>
    ///     this class special for mail settings
    /// </summary>
    public class MailSetting
    {
        /// <summary>
        ///     this property expresses the e-mail address that beint by is send mail.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     this property expresses the Name Display in mail message.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        ///     this property expresses the password special for e-mail sender.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     this property expresses mail Host.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        ///     this property expresses mail Port.
        /// </summary>
        public int Port { get; set; }
    }
}
