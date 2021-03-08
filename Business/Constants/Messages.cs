using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
   public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string MaintenanceTime = "Sistem bakımda";
        public static string ProductsListed = "Ürünler listelendi";

        public static string ProductCountOfCatogoryError = "Ürün kategorisi hatası";
        public static string ProductNameAlreadyExist="Bu isimde bir isim zaten var";
        public static string CategoryLimitExceded="Kategori limiti aşıldığı için yeni ürün eklenemiyor";
        public static string AuthorizationDenied = "Yetkiniz yok";
        public static string  UserNotFound;
        public static string  PasswordError="Parola hatali";

        public static string SuccessfullLogin = "Giriş başarılı";
        public static string UserAlreadyExists = "kurstan çoktan çıktınız";
        public static string AccessTokenCreated = "işlem başarılı";

        public static string UserRegistered = "kayıt başarılı";
    }
}
