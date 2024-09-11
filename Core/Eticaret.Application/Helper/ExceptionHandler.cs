using Npgsql;
using System;
using System.Net.Sockets;

namespace EticaretAPI.Application.Helper
{
    public static class ExceptionHandler
    {
        public static void Execute(Action action , string operationName = null)
        {
            try
            {
                action();   
            }
            catch(ArgumentNullException ex)
            {
                Console.WriteLine($"Bir argüman boş olamaz: {nameof(action)} in {operationName ?? "unknown operation"}. Hata: {ex.Message}");
            }
            catch(NpgsqlException ex)
            {
                Console.WriteLine($"Veritabanı bağlantısı sırasında bir hata oluştu: {nameof(action)} in {operationName ?? "unknown operation"}. Hata: {ex.Message}");
            }
            catch(SocketException ex)
            {
                throw new InvalidOperationException("Ağ bağlantısı sırasında bir hata oluştu." , ex);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Beklenmeyen bir hata oluştu: {nameof(action)} in {operationName ?? "unknown operation"}. Hata: {ex.Message}");
            }
        }

        public static T Execute<T>(Func<T> func , string operationName = null)
        {
            try
            {
                return func();  // Gönderilen metodu çalıştır ve geri dönüş değerini al
            }
            catch(ArgumentNullException ex)
            {
                Console.WriteLine($"Bir argüman boş olamaz: {nameof(func)} in {operationName ?? "unknown operation"}. Hata: {ex.Message}");
            }
            catch(NpgsqlException ex)
            {
                Console.WriteLine($"Veritabanı bağlantısı sırasında bir hata oluştu: {nameof(func)} in {operationName ?? "unknown operation"}. Hata: {ex.Message}");
            }
            catch(SocketException ex)
            {
                throw new InvalidOperationException("Ağ bağlantısı sırasında bir hata oluştu." , ex);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Beklenmeyen bir hata oluştu: {nameof(func)} in {operationName ?? "unknown operation"}. Hata: {ex.Message}");
            }

            return default; // Hata durumunda varsayılan bir değer döndür
        }
    }
} 
