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



        /// <summary>
        /// تعداد بازدید خبر
        /// </summary>
        [Display(Name = "تعداد بازدید")]
        public int VisitCount { get; set; }


        /// <summary>
        /// تاریخ ثبت خبر
        /// </summary>
        [Display(Name = "تاریخ خبر")]
        public string NewsDate { get; set; }


        /// <summary>
        /// زمان ثبت خبر
        /// </summary>
        [Display(Name = "زمان خبر")]
        public string NewsTime { get; set; }



        /// <summary>
        /// محل نمایش خبر را مشخص میکند
        /// </summary>
        ///  مقادیرش را میگیرد وکاربر با اتخاب محل مورد نظر خود آیدی محل را درانجا ذخیره میکند asdf این ستون از کلاس ثابت  
        [Display(Name = "محل نمایش خبر")]
        public byte NewsPlace { get; set; }


        /// <summary>
        /// نوع خبر
        /// </summary>
        /// نوع های داخلی=0 خارجی=1 و اختصاصی=2 را میگیرد برای نمایش در تب های مربوطه در صفحه اصلی
        [Display(Name = "نوع خبر")]
        public byte NewsType { get; set; }


        /// <summary>
        /// تصویر کوچکی که کنار عنوان خبر نشان میدهیم
        /// </summary>
        [Display(Name = "تصویر شاخص")]
        public string IndexImage { get; set; }


        /// <summary>
        /// آیدی کاربری که خبر را ثبت کرده است 
        /// </summary>
        /// بااین ویژگی کاربری که خبر را ایجاد کرده را مشخص میگنیم
        public string UserId { get; set; }


        /// <summary>
        /// دسته بندی خبر
        /// </summary>
        /// خبر مربوط به چه دسته بندی است
        [Display(Name ="دسته بندی ")]
        public int CategoryId { get; set; }



        /// <summary>
        /// متاتگ - Seo
        /// </summary>
        /// مرتبط با سیو
        [Display(Name ="متاتگ")]
        public string  MetaTag { get; set; }



        /// <summary>
        /// متای توضیحات - Seo
        /// </summary>
        /// مرتبط با سیو
        [Display(Name = "متای توضیحات")]
        public string MetaDescription { get; set; }






        #endregion


        #region #################### navigation Properties #################################

        [ForeignKey("UserId")]
        public virtual ApplicationUsers User { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }

        #endregion


    }
}
