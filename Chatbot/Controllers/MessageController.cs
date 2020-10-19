using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Chatbot.Repository;
using Chatbot.Utilities;
using Chatbot.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account.Manage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Chatbot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessageController : ControllerBase
    {
        private readonly MessageRepository _msg;
        private readonly WordClassificationRepository _wordClassification;
        private readonly BotSpeechRepository _botSpeech;
        private readonly WordCountUseRepository _wordCountUse;
        private readonly ILogger<MessageController> _logger;

        public MessageController(MessageRepository msg, 
            WordClassificationRepository wordClassification,
            BotSpeechRepository botSpeech, 
            WordCountUseRepository wordCountUse,
            ILogger<MessageController> logger)
        {
            _msg = msg;
            _wordClassification = wordClassification;
            _botSpeech = botSpeech;
            _wordCountUse = wordCountUse;
            _logger = logger;
        }
        
        [HttpPost("Sent")]
        public async Task<IActionResult> SaveMessagesWords(MessageViewModel message)
        {
            await _wordCountUse.Insert(new Regex("[^ء-يa-z A-Z0-9]").Replace(message.MessageBody, "").Split(' '));

            return Ok();
        }

        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage(MessageViewModel message)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { message = "" });
            }
            string humanId = User.GetUserId();

            _msg.BotId = BotExtentions.BotId;
            _msg.HumanId = humanId;
            _msg.MessageBody = new Regex("[^ء-يa-z A-Z0-9]").Replace(message.MessageBody, "");
            _msg.SenderIsBot = false;

            await _msg.InsertAsync(_msg);

            var classifications = await _wordClassification.GetClassificationInMessageAsync(_msg.MessageBody);

            string botMsg = await _botSpeech.GenerateBotSpeech(_msg.MessageBody, classifications);

            _msg.MessageBody = botMsg;
            _msg.SenderIsBot = true;

            await _msg.InsertAsync(_msg);

            return Ok(new { message = botMsg });
        }
    }
}
