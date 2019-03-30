using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web90TvCore2.PublicClass
{
    /// <summary>
    /// Header Modal
    /// مقدار دهی هدر مودال
    /// </summary>
    public class ModalHeader
    {
        /// <summary>
        /// متن هدر مودال
        /// </summary>
        /// توسط کاربر مقدار دهی میشود
        ///the modal header text for dynamic header in the modal header
        public string Heading { get; set; }

        /// <summary>
        /// ورژن بوت استرپ
        /// </summary>
        public BootstrapVr BootstrapVersion { get; set; }


    }
    public enum BootstrapVr
    {
        version3 = 0,
        version4 = 1
    }
}
