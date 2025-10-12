//dateStr
$('.date-input').persianDatepicker({
    format: 'YYYY/MM/DD',
    calendar: {
        persian: {
            leapYearMode: 'astronomical'
        }
    },
    autoClose: true
});
var date = $("#lastDate").val();
if (date !== '' && date !== null && date !== undefined) {
    $('.date-input').val(date);
} else {
    $('.date-input').val('');
}

//toDateStr
$('.to-date-input').persianDatepicker({
    format: 'YYYY/MM/DD',
    calendar: {
        persian: {
            leapYearMode: 'astronomical'
        }
    },
    autoClose: true
});
var date = $("#lastToDate").val();
if (date !== '' && date !== null && date !== undefined) {
    $('.to-date-input').val(date);
} else {
    $('.to-date-input').val('');
}

/////


$('.date-input1').persianDatepicker({
    format: 'YYYY/MM/DD',
    calendar: {
        persian: {
            leapYearMode: 'astronomical'
        }
    },
    autoClose: true
});
var date = $("#lastDate1").val();
if (date !== '' && date !== null && date !== undefined) {
    $('.date-input1').val(date);
} else {
    $('.date-input1').val('');
}

$('.to-date-input1').persianDatepicker({
    format: 'YYYY/MM/DD',
    calendar: {
        persian: {
            leapYearMode: 'astronomical'
        }
    },
    autoClose: true
});

var date = $("#lastToDate1").val();
if (date !== '' && date !== null && date !== undefined) {
    $('.to-date-input1').val(date);
} else {
    $('.to-date-input1').val('');
}

$(document).ready(function () {
    $('.js-example-basic-multiple').select2();
});


//#region User Paging
function FullInfoUserPaging(type = 1, pageActionType = 1, userId = 0, inputPagingId = '', idElementData = '') {
    var inputPaging = document.getElementById(`${inputPagingId}`);
    if (inputPaging === null || inputPaging === undefined) {
        ShowFailedAlert();
        return;
    }
    var pageId = Number(inputPaging.value);
    // pageActionType 1 for upper page
    // pageActionType 2 for downer page
    if (pageActionType === 1) {
        pageId++;
    }
    else if (pageActionType === 2) {
        if (pageId === 1) {
            ShowDynamicWarningAlert('صفحه قبلی موجود نیست !');
            return;
        }
        pageId--;
    }
    else {
        ShowFailedAlert();
    }
    debugger;
    try {
        $.ajax({
            url: `/Admin/User/Index/RequestLoginPaging/${type}/${userId}/${pageId}`,
            type: "get",
            data: {
            },
            beforeSend: function () {
                StartLoading();
            },
            success: function (response) {
                CloseLoading();
                if (response === "" || response === '' || response === null) {
                    ShowDynamicWarningAlert('صفحه بعدی موجود نیست !');
                    return;
                }
                var elementData = document.getElementById(`${idElementData}`);
                debugger;
                if (elementData !== null && elementData !== undefined) {
                    elementData.innerHTML = response;
                    inputPaging.value = pageId;
                }
                else {
                    ShowFailedAlert();
                }
            },
            error: function () {
                CloseLoading();
                ShowFailedAlert();
            }
        });

    } catch (e) {
        console.log(e);
    }
}
//function removeOnclickInPageOne(input = '', previousBtn = '') {
//    var inputPaging = document.getElementById(input);
//    var btnPreviousPage = document.getElementById(previousBtn);
//    if (inputPaging !== undefined && inputPaging !== null) {
//        if (Number(inputPaging.value) === 1) {
//            debugger;
//            btnPreviousPage.removeAttribute('onclick');
//            btnPreviousPage.removeAttribute('href');
//        }
//    }
//}
//#endregion
$("#FormInModal").on("submit", function(event) {
    var validator = $("#FormInModal" ).validate();
    var flag = validator.form();
    if(flag)
    {
        $(this).find(':input[type=submit]').prop('disabled', true);
    }

});

function ShowPassword(elementId) {
    var x = document.getElementById(elementId);
    if (x.type === "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
}

document.addEventListener('DOMContentLoaded', function () {
    const rangeInput1 = document.getElementById('customRange1');
    const rangeValue1 = document.getElementById('rangeValue1');
    rangeInput1.addEventListener('input', function () {
        rangeValue1.textContent = this.value;
    });
    rangeValue1.textContent = rangeInput1.value;

    const rangeInput2 = document.getElementById('customRange2');
    const rangeValue2 = document.getElementById('rangeValue2');
    rangeInput2.addEventListener('input', function () {
        rangeValue2.textContent = this.value;
    });
    rangeValue2.textContent = rangeInput2.value;

    const rangeInput3 = document.getElementById('customRange3');
    const rangeValue3 = document.getElementById('rangeValue3');
    rangeInput3.addEventListener('input', function () {
        rangeValue3.textContent = this.value;
    });
    rangeValue3.textContent = rangeInput3.value;

    const rangeInput4 = document.getElementById('customRange4');
    const rangeValue4 = document.getElementById('rangeValue4');
    rangeInput4.addEventListener('input', function () {
        rangeValue4.textContent = this.value;
    });
    rangeValue4.textContent = rangeInput3.value;
});
function toPersianDigits(number) {
    const persianDigits = ['۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹'];
    return number.toString().replace(/\d/g, d => persianDigits[d]);
}
$(".copy-text").on("click", function () {
    const text = $(this).text();
    navigator.clipboard.writeText(text)
        .then(() => {
            showTemporaryModal("کپی شد!", "success");
        })
        .catch(err => {
            console.error("مشکل در کپی: ", err);
        }
    );
});
