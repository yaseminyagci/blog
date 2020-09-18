using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Extensions;

namespace Core.Utilities
{
    public static class MessageBuilder
    {
        public static string Login = "Başarıyla Giriş Yapıldı";
        public static string Logout = "Başarıyla Çıkış Yapıldı";
        public static string NotFound = "Bulunamadı";
        public static string LoginFault = "Kullanıcı Adı/Email veya Parola yanlış";
        public static string Success = "Başarılı";
        public static string NotEditable = "Bu kayıtın bilgilerini değiştiremezsin!";
        public static string MacOrIpIsExist = "Bu Mac veya Ip adresi zaten mevcut!";

        public static string Added(string key = "")
        {
            var msg = "Başarıyla Eklendi";
            if (string.IsNullOrEmpty(key))
                return msg.FirstLetterToUpper();
            return $"{key} {msg}";
        }

        public static string Deleted(string key = "")
        {
            var msg = "Başarıyla Silindi";
            if (string.IsNullOrEmpty(key))
                return msg.FirstLetterToUpper();
            return $"{key} {msg}";
        }

        public static string Edited(string key = "")
        {
            var msg = "Başarıyla Güncellendi";
            if (string.IsNullOrEmpty(key))
                return msg.FirstLetterToUpper();
            return $"{key} {msg}";
        }
        public static string AlreadyExist(string key = "")
        {
            var msg = "Zaten Mevcut!";
            if (string.IsNullOrEmpty(key))
                return msg.FirstLetterToUpper();
            return $"{key} {msg}";
        }
        public static string RelatedDataExist(params string[] keys)
        {
            var keysString = String.Join(",", keys);
            var msg = $"Bu kayıta bağlı veriler({keysString}) bulunmaktadır . Ilkönce onları siliniz!";

            return $"{msg}";
        }


    }
}
