using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parser.Common.Logging.Models
{
    public class LogEntry : ILogEntry
    {
        public LogEntry(string message, MessageType messageType)
        {
            this.Message = message;
            this.MessageType = messageType;
        }

        public LogEntry()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Message { get; set; }

        [Required]
        public MessageType MessageType { get; set; }
    }
}
