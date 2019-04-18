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
    /// جدول نطرات کاربران
    /// </summary>
    public class Comment
    {

        #region ################################## Properties ################################

        /// <summary>
        /// شناسه نظر دهنده
        /// </summary>
        public int Id { get; set; }




        /// <summary>
        /// شنتاسه خبری که درباره ان نطر می دهند
        /// </summary>
        public int NewsId { get; set; }


        /// <summary>
        /// نام نظر دهنده
        /// </summary>

        [Display(Name = "نام")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [StringLength(60, MinimumLength = 3, ErrorMessage = PublicConst.LengthMessage)]
        [RegularExpression(@"[ا-ی\sآ]", ErrorMessage = PublicConst.DangrouseMessageForBadCharachter)]
        public string FullName { get; set; }



        /// <summary>
        /// ایمیل نظر دهنده
        /// </summary>
        [Display(Name = "ایمیل")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [DataType(DataType.EmailAddress, ErrorMessage = "ایمیل را صحیح وارد نمایید")]
        public string Email { get; set; }




        /// <summary>
        /// متن نظرس
        /// </summary>
        [Display(Name = "متن نظر")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [StringLength(2000, MinimumLength = 10, ErrorMessage = PublicConst.LengthMessage)]
        [RegularExpression(@"[0-9A-Zا-یa-z_\s\-\(\)\.]+", ErrorMessage = PublicConst.DangrouseMessageForBadCharachter)]
        public string Message { get; set; }





        /// <summary>
        /// آی پی کاربرنظر دهنده
        /// </summary>
        [Display(Name = "آی پی")]
        public string IP { get; set; }




        /// <summary>
        /// تاریخ ارسال نظر
        /// </summary>
        [Display(Name = "تاریخ ارسال نظر")]
        public string commentDate { get; set; }



        /// <summary>
        /// زمان ارسال نظر
        /// </summary>
        [Display(Name = "زمان ارسال نظر")]
        public string commentTime { get; set; }




        /// <summary>
        /// تعداد لایک
        /// </summary>
        public int LikeCount { get; set; }



        /// <summary>
        /// تعداد دیسلایک
        /// </summary>
        public int DisLikeCount { get; set; }



        /// <summary>
        /// وضعیت انتشار
        /// </summary>
        [Display(Name = "وضعیت انتشار")]
        public bool status { get; set; }



        /// <summary>
        /// شناسه پاسخ
        /// </summary>
        public int ReplyID { get; set; }

       

        #endregion ####################

        #region ################################## Navigation properties ################################

        [ForeignKey("NewsId")]
        public virtual News TblNews { get; set; }

        #endregion####################

    }
}
