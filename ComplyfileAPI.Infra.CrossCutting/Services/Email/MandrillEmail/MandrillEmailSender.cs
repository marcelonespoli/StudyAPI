using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using ComplyfileAPI.Infra.CrossCutting.Services.Email.MandrillEmail.Models;
using ComplyfileAPI.Infra.CrossCutting.Services.Email.MandrillEmail.Models.SenderData;
using ComplyfileAPI.Infra.CrossCutting.Services.Interfaces;
using Newtonsoft.Json;

namespace ComplyfileAPI.Infra.CrossCutting.Services.Email.MandrillEmail
{
    public class MandrillEmailSender : IMandrillEmailSender
    {
        private string _key;
        private string _userName;
        private string _pingEndPoint;
        private string _sendEndPoint;
        private string _infoEndPoint;

        public void SetMandrillEmailSender(string userName, string key)
        {
            _userName = userName;
            _key = key;

            _pingEndPoint = ConfigurationManager.AppSettings["Mandrill-PingEndPoint"];
            _sendEndPoint = ConfigurationManager.AppSettings["Mandrill-SendEndPoint"];
            _infoEndPoint = ConfigurationManager.AppSettings["Mandrill-InfoEndPoint"];
        }

        public MandrillMessageInfo CheckStatus(string idMessage)
        {
            MandrillMessageInfo result = new MandrillMessageInfo();
            string jsonString = "{";
            jsonString += "\"key\": \"" + _key + "\",";
            jsonString += "\"id\": \"" + idMessage + "\"";
            jsonString += "}";

            try
            {
                var httpRequest = CreateRequest(_infoEndPoint);
                result = GetResponse<MandrillMessageInfo>(httpRequest, jsonString);

                return result;
            }
            catch (WebException ex)
            {
                var wex = ex.Response;
                using (var streamReader = new StreamReader(wex.GetResponseStream()))
                {
                    string streamResult = streamReader.ReadToEnd();
                    result.messageError = JsonConvert.DeserializeObject<MandrillMessageError>(streamResult);
                    streamReader.Close();
                }
                return result;
            }
        }

        /// <summary>
        /// Send a email message using Madrill Api.
        /// </summary>
        /// <param name="message">A Mandrill message type.</param>
        public MandrillMessageStatus Send(MandrillEmailMessage message)
        {
            if (!TestConnectionWithServer())
            {
                throw new Exception("Without connection with Mandrill Server.");
            }
            try
            {

                MandrillSenderData senderData = new MandrillSenderData();
                senderData.key = _key;
                senderData.message = message;
                senderData.async = false;
                senderData.ip_pool = "Main Pool";
                senderData.send_at = DateTime.Now;
                return Send(senderData);
            }
            catch (Exception ex)
            {
                MandrillMessageStatus messageStatus = new MandrillMessageStatus();
                messageStatus.MessageSent = false;
                messageStatus.MessageError = ex.Message;
                return messageStatus;
            }
        }

        #region Private Methods
        private HttpWebRequest CreateRequest(string client)
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(client);
            httpRequest.ContentType = "application/json";
            httpRequest.Method = "POST";
            var creds = new NetworkCredential(_userName, _key);
            httpRequest.Credentials = creds;
            return httpRequest;
        }

        private T GetResponse<T>(HttpWebRequest httpRequest, string jsonString)
        {
            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(jsonString);
                streamWriter.Flush();
            }

            var response = (HttpWebResponse)httpRequest.GetResponse();

            T apiResponse;
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();
                apiResponse = JsonConvert.DeserializeObject<T>(result);
                streamReader.Close();
            }

            return apiResponse;
        }

        private MandrillMessageStatus Send(MandrillSenderData senderData)
        {
            MandrillMessageStatus ms = new MandrillMessageStatus();
            try
            {
                string jsonString = JsonConvert.SerializeObject(senderData);
                HttpWebRequest httpRequest = CreateRequest(_sendEndPoint);
                ms.messageResult = GetResponse<List<MandrillMessageResult>>(httpRequest, jsonString);

                ms.MessageSent = true;
                return ms;
            }
            catch (WebException ex)
            {
                var wex = ex.Response;
                using (var streamReader = new StreamReader(wex.GetResponseStream()))
                {
                    string result = streamReader.ReadToEnd();
                    ms.messageError = JsonConvert.DeserializeObject<MandrillMessageError>(result);
                    streamReader.Close();
                }

                ms.MessageSent = false;
                ms.MessageError = ex.Message;
                return ms;
            }
            catch (Exception ex)
            {
                ms.MessageSent = false;
                ms.MessageError = ex.Message;
                return ms;
            }
        }

        private bool TestConnectionWithServer()
        {
            string jsonString = "{";
            jsonString += "\"key\": \"" + _key + "\"";
            jsonString += "}";

            try
            {
                var httpRequest = CreateRequest(_pingEndPoint);
                string result = GetResponse<string>(httpRequest, jsonString);

                if (result != "PONG!")
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

    }
}
