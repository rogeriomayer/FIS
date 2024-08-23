using System.Collections.Generic;

namespace FMC.FIS.Business.Models.RCS
{
    public class SendRCSRequest
    {
        public SendRCSRequest()
        {
            messages = new HashSet<Message>();
        }
        public ICollection<Message> messages { get; set; }
    }

    public class ContentRoot
    {
        public string orientation { get; set; }
        public string alignment { get; set; }
        public ContentChild content { get; set; }
        public string type { get; set; }

    }
    public class Options
    {
        public SmsFailover smsFailover { get; set; }
    }

    public class SmsFailover
    {
        public string sender { get; set; }
        public string text { get; set; }
    }

    public class ContentChild
    {
        public string title { get; set; }
        public string description { get; set; }
        public Media media { get; set; }
        public List<Suggestion> suggestions { get; set; }
    }

    public class Destination
    {
        public string to { get; set; }
    }

    public class File
    {
        public string url { get; set; }
    }

    public class Media
    {
        public File file { get; set; }
        public Thumbnail thumbnail { get; set; }
        public string height { get; set; }
    }

    public class Message
    {
        public string sender { get; set; }
        public List<Destination> destinations { get; set; }
        public ContentRoot content { get; set; }

        public Options options { get; set; }
    }


    public class Suggestion
    {
        public string text { get; set; }
        public string postbackData { get; set; }
        public string url { get; set; }
        public string type { get; set; }
    }

    public class Thumbnail
    {
        public string url { get; set; }
    }





    //Response

    public class MessageResponse
    {
        public string messageId { get; set; }
        public Status status { get; set; }
        public string destination { get; set; }
    }

    public class SendRCSResponse
    {
        public SendRCSResponse()
        {
            messages = new HashSet<MessageResponse>();
        }
        public string bulkId { get; set; }
        public ICollection<MessageResponse> messages { get; set; }
    }

    public class Status
    {
        public int groupId { get; set; }
        public string groupName { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}
