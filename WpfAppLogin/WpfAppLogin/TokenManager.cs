using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Windows;

namespace WpfAppLogin
{
    class TokenManager
    {
        public static string AccessToken { get; set; }
        public static string UrlHost { get; set; }
        public static int IdClient { get; set; }

        public class AuthResponse
        {
            public int IdClient { get; set; }
            public string AccessToken { get; set; }
            public string TokenType { get; set; }
        }

        public static string GetAccessToken(String username, String password)
        {

            var loginRequest = new
            {
                username = username,
                password = password
            };

            string loginUrl = "/api/auth/signin";

            using (var client = new HttpClient())
            {
                try
                {
                    var content = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");
                    var response = client.PostAsync(UrlHost + loginUrl, content).Result;

                    // MessageBox.Show(response.ToString());

                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = response.Content.ReadAsStringAsync().Result;
                        var authResponse = JsonConvert.DeserializeObject<AuthResponse>(responseBody);

                        IdClient = authResponse.IdClient + 1;
                        return authResponse.AccessToken;
                    }
                    else if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        Console.WriteLine("Bad credentials: Unauthorized");
                        MessageBox.Show("Error: Contraseña o Usuario incorrectos");
                        return null;
                    }
                    else
                    {
                        Console.WriteLine("Error en el inicio de sesión");
                        MessageBox.Show("Error en el inicio de sesión");
                        return null;
                    }
                }
                catch (AggregateException e)
                {
                    string mensaje = "Error al conectar con el servidor";
                    string titulo = "Error" + e;
                    MessageBox.Show(mensaje, titulo, MessageBoxButton.OK, MessageBoxImage.Information);
                    return null;
                }
            }
        }

        public static string GetItems(String url)
        {

            var request = (HttpWebRequest)WebRequest.Create(UrlHost + url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Headers.Add("Authorization", "Bearer " + TokenManager.AccessToken);
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) ;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            //MessageBox.Show(responseBody);
                            return responseBody;
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine("Falla" + ex);
                string mensaje = "Error al cargar la información";
                string titulo = "Error" + ex;
                MessageBox.Show(mensaje, titulo, MessageBoxButton.OK, MessageBoxImage.Information);
                return null;
            }
        }

        public static void PostItems(string url, string cadenajson)
        {
            var request = (HttpWebRequest)WebRequest.Create(UrlHost + url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Headers.Add("Authorization", "Bearer " + TokenManager.AccessToken);

            try
            {
                using (Stream requestStream = request.GetRequestStream())
                {
                    byte[] data = Encoding.UTF8.GetBytes(cadenajson);
                    requestStream.Write(data, 0, data.Length);
                }

                using (WebResponse response = request.GetResponse())
                {
                    string mensaje = "Añadido con éxito";
                    string titulo = "Exito";
                    MessageBox.Show(mensaje, titulo, MessageBoxButton.OK, MessageBoxImage.Information);
                    Console.WriteLine("añadido con éxito.");
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine("Falla: " + ex.Message);
            }
        }

        public static void PutItems(string url, string cadenajson, int id)
        {
            var re = UrlHost + url + id;
            MessageBox.Show(re);
            var request = (HttpWebRequest)WebRequest.Create(UrlHost + url + id);
            request.Method = "PUT";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Headers.Add("Authorization", "Bearer " + TokenManager.AccessToken);

            try
            {
                using (Stream requestStream = request.GetRequestStream())
                {
                    byte[] data = Encoding.UTF8.GetBytes(cadenajson);
                    requestStream.Write(data, 0, data.Length);
                }

                using (WebResponse response = request.GetResponse())
                {
                    string mensaje = "Actualizado con éxito";
                    string titulo = "Exito";
                    MessageBox.Show(mensaje, titulo, MessageBoxButton.OK, MessageBoxImage.Information);
                    Console.WriteLine("Incidencia actualizada con éxito.");
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine("Falla: " + ex.Message);
            }
        }

        public static async Task DeleteItems(int id, string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(UrlHost + url + id);
            request.Method = "DELETE";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Headers.Add("Authorization", "Bearer " + TokenManager.AccessToken);

            try
            {
                using (WebResponse response = await request.GetResponseAsync())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = await objReader.ReadToEndAsync();
                            string mensaje = "Eliminado con éxito";
                            string titulo = "Éxito";
                            MessageBox.Show(mensaje, titulo, MessageBoxButton.OK, MessageBoxImage.Information);
                            Console.WriteLine(responseBody);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine("Falla: " + ex.Message);
            }
        }

    }
}
