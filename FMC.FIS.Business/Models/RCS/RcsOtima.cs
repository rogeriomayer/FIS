namespace FMC.FIS.Business.Models.RCS
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class RcsOtimaResponse
    {
        [JsonProperty("document")]
        public string Document { get; set; }

        [JsonProperty("message_id")]
        public string MessageId { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("status_description")]
        public string StatusDescription { get; set; }

    }
    public class RcsOtimaRequest
    {
        [JsonProperty("broker_code")]
        public string BrokerCode { get; set; }

        [JsonProperty("customer_code")]
        public string CustomerCode { get; set; }

        [JsonProperty("messages")]
        public List<MessageItem> Messages { get; set; }
    }

    public class MessageItem
    {
        [JsonProperty("content")]
        public MessageContent Content { get; set; }

        /*[JsonProperty("date")]
        public Nullable<DateTime> Date { get; set; }*/

        [JsonProperty("document")]
        public string Document { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("fallback")]
        public Fallback Fallback { get; set; }

        [JsonProperty("url_callback_mo")]
        public string UrlCallbackMo { get; set; }

        [JsonProperty("url_callback_status")]
        public string UrlCallbackStatus { get; set; }
    }

    public class MessageContent
    {
        // TEXT
        [JsonProperty("text")]
        public string Text { get; set; }

        // FILE
        [JsonProperty("file_url")]
        public string FileUrl { get; set; }

        // RICHCARD
        [JsonProperty("card_orientation")]
        public string CardOrientation { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("thumbnail_alignment")]
        public string ThumbnailAlignment { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        // CAROUSEL
        [JsonProperty("carousel")]
        public List<CarouselItem> Carousel { get; set; }

        [JsonProperty("suggestions")]
        public List<Suggestions> Suggestions { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class CarouselItem
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("suggestions")]
        public List<Suggestion> Suggestions { get; set; }
    }

    public class Fallback
    {

        [JsonProperty("auth_token")]
        public string AuthToken { get; set; }

        [JsonProperty("broker_code")]
        public string BrokerCode { get; set; }

        [JsonProperty("customer_code")]
        public string CustomerCode { get; set; }

        [JsonProperty("solution")]
        public string Solution { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("url_callback_fallback_mo")]
        public string UrlCallbackFallbackMo { get; set; }

        [JsonProperty("url_callback_fallback_status")]
        public string UrlCallbackFallbackStatus { get; set; }

    }

    public class Suggestions
    {
        [JsonProperty("reply_id")]
        public string ReplyId { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        // OPEN_URL
        [JsonProperty("url")]
        public string Url { get; set; }

        // DIAL
        [JsonProperty("number")]
        public string Number { get; set; }

        // CALENDAR_EVENT
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("start_time")]
        public DateTime? StartTime { get; set; }

        [JsonProperty("end_time")]
        public DateTime? EndTime { get; set; }

        [JsonProperty("original_start_time")]
        public DateTime? OriginalStartTime { get; set; }

        [JsonProperty("original_end_time")]
        public DateTime? OriginalEndTime { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        // VIEW_LOCATION
        [JsonProperty("address")]
        public string Address { get; set; }

        // WEB_PIX
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("qrcode")]
        public string QrCode { get; set; }

        [JsonProperty("telefone")]
        public string Telefone { get; set; }

        // LINHA_DIGITAVEL
        [JsonProperty("linha_digitavel")]
        public string LinhaDigitavel { get; set; }
    }
}
