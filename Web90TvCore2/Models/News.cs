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
    /// اخبار
    /// </summary>

    [Table("News")]
    public class News
    {


        #region  ########################### Constructor ################

        public News()
        {

        }

        #endregion

        #region ################# Properties ################################


        [Key]
        public int NewsId { get; set; }

        [Display(Name = "عنوان خبر")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [StringLength(250, MinimumLength = 4, ErrorMessage = PublicConst.LengthMessage)]
        [RegularExpression(@"[0-9 ۰-۹ A-Zا-ی ء ؤ ئ ةأإآa-z_\s\-\(\)\.]+", ErrorMessage = PublicConst.DangrouseMessageForBadCharachter)]
        public string Title { get; set; }


        [Display(Name = "متن خبر")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        public string Content { get; set; }


        [Display(Name = "چکیده")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [StringLength(400, MinimumLength = 5, ErrorMessage = PublicConst.LengthMessage)]
        [RegularExpression(@"[0-9 ۰-۹ A-Zا-ی ء ؤ ئ ةأإآa-z_\s\-\(\)\.]+", ErrorMessage = PublicConst.DangrouseMessageForBadCharachter)]
        public string Abstract { get; set; }

        [Display(Name = "تعداد بازدید")]
        public int VisitCount { get; set; }


        [Display(Name = "تاریخ خبر")]
        public string NewsDate { get; set; }


        [Display(Name = "زمان خبر")]
        public string NewsTime { get; set; }

        /// <summary>
        /// محل نمایش خبر را مشخص میکند
        /// </summary>
        ///  مقادیرش را میگیرد وکاربر با اتخاب محل مورد نظر خود آیدی محل را درانجا ذخیره میکند asdf این ستون از کلاس ثابت  
        [Display(Name = "محل نمایش خبر")]
        public int NewsPlace { get; set; }

        [Display(Name = "تصویر شاخص")]
        public string IndexImage { get; set; }


        public string UserId { get; set; }

        [Display(Name ="دسته بندی ")]
        public int CategoryId { get; set; }
        #endregion

        #region #################### navigation Properties #################################

        [ForeignKey("UserId")]
        public virtual ApplicationUsers User { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }

        #endregion


    }
}
