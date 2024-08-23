using FMC.FIS.BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TgSharp.Core;
using TgSharp.Core.Utils;
using TgSharp.TL;
using TgSharp.TL.Contacts;

namespace FMC.FIS.Business.Code.Api.Telegram
{
    public class TelegramAPI
    {
        TelegramClient Client;
        public TelegramAPI()
        {
            var guid = new Guid().ToString();
            Client = new TelegramClient(Constants.TelegramApiId, Constants.TelegramApiHash, sessionUserId: guid);
            Client.ConnectAsync().GetAwaiter().GetResult();

            string hash = "";

            if (!Client.IsUserAuthorized())
            {
                hash = Client.SendCodeRequestAsync("+55" + Constants.TelegramPhoneNumber).GetAwaiter().GetResult();

                Client.MakeAuthAsync("+55" + Constants.TelegramPhoneNumber, hash, Constants.TelegramCodeAuth).GetAwaiter().GetResult();
            }
        }

        public async Task<TLUser> GetUser(string phoneNumber, string firstName, string lastName)
        {
            try
            {
                /*TLRequestImportContacts requestImportContacts = new TLRequestImportContacts();
                requestImportContacts.Contacts = new TLVector<TLInputPhoneContact>();
                requestImportContacts.Contacts.Add(new TLInputPhoneContact()
                {
                    ClientId = 0,
                    Phone = "55" + phoneNumber,
                    FirstName = firstName,
                    LastName = lastName
                });
                */
                var phoneContact = new TLInputPhoneContact() { ClientId = new Random().Next(-208488460, 208488460), Phone = "+55" + phoneNumber, FirstName = firstName, LastName = lastName };

                var tlVector = new TLVector<TLInputPhoneContact>();
                tlVector.Add(phoneContact);

                var requestImportContacts = new TgSharp.TL.Contacts.TLRequestImportContacts() { Contacts = tlVector };

                var user = await Client.SendRequestAsync<TgSharp.TL.Contacts.TLImportedContacts>((TLMethod)requestImportContacts);
                if (user != null)
                    if (user.Users != null && user.Users.Count > 0)
                        return (user.Users.First() as TLUser);
                    else
                        return null;
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> SendTextMessage(long chatId, string message)
        {
            var d = await Client.SendMessageAsync(new TLInputPeerUser() { UserId = chatId }, message);
            return d;
        }

        public async Task<object> SendChatMessage(long chatId, string message)
        {
            var d = await Client.SendMessageAsync(new TLInputPeerChat() { ChatId = chatId }, message);
            return d;
        }

        public async Task<Object> GetChat(string chatName)
        {
            var dialogs = Client.GetUserDialogsAsync().GetAwaiter().GetResult();
            if (dialogs != null)
            {
                var chats = ((TgSharp.TL.Messages.TLDialogs)dialogs).Chats.ToList();
                return chats.FirstOrDefault();
            }

            return null;
        }

        public static async Task SendTextMessage(string botId, string chatId, string message)
        {


            var uri = "https://api.telegram.org/bot" + botId + "/sendMessage";
            var builder = new UriBuilder(uri);
            builder.Port = -1;
            var query = HttpUtility.ParseQueryString(builder.Query);

            query["chat_id"] = chatId;
            query["text"] = message;

            builder.Query = query.ToString();
            string url = builder.ToString();

            var result = new SendTelegramBLL().SingleMessage(url);

            if (result != null && result.result.Contains("\"ok\":true"))
            {
                return;
            }
            else
                throw new Exception("result.result");

        }

        public async Task SendFile(long chatId, StreamReader file, string fileType, string fileName, string message)
        {

            TLAbsInputFile inputFile = await Client.UploadFile(fileName, file);

            var attr = new TLVector<TLAbsDocumentAttribute>() { new TLDocumentAttributeFilename { FileName = fileName }, };

            await Client.SendUploadedDocument(new TLInputPeerUser() { UserId = chatId }, inputFile, "application/" + fileType, message, attr);

        }

        public async Task SendPhoto(long chatId, StreamReader file, string fileName, string message)
        {
            TLAbsInputFile inputFile = await Client.UploadFile(fileName, file);
            await Client.SendUploadedPhoto(new TLInputPeerUser() { UserId = chatId }, inputFile, message);
        }
    }

}
