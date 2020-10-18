
function getTime(date) {
    var hours = date.getHours();
    var minutes = date.getMinutes();

    hours = hours < 10 ? '0' + hours : hours;
    minutes = minutes < 10 ? '0' + minutes : minutes;
    var strTime = hours + ':' + minutes;
    return strTime;
}

function insertChat(who, text) {
    
    let control = "";
    let date = getTime(new Date());

    if (who === 'bot') {

        control = '<div class="incoming_msg">' +
                    '<div class="incoming_msg_img"> <img src="https://cdn2.iconfinder.com/data/icons/chatbot-line/100/chatbot-07-512.png" alt="sunil"> </div>' +
                        '<div class="received_msg">' +
                            '<div class="received_withd_msg">' +
                                '<p style="white-space: pre-line">' + text + '</p>' +
                                '<span class="time_date">' + date + '</span>' +
                            '</div>' +
                        '</div>' +
                    '</div>';


    } else {
        control = '<div class="outgoing_msg">' +
                    '<div class="sent_msg">' +
                        '<p style="white-space: pre-line">' + text + '</p>' +
                        '<span class="time_date">' + date + '</span>' +
                    '</div>' +
                '</div>';
    }

    $('#messagesDiv').append(control);

    $("#messagesDiv").scrollTop($("#messagesDiv")[0].scrollHeight);
}


$('#sendMessageForm').on('submit', function (e) {
    e.preventDefault();

    let message = $('#messageBody').val();

    insertChat('human', message)

    $('#messageBody').val('')

    $.ajax({
        url: '/api/Message/SendMessage',
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ MessageBody: message }),
        success: function (e) {
            insertChat('bot', e.message)
        },
        error: function (e) {
            console.log("ops")
        }
    })

    $.ajax({
        url: '/api/Message/Sent',
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ MessageBody: message }),
    })
})