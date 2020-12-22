using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Util.Files
{
    #region UploadFileTypeEnum
    public enum UploadFileType
    {
        [Description("Excel")]
        Excel = 1,

        [Description("Images")]
        Images = 3,

        [Description("PDF")]
        PDF = 5
    }
    #endregion


    public class FileHelper
    {
        #region 创建文本文件
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        public static void CreateFile(string path, string content)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8))
            {
                sw.Write(content);
            }
        }
        #endregion

        #region 上传单个文件
        /// <summary>
        /// 上传单个文件
        /// </summary>
        /// <param name="fileModule"></param>
        /// <param name="fileCollection"></param>
        /// <returns></returns>
        public async static Task<TData<string>> UploadFile(int fileModule, string rootPath, IFormFileCollection files)
        {
            TData<string> obj = new TData<string>();

            string dirModule = GetDescriptionByEnum<UploadFileType>(fileModule);

            if (files == null || files.Count == 0)
            {
                obj.Message = "请先选择文件！";
                return obj;
            }
            if (files.Count > 1)
            {
                obj.Message = "一次只能上传一个文件！";
                return obj;
            }

            IFormFile file = files[0];
            string fileExtension = GetCustomValue(Path.GetExtension(file.FileName), ".png");

            string newFileName = GetGuid() + fileExtension;
            string dir = "Resource" + Path.DirectorySeparatorChar + dirModule + Path.DirectorySeparatorChar + DateTime.Now.ToString("yyyy-MM-dd").Replace('-', Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;

            string absoluteDir = Path.Combine(rootPath, dir);
            string absoluteFileName = string.Empty;
            if (!Directory.Exists(absoluteDir))
            {
                Directory.CreateDirectory(absoluteDir);
            }
            absoluteFileName = absoluteDir + newFileName;
            try
            {
                using (FileStream fs = File.Create(absoluteFileName))
                {
                    await file.CopyToAsync(fs);
                    fs.Flush();
                }
                obj.Data = Path.AltDirectorySeparatorChar + ConvertDirectoryToHttp(dir) + newFileName;
                obj.Message = Path.GetFileNameWithoutExtension(GetCustomValue(file.FileName, newFileName));
                obj.Description = (file.Length / 1024).ToString(); // KB
                obj.Tag = 1;
            }
            catch (Exception ex)
            {
                obj.Message = ex.Message;
            }
            return obj;
        }
        #endregion

        #region 下载文件
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="delete"></param>
        /// <returns></returns>
        public static TData<FileContentResult> DownloadFile(string rootPath, string filePath, int delete)
        {
            TData<FileContentResult> obj = new TData<FileContentResult>();
            string absoluteFilePath = rootPath + Path.DirectorySeparatorChar + filePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            byte[] fileBytes = File.ReadAllBytes(absoluteFilePath);
            if (delete == 1)
            {
                File.Delete(absoluteFilePath);
            }
            string fileNamePrefix = DateTime.Now.ToString("yyyyMMddHHmmss");
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
            string title = string.Empty;
            if (fileNameWithoutExtension.Contains("_"))
            {
                title = fileNameWithoutExtension.Split('_')[1].Trim();
            }
            string fileExtensionName = Path.GetExtension(filePath);
            obj.Data = new FileContentResult(fileBytes, "application/octet-stream")
            {
                FileDownloadName = string.Format("{0}_{1}{2}", fileNamePrefix, title, fileExtensionName)
            };
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Method
        public static string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            var contentType = types[ext];
            if (string.IsNullOrEmpty(contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }

        public static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

        public static void CreateDirectory(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public static void DeleteDirectory(string filePath)
        {
            try
            {
                if (Directory.Exists(filePath)) //如果存在这个文件夹删除之 
                {
                    foreach (string d in Directory.GetFileSystemEntries(filePath))
                    {
                        if (File.Exists(d))
                            File.Delete(d); //直接删除其中的文件                        
                        else
                            DeleteDirectory(d); //递归删除子文件夹 
                    }
                    Directory.Delete(filePath, true); //删除已空文件夹                 
                }
            }
            catch
            {

            }
        }

        public static string ConvertDirectoryToHttp(string directory)
        {
            directory = directory.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            return directory;
        }

        public static string ConvertHttpToDirectory(string http)
        {
            http = http.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            return http;
        }

        //public static TData CheckFileExtension(string fileExtension, string allowExtension)
        //{
        //    TData obj = new TData();
        //    string[] allowArr = SplitToArray<string>(allowExtension.ToLower(), '|');
        //    if (allowArr.Where(p => p.Trim() == fileExtension.ToLower()).Any())
        //    {
        //        obj.Tag = 1;
        //    }
        //    else
        //    {
        //        obj.Message = "只有文件扩展名是 " + allowExtension + " 的文件才能上传";
        //    }
        //    return obj;
        //}

        public static string GetCustomValue(string value, string defaultValue)
        {
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }
            else
            {
                return value;
            }
        }

        public static string GetGuid()
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty).ToLower();
        }

        public static string GetDescription(System.Enum enumType)
        {
            FieldInfo EnumInfo = enumType.GetType().GetField(enumType.ToString());
            if (EnumInfo != null)
            {
                DescriptionAttribute[] EnumAttributes = (DescriptionAttribute[])EnumInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (EnumAttributes.Length > 0)
                {
                    return EnumAttributes[0].Description;
                }
            }
            return enumType.ToString();
        }
        #endregion

        #region 根据值获取枚举的描述
        public static string GetDescriptionByEnum<T>(object obj)
        {
            var tEnum = System.Enum.Parse(typeof(T), obj.ParseToString()) as System.Enum;
            var description = GetDescription(tEnum);
            return description;
        }
        #endregion

        //public static T[] SplitToArray<T>(string value, char split)
        //{
        //    T[] arr = value.Split(new string[] { split.ToString() }, StringSplitOptions.RemoveEmptyEntries).CastSuper<T>().ToArray();
        //    return arr;
        //} 

    }
}
