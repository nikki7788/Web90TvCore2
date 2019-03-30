using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web90TvCore2.Models
{
    /// <summary>
    /// treeview کلاس مربوط به  
    /// </summary>
    /// نام تمام پراپرتی ها باید کوچک نوشته شود
    public class TreeViewNode
    {
        #region ################### properties #####################
        /// <summary>
        /// شناسه هر جز
        /// </summary>
        public string id { get; set; }


        /// <summary>
        /// پدر هر جز را مشخص میکند و در خود نگه میدارد
        /// ---شناسه پدر هر جز را نگه میدارد
        /// </summary>
        public string parent { get; set; }


        /// <summary>
        /// متن هر جز را در خود نگه میدارد
        /// </summary>
        public string text { get; set; }


        /// <summary>
        /// پراپرتی برای نمایش ایکن ها
        /// </summary>
        public string icon { get; set; }


        #endregion###################################
    }
}
