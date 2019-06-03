using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web90TvCore2.services
{

    /// <summary>
    /// UploadingFileService تعریف  متد ها برای پیاده سازی کلاس 
    /// در کنترلر و پروژه UploadingFileService  اینترفیس برای استفاده از کلاس 
    /// </summary>
    public interface IUploadingFileService
    {
        /// <summary>
        /// آپلودکردن فایل ---
        ///   اگر نیاز به تصویر بند انگشتی نیست
        ///   نال بگذارید (null)
        /// </summary>
        /// <param name="files">فایل های دریافتی برای آپلود از اکشن</param>
        /// <param name="imagePath">مسیر ذخیره تصویرعادی </param>
        /// <param name="thumbnailImagePath">  قرار داده شود  به جای این متغیر null  اگر نیاز به تصویر بند انگشتی نیست </param>
        /// example:(files,  imagePath,  null)
        /// <returns></returns>
        Task<string> UploadFiles(IEnumerable<IFormFile> files, string imagePath, string thumbnailImagePath);

       
    }
}
