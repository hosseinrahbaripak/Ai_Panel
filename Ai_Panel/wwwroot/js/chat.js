function SendFormDataMessage(formId = '', url = '') {
    var form = $(`form#${formId}`);
    form.off('submit');
    form.submit(function (e) {
        e.preventDefault();
        var formElement = form[0];
        var formData = new FormData(formElement);
        var token = document.getElementById('Token').value;
        var partId = Number(document.getElementById('PartId')?.value ?? 0);
        var message = document.getElementById('Message')?.value ?? "";
        if (message === undefined || message === null || message === "") {
            ShowDynamicFailedAlert('لطفا متن پیام خود را وارد کنید !');
            return;
        }
        var sendBtn = document.getElementById('SendMessageBtn');
        if (!sendBtn) {
            return;
        }
        sendBtn.classList.add('disabled');
        sendBtn.innerHTML = "<div class='spinner-border' role='status'><span class='visually-hidden' > Loading...</span > </div >";
        formData.append('token', token);
        formData.append('partId', partId);
        formData.append('message', message);
        $.ajax({
            url: `${url}`,
            type: 'POST',
            headers: {
                'Token': token // Set your custom header here
            },
            cache: false,
            contentType: false,
            processData: false,
            data: formData,
            beforeSend: function () {
            },
            success: function (response) {
                if (response.errorId === 0) {
                    document.getElementById('Message').value = "";
                    var res = response.result;
                    createMessage(res.id, message, res.aiResponse, res.datePersian, res.userName);
                }
                else {
                    ShowDynamicFailedAlert(response.ErrorTitle);
                }
                sendBtn.innerHTML = "<i class='fa fa-send'></i>";
                sendBtn.classList.remove('disabled');
            }
        });
    });
}
function SendFormDataMessageInPanel(formId = '', url = '') {
    var form = $(`form#${formId}`);
    form.off('submit');
    form.submit(function (e) {
        e.preventDefault();
        var formElement = form[0];
        const formData = new FormData(formElement);
        const bookId = Number(document.getElementById('bookSelectBox')?.value);
        const partId = Number(document.getElementById('bookPartsSelectBox')?.value);
        const message = document.getElementById('Message')?.value ?? "";
        const firstMessage = document.getElementById('FirstMessage')?.value ?? "";
        const maxTokens = Number(document.getElementById('AiConfigDto_MaxTokens')?.value);
        const selectAiPlatformElement = document.getElementById("AiPlatform");
        const aiPlatformId = selectAiPlatformElement.value;
        const selectElement = document.getElementById("AiType");
        const ai = selectElement.options[selectElement.selectedIndex].text;
        const aiId = selectElement.value;
        const selectAiModelElement = document.getElementById("AiModelTypes");
        const aiModel = selectAiModelElement.options[selectAiModelElement.selectedIndex].text;
        const aiModelId = selectAiModelElement.value;
        const bookContent = document.getElementById('BookContent')?.value ?? "";
        const prompt = document.getElementById('Prompt')?.value ?? "";
        const Temperature = Number(document.getElementById('customRange2')?.value ?? 0);
        const TopP = Number(document.getElementById('customRange4')?.value ?? 0);
        const PresencePenalty = Number(document.getElementById('customRange3')?.value ?? 0);
        const FrequencyPenalty = Number(document.getElementById('customRange1')?.value ?? 0);
        const stop = document.getElementById('stop')?.value;
        if (message === undefined || message === null || message === "") {
            ShowDynamicFailedAlert('لطفا متن پیام خود را وارد کنید !');
            return;
        }
        //if (!bookId || !partId || StringIsNullOrEmpty(ai) || StringIsNullOrEmpty(aiModel) || !maxTokens || StringIsNullOrEmpty(bookContent) || StringIsNullOrEmpty(prompt)) {
        if (StringIsNullOrEmpty(ai) || StringIsNullOrEmpty(aiModel) || !maxTokens || StringIsNullOrEmpty(prompt) || StringIsNullOrEmpty(firstMessage)) {
            ShowDynamicFailedAlert('لطفاً مقادیر "مدل هوش مصنوعی"، "حداکثر تعداد کلمات خروجی"، "پرامپت" و "پیام آغازین" را تکمیل کنید !');
            return;
        }
        var sendBtn = document.getElementById('SendMessageBtn');
        if (!sendBtn) {
            return;
        }
        sendBtn.classList.add('disabled');
        sendBtn.innerHTML = "<div class='spinner-border' role='status'><span class='visually-hidden' > Loading...</span > </div >";
        formData.append('BookId', bookId?.toString());
        formData.append('PartId', partId?.toString());
        formData.append('AiPlatformId', aiPlatformId);
        formData.append('AiStr', ai);
        formData.append('AiId', aiId);
        formData.append('AiModelId', aiModelId);
        formData.append('AiModelStr', aiModel);
        formData.append('MaxTokens', maxTokens);
        formData.append('BookContent', bookContent);
        formData.append('Prompt', prompt);
        formData.append('FirstMessage', firstMessage);
        formData.append('TopP', TopP);
        formData.append('PresencePenalty', PresencePenalty);
        formData.append('FrequencyPenalty', FrequencyPenalty);
        formData.append('Temperature', Temperature);
        formData.append('StopStr', stop);
        $.ajax({
            url: `${url}`,
            type: 'POST',
            cache: false,
            contentType: false,
            processData: false,
            data: formData,
            beforeSend: function () {
            },
            success: function (response) {
                if (response.errorId === 0) {
                    document.getElementById('Message').value = "";
                    var res = response.result;
                    createMessage(res.id, message, res.aiResponse, res.datePersian, res.userName);
                }
                else {
                    ShowDynamicFailedAlert(response.errorTitle);
                }
                sendBtn.innerHTML = "<i class='fa fa-send'></i>";
                sendBtn.classList.remove('disabled');
            },
            error: function () {
                sendBtn.innerHTML = "<i class='fa fa-send'></i>";
                sendBtn.classList.remove('disabled');
                ShowFailedAlert();
            },
        });
    });
}
function createMessage(id, userMessage, aiResponse, date, username) {
    var container = document.createElement('div');
    container.className = 'col-12';
    container.id = 'message_' + id; // Sample ID, adjust as needed

    // Create the first direct-chat-msg
    var directChatMsg1 = document.createElement('div');
    directChatMsg1.className = 'direct-chat-msg mb-3 right';

    var directChatInfos1 = document.createElement('div');
    directChatInfos1.className = 'direct-chat-infos clearfix';

    var directChatName1 = document.createElement('span');
    directChatName1.className = 'direct-chat-name float-right';
    directChatName1.innerText = username;

    var directChatTimestamp1 = document.createElement('span');
    directChatTimestamp1.className = 'direct-chat-timestamp float-right me-3';
    directChatTimestamp1.setAttribute('dir', 'ltr');
    directChatTimestamp1.innerText = date;

    // var directChatImg1 = document.createElement('img');
    // directChatImg1.className = 'direct-chat-img';
    // directChatImg1.src = '/images/user-avatar.jpg';
    // directChatImg1.alt = '.';

    var directChatText1 = document.createElement('div');
    directChatText1.className = 'direct-chat-text';

    var p1 = document.createElement('p');
    p1.innerText = userMessage;

    directChatText1.appendChild(p1);
    directChatInfos1.appendChild(directChatName1);
    directChatInfos1.appendChild(directChatTimestamp1);
    // directChatMsg1.appendChild(directChatImg1);
    directChatMsg1.appendChild(directChatInfos1);
    directChatMsg1.appendChild(directChatText1);

    // Create the second direct-chat-msg
    var directChatMsg2 = document.createElement('div');
    directChatMsg2.className = 'direct-chat-msg mb-3 float-end';

    var directChatInfos2 = document.createElement('div');
    directChatInfos2.className = 'direct-chat-infos clearfix';

    var directChatName2 = document.createElement('span');
    directChatName2.className = 'direct-chat-name float-left';
    directChatName2.innerText = 'دستیار آموزشی';

    var directChatImg2 = document.createElement('img');
    directChatImg2.className = 'direct-chat-img';
    directChatImg2.src = '/images/1cfc25cf-41bf-41cb-916b-f2d889933703_page-0001.png';
    directChatImg2.alt = '.';

    var directChatText2 = document.createElement('div');
    directChatText2.className = 'direct-chat-text';

    var p2 = document.createElement('p');
    p2.id = "aiResponse_" + id;
    const words = aiResponse?.split(" ") ?? "";
    let index = 0;
    const intervalId = setInterval(() => {
        if (index < words.length) {
            if (index === 0) {

                p2.innerText = words[index];
                directChatText2.appendChild(p2);
                directChatInfos2.appendChild(directChatName2);
                directChatMsg2.appendChild(directChatImg2);
                directChatMsg2.appendChild(directChatInfos2);
                directChatMsg2.appendChild(directChatText2);

                // Append both direct-chat-msgs to the container
                container.appendChild(directChatMsg1);
                container.appendChild(directChatMsg2);

                // Append the container to the body or any other desired parent element
                document.getElementById('chatbody').appendChild(container);

                ScrollToSection(container.id);
            }
            else {
                var aiResponseP = document.getElementById(`aiResponse_${id}`);
                var lastValue = aiResponseP.innerText;
                document.getElementById(`aiResponse_${id}`).innerText = lastValue + " " + words[index];
            }
            index++;
        }
        else {
            clearInterval(intervalId);
        }
    }, 200);


}
function ScrollToSection(id) {
    var element = document.getElementById(`${id}`);
    if (element !== undefined && element !== null) {
        element.scrollIntoView();
    }
}
function chatBookSelectTrigger() {
    const $chatSelect = $('#chatBookSelect');
    const $chatSelectIcon = $('#chatBookSelectTriggerIcon');
    if ($chatSelect.is(":visible")) {
        $chatSelect.slideUp(300);
        $chatSelectIcon.removeClass("fa-angle-up");
        $chatSelectIcon.addClass("fa-angle-down");
    } else {
        $chatSelect.slideDown(300);
        $chatSelectIcon.removeClass("fa-angle-down");
        $chatSelectIcon.addClass("fa-angle-up");
    }
}
function resetTestDateTimeBegging() {
    const now = new Date();
    const tehranTime = new Date(now.getTime() + (3.5 * 60 * 60 * 1000));
    const tehranTimeIso = tehranTime.toISOString();
    $("#TestDateTimeBegging").val(tehranTimeIso);
}
function aiChengedAlert() {
    if (!$("#chat-overlay").is(":visible")) {
        ShowDynamicWarningAlert('با تغییر مدل هوش مصنوعی، چت تستی از سر گرفته خواهد شد!');
        partChange()
    }
    resetTestDateTimeBegging();
}