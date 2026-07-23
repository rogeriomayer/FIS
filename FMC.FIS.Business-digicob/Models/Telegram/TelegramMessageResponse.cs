using System.ComponentModel.DataAnnotations;

namespace FMC.FIS.Business.Models.Telegram
{
    public class TelegramMessageResponse
    {
        //public bool ok { get; set; }
        //public virtual Result result { get; set; }
        public string result { get; set; }
    }
    public class Chat
    {
        [Key]
        public int id { get; set; }
        public string title { get; set; }
        public string type { get; set; }
    }

    public class From
    {
        [Key]
        public long id { get; set; }
        public bool is_bot { get; set; }
        public string first_name { get; set; }
        public string username { get; set; }
    }

    public class Result
    {
        [Key]
        public int message_id { get; set; }
        public virtual From from { get; set; }
        public virtual Chat chat { get; set; }
        public int date { get; set; }
        public string text { get; set; }
    }


}
