using Chatbot.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chatbot.Repository
{
    public class MessageRepository : Message
    {
        private readonly ApplicationDbContext _ctx;
        private readonly ILogger<MessageRepository> _logger;

        public MessageRepository(ApplicationDbContext ctx, ILogger<MessageRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public async Task<bool> InsertAsync(MessageRepository msg)
        {
            try
            {
                Message message = new Message
                {
                    BotId = msg.BotId,
                    HumanId = msg.HumanId,
                    MessageBody = msg.MessageBody,
                    SentAt = DateTime.Now,
                    SenderIsBot = msg.SenderIsBot,
                };

                _ctx.Messages.Add(message);

                return await _ctx.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogTrace(ex, ex.Message);
                return false;
            }
        }
    }
}
