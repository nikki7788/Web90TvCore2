using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Web90TvCore2.PublicClass;

namespace Web90TvCore2.Models
{
    /// <summary>
    /// دسته بندی اخبار
    /// </summary>

    [Table("Category")]
    public class Category
    {
        #region  ########################### Constructor ################

        public Category()
        {

        }

        #endregion
        #region######################### Properties ########################################

        [Key]
        public int CategoryId { get; set; }

        /// <summary>
        /// عنوان دسته بندی
        /// </summary>
       
        [Display(Name = "دسته بندی")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [StringLength(150, MinimumLength = 4, ErrorMessage = PublicConst.LengthMessage)]
        [RegularExpression(@"[0-9A-Zا-ی ء ؤ ئ ةأإآa-z_\s\-\(\)\.]+", ErrorMessage = PublicConst.DangrouseMessageForBadCharachter)]
        public string Title { get; set; }

        /// <summary>
        /// توضیحات دسته بندی
        /// </summary>
       
        [Display(Name = "توضیحات")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [StringLength(800, MinimumLength = 10, ErrorMessage = PublicConst.LengthMessage)]
        [RegularExpression(@"[0-9A-Zا-ی ء ؤ ئ ةأإآa-z_\s\-\(\)\.]+", ErrorMessage = PublicConst.DangrouseMessageForBadCharachter)]
        public string Description { get; set; }

        #endregion

        #region ######################## navigation Properties ############################

        #endregion
    }
}
