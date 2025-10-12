var
    persianNumbers = [/۰/g, /۱/g, /۲/g, /۳/g, /۴/g, /۵/g, /۶/g, /۷/g, /۸/g, /۹/g],
    arabicNumbers = [/٠/g, /١/g, /٢/g, /٣/g, /٤/g, /٥/g, /٦/g, /٧/g, /٨/g, /٩/g],
    fixNumbers = function (str) {
        if (typeof str === 'string') {
            for (var i = 0; i < 10; i++) {
                str = str.replace(persianNumbers[i], i).replace(arabicNumbers[i], i);
            }
        }
        return str;
    };

JalaliDate = {
    g_days_in_month: [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31],
    j_days_in_month: [31, 31, 31, 31, 31, 31, 30, 30, 30, 30, 30, 29]
};

JalaliDate.jalaliToGregorian = function (j_y, j_m, j_d) {
    j_y = parseInt(j_y);
    j_m = parseInt(j_m);
    j_d = parseInt(j_d);
    var jy = j_y - 979;
    var jm = j_m - 1;
    var jd = j_d - 1;

    var j_day_no = 365 * jy + parseInt(jy / 33) * 8 + parseInt((jy % 33 + 3) / 4);
    for (var i = 0; i < jm; ++i) j_day_no += JalaliDate.j_days_in_month[i];

    j_day_no += jd;

    var g_day_no = j_day_no + 79;

    var gy = 1600 + 400 * parseInt(g_day_no / 146097); /* 146097 = 365*400 + 400/4 - 400/100 + 400/400 */
    g_day_no = g_day_no % 146097;

    var leap = true;
    if (g_day_no >= 36525) /* 36525 = 365*100 + 100/4 */ {
        g_day_no--;
        gy += 100 * parseInt(g_day_no / 36524); /* 36524 = 365*100 + 100/4 - 100/100 */
        g_day_no = g_day_no % 36524;

        if (g_day_no >= 365) g_day_no++;
        else leap = false;
    }

    gy += 4 * parseInt(g_day_no / 1461); /* 1461 = 365*4 + 4/4 */
    g_day_no %= 1461;

    if (g_day_no >= 366) {
        leap = false;

        g_day_no--;
        gy += parseInt(g_day_no / 365);
        g_day_no = g_day_no % 365;
    }

    for (var i = 0; g_day_no >= JalaliDate.g_days_in_month[i] + (i == 1 && leap); i++)
        g_day_no -= JalaliDate.g_days_in_month[i] + (i == 1 && leap);
    var gm = i + 1;
    var gd = g_day_no + 1;

    gm = gm < 10 ? "0" + gm : gm;
    gd = gd < 10 ? "0" + gd : gd;

    return [gy, gm, gd];
}


function CheckMobileFormat(that, id) {
    debugger;
    var num = that.value;

    if (!StringIsNullOrEmpty(num) && (num.charAt(0) !== "0" || num.charAt(1) !== "9")) {
        $("#js-error").html("فرمت شماره موبایل وارد شده اشتباه است.");
        $("#" + id).css("border-color", "red");
        $("#submit-btn").prop("disabled", true);
    } else {
        $("#js-error").html("");
        $("#" + id).css("border-color", "#ced4da");
        $("#submit-btn").prop("disabled", false);
    }
}
function CheckNationalCodeInput(that, id) {
    debugger;
    var num = that.value;
    if (!StringIsNullOrEmpty(num) && CheckNationalCode(num) === false) {
        $("#js-error").html("فرمت کدملی وارد شده اشتباه است.");
        $("#" + id).css("border-color", "red");
        $("#submit-btn").prop("disabled", true);
    } else {
        $("#js-error").html("");
        $("#" + id).css("border-color", "#ced4da");
        $("#submit-btn").prop("disabled", false);
    }
}

function CheckNationalCode(nationalCode) {

    if (nationalCode == null) return false;
    if (nationalCode == '') return false;
    if (nationalCode.length != 10) return false;
    if (isNaN(Number(nationalCode))) return false;

    var allDigitEqual = new Array("0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666", "7777777777", "8888888888", "9999999999");
    if (allDigitEqual.indexOf(nationalCode) != -1) return false;

    var chArray = nationalCode.split('');
    var num0 = Number(chArray[0].toString()) * 10;
    var num2 = Number(chArray[1].toString()) * 9;
    var num3 = Number(chArray[2].toString()) * 8;
    var num4 = Number(chArray[3].toString()) * 7;
    var num5 = Number(chArray[4].toString()) * 6;
    var num6 = Number(chArray[5].toString()) * 5;
    var num7 = Number(chArray[6].toString()) * 4;
    var num8 = Number(chArray[7].toString()) * 3;
    var num9 = Number(chArray[8].toString()) * 2;
    var a = Number(chArray[9].toString());

    var b = (((((((num0 + num2) + num3) + num4) + num5) + num6) + num7) + num8) + num9;
    var c = b % 11;

    return (((c < 2) && (a == c)) || ((c >= 2) && ((11 - c) == a)));
}

function formatMoney(number) {

    var length = number.toString().length;
    if (length > 3) {
        var strNumber = number.toString().split(".");
        var currency = "";
        var strArray = [];
        for (var i = strNumber[0].length - 1; i >= 0; i = i - 3) {
            strArray.push(strNumber[0].substring(i + 1, i - 2));
        }
        currency = strArray.reverse().join();
        if (strNumber.length > 1 && strNumber[1] !== "") {
            strNumber[1] = strNumber[1].substring(0, 2);
            currency = currency + "." + strNumber[1];
        }
        return currency;
    }
    return number;
}

$("#IconName").on('change', function () {

    if (typeof (FileReader) != "undefined") {

        var image_holder = $("#image-holder");
        image_holder.empty();
        $("#oldImg").hide();
        var reader = new FileReader();
        reader.onload = function (e) {
            $("<img />", {
                "src": e.target.result,
                "class": "img-thumbnail",
                "style": "max-width: 200px"
            }).appendTo(image_holder);

        }
        image_holder.show();
        reader.readAsDataURL($(this)[0].files[0]);
    } else {
        alert("This browser does not support FileReader.");
    }
});
$("#fileUpload").on('change', function () {

    if (typeof (FileReader) != "undefined") {

        $("#oldImg").hide();
        var image_holder = $("#image-holder");
        image_holder.empty();

        var reader = new FileReader();
        reader.onload = function (e) {
            $("<img />", {
                "src": e.target.result,
                "class": "img-thumbnail",
                "style": "max-width: 200px"
            }).appendTo(image_holder);

        }
        image_holder.show();
        reader.readAsDataURL($(this)[0].files[0]);
    } else {
        alert("This browser does not support FileReader.");
    }
});
function SetCheckBox(type) {

    if ($("#" + type).prop('checked') === true) {
        $("#" + type).val(true);
    }
    else {
        $("#" + type).val(false);
    }
}
$(document).ready(function () {

    var newversion = "Epsilon10";
    var currentversion = readCookie("VersionOfView");
    if (currentversion == null) {
        createCookie("VersionOfView", newversion);
    } else {
        if (currentversion !== newversion) {
            eraseCookie("VersionOfView");
            createCookie("VersionOfView", newversion);
            location.reload(true);
        }
    }

    ErrorHandle();
});
function createCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    } else var expires = "";

    document.cookie = name + "=" + value + expires + "; path=/";
}

function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(";");
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == " ") c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function eraseCookie(name) {
    createCookie(name, "", -1);
}

function OpenModal(url = '', label = '') {
    try {
        $.ajax({
            url: url,
            type: "get",
            data: {},
            beforeSend: function () {
                StartLoading();
            },
            success: function (response) {
                CloseLoading();
                $('#exampleModal').modal('show');
                $('#exampleModalLabel').html(label);
                $('#modalbody').html(response);

                $('#FormInModal').data('validator', null);
                $.validator.unobtrusive.parse('#FormInModal');
            },
            error: function () {
                CloseLoading();
                ShowFailedAlert();
            }
        });
    } catch (e) {
        console.log(e.message);
    }

}
function OpenModalLg(url = '', label = '') {
    $.ajax({
        url: url,
        type: "get",
        data: {},
        beforeSend: function () {
            StartLoading();
        },
        success: function (response) {
            CloseLoading();
            $('#Modal-lg').modal('show');
            $('#Modal-lg-Label').html(label);
            $('#Modal-lg-Body').html(response);

            $('#FormInModal').data('validator', null);
            $.validator.unobtrusive.parse('#FormInModal');
        },
        error: function () {
            CloseLoading();
            ShowFailedAlert();
        }
    });
}
function GetHtml(url, elem) {
    debugger;
    Loader(1);
    $.get(url, function (res) {
        Loader(0);
        $("#" + elem).html(res);
    });
}
function Loader(show) {
    if (show === 1) {
        $('#loaderModal').modal('show');
    }
    else {
        $("#loaderModal").modal('hide');
    }
}
function GoToUrl(url) {

    window.location.href = url;
}
function CloseModal(url, label) {
    $("#myModal").modal('hide');
}
function ErrorMsg(title, text) {
    $.confirm({
        title: title,
        content: text,
        rtl: true,
        type: 'red',
        typeAnimated: true,
        buttons: {
            confirm: {
                text: "متوجه شدم!",
                btnClass: 'btn-red',
                action: function () {
                }
            }
        }
    });
}
function SuccessMsg(title, text) {
    $.confirm({
        title: title,
        content: text,
        rtl: true,
        type: 'green',
        typeAnimated: true,
        buttons: {
            confirm: {
                text: "متوجه شدم!",
                btnClass: 'btn-green',
                action: function () {
                }
            }
        }
    });
}
function ErrorHandle() {

    var msg = $("#msg").val();
    if (msg !== "" && msg !== undefined && msg !== "undefined" && msg !== null) {
        ErrorMsg("خطا!", msg);
    }
}
function StringIsNullOrEmpty(val) {
    if (val === "" || val === undefined || val === "undefined" || val === null) {
        return true;
    }
    return false;
}
function validateEmail(email) {
    const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}
$("input[data-type='currency']").on({
    keyup: function () {
        debugger;
        var spanId = $(this).attr('data-span');
        $("#" + spanId).html(formatMoney($(this).val()) + " تومان")
        /*formatCurrency($(this));*/
    },
    blur: function () {
        debugger;
        var spanId = $(this).attr('data-span');
        $("#" + spanId).html(formatMoney($(this).val()) + " تومان")
        //formatCurrency($(this), "blur");
    }
});

function formatNumber(n) {
    // format number 1000000 to 1,234,567
    return n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}
function formatCurrency(input, blur) {
    // appends $ to value, validates decimal side
    // and puts cursor back in right position.

    // get input value
    var input_val = input.val();

    // don't validate empty input
    if (input_val === "") { return; }

    // original length
    var original_len = input_val.length;

    // initial caret position 
    var caret_pos = input.prop("selectionStart");

    // check for decimal
    if (input_val.indexOf(".") >= 0) {

        // get position of first decimal
        // this prevents multiple decimals from
        // being entered

        // split number by decimal point
        var left_side = input_val.substring(0, decimal_pos);

        // add commas to left side of number
        left_side = formatNumber(left_side);


        // join number by .
        input_val = "" + left_side;

    } else {
        // no decimal entered
        // add commas to number
        // remove all non-digits
        input_val = formatNumber(input_val);
        input_val = "" + input_val;

    }

    // send updated string to input
    input.val(input_val);

    // put caret back in the right position
    var updated_len = input_val.length;
    caret_pos = updated_len - original_len + caret_pos;
    input[0].setSelectionRange(caret_pos, caret_pos);
}
function readURL(input, id) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#' + id).attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]); // convert to base64 string
    }
}

$("#imgUp").change(function () {
    debugger;
    readURL(this, 'imgPreview');
});
$("#iconUp").change(function () {
    readURL(this, 'iconPreview');
});
function DeleteFunction(url, id) {
    $.confirm({
        title: "هشدار!",
        content: "از حذف مطمئن هستید؟",
        rtl: true,
        type: 'red',
        typeAnimated: true,
        buttons: {
            confirm: {
                text: "بله",
                btnClass: 'btn-red',
                action: function () {
                    DeleteItemPost(url, id);
                }
            },
            cancel: {
                text: 'خیر',
                action: function () {
                }
            }
        }
    });
}
function DeleteItemPost(url, id) {
    debugger;
    var page = $("#PageId").val();
    if (page === undefined || page === "" || page === null) {
        page = "0";
    }

    $.ajax({
        url: `${url}`,
        type: 'POST',
        data: {
            id: id,
            __RequestVerificationToken: GetAntiForgeryToken()
        },
        beforeSend: function () {
            StartLoading();
        },
        success: function (res) {
            CloseLoading();
            if (res.errorId === 0) {
                location.reload();
                return;
            } else {
                if (res.errorTitle !== "") {
                    ErrorMsg('خطا', res.errorTitle);
                }
            }
        },
        error: function () {
            CloseLoading();
            ShowFailedAlert();
        }
    });
}
function GetAntiForgeryToken() {
    var token = document.getElementsByName('__RequestVerificationToken')[0].value;
    debugger;
    return token;
}
function RefreshPage() {
    window.location.reload();
}
function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}
function SendReqAjaxWithFormData(formId = '', url = '', targetId = "TableDiv") {
    $(`form#${formId}`).submit(function (e) {

        StartLoading();
        e.preventDefault();
        var formData = new FormData(this);
        $.ajax({
            url: `${url}`,
            type: 'POST',
            data: formData,
            beforeSend: function () {
                StartLoading();
            },
            success: function (response) {
                CloseLoading();
                FormSubmitHandler(response, targetId);
            },
            error: function () {
                CloseLoading();
                ShowFailedAlert();
            },
            cache: false,
            contentType: false,
            processData: false
        });
    });
}
function SendAjaxReq(url, method = 'GET', body) {
    StartLoading();
    $.ajax({
        url: `${url}`,
        type: `${method}`,
        data: body,
        beforeSend: function () {
            StartLoading();
        },
        success: function (response) {
            CloseLoading();
            if (response.status === 'success') {
                ShowSuccessAlert();
            }
            if (response.status === 'failed') {
                ShowFailedAlert();
            }
        },
        error: function () {
            CloseLoading();
            ShowFailedAlert();
        },
        cache: false,
        contentType: false,
        processData: false
    });
}
function SendReqAjaxChartFilterWithFormData(url, canvasId, chart, formData, type = null, unit = null, isdate = false, backgroundColorArray = null, borderColorArray = null) {
    if (chart) {
        chart?.destroy();
    }
    let existingChart = Chart.getChart(canvasId);
    if (existingChart) {
        existingChart.destroy();
    }
    $.ajax({
        url: url,
        type: 'POST',
        data: formData,
        beforeSend: function () {
            StartLoading();
        },
        success: function (response) {
            CloseLoading();
            const newChart = ChartMaker(canvasId, response.titles, response.values, type, unit, isdate, backgroundColorArray, borderColorArray);
            return newChart;
        },
        error: function () {
            CloseLoading();
            ShowFailedAlert();
        },
        cache: false,
        contentType: false,
        processData: false
    });
}
function ChartMaker(canvasId, titles, values, type = null, unit = null, isdate = false, backgroundColorArray = null, borderColorArray = null) {
    if (StringIsNullOrEmpty(type)) {
        type = "bar";
    }
    if (StringIsNullOrEmpty(backgroundColorArray)) {
        backgroundColorArray = [
            'rgba(120, 179, 206, 0.7)',
            'rgba(227, 142, 73, 0.7)',
            'rgba(130, 160, 216, 0.7)',
            'rgba(112, 92, 83, 0.7)',
            'rgba(117, 5, 80, 0.7)',
            'rgba(5, 146, 18, 0.7)',
            'rgba(136, 214, 108, 0.7)',
            'rgba(255, 41, 41, 0.7)',
            'rgba(255,69,0, 0.7)',
            'rgba(255, 235, 85, 0.7)'
        ];
    }
    if (StringIsNullOrEmpty(borderColorArray)) {
        borderColorArray = [
            'rgba(120, 179, 206, 1)',
            'rgba(227, 142, 73, 1)',
            'rgba(130, 160, 216, 1)',
            'rgba(112, 92, 83, 1)',
            'rgba(117, 5, 80, 1)',
            'rgba(5, 146, 18, 1)',
            'rgba(136, 214, 108, 1)',
            'rgba(255, 41, 41, 1)',
            'rgba(255,69,0, 1)',
            'rgba(255, 235, 85, 1)'
        ]
    }
    debugger;
    const ctx = document.getElementById(canvasId).getContext('2d');
    let titleArray = titles.split(',');
    let valueArray = values.split(',');
    let borderColors = borderColorArray;

    //let normalizedData = normalize(valueArray, titleArray);
    return new Chart(ctx, {
        type: type,
        data: {
            labels: titleArray,
            //labels: normalizedData.map((item) => item.title),
            datasets: [
                {
                    data: valueArray,
                    borderWidth: 2,
                    backgroundColor: isdate ? titleArray.map((item) => SeasonalColoring(item)) : backgroundColorArray,
                    borderColor: isdate ? titleArray.map((item) => SeasonalColoring(item)) : borderColorArray
                }
            ]
        },
        options: {
            plugins: {
                legend: { display: false },
                tooltip: {
                    callbacks: {
                        label: (item) =>
                            `${item.label}: ${item.formattedValue} ${unit ? unit : ""}`,
                    },
                },
            },
            scales: {
                y: {
                    beginAtZero: true,
                    ticks: {
                        font: {
                            family: "Vazir",
                        },
                        callback: function (value, index, ticks) {
                            return `${value} ${unit ? unit : ""}`;
                        }
                    }
                },
                x: {
                    ticks: {
                        font: {
                            family: "Vazir",
                        },
                    },
                }
            },
        }
    });
}
function SeasonalColoring(date) {

    var dateArray = "";
    if (date.includes('/')) {
        dateArray = date.split('/');
    }
    let month = parseInt(dateArray[1]);
    let monthIndex = MonthIndexOfSeason(month);
    let day = parseInt(dateArray[2]);
    let step = 0.7 / 125;
    let opacity = ((monthIndex * 30 + day) * step) + 0.3;
    if (month >= 1 && month <= 3) {
        return `rgba(5, 146, 18, ${opacity})`;
    }
    else if (month >= 4 && month <= 6) {
        return `rgba(117, 5, 80, ${opacity})`;
    }
    else if (month >= 7 && month <= 9) {
        return `rgba(255,69,0, ${opacity})`;
    }
    else if (month >= 10 && month <= 12) {
        return `rgba(130, 160, 216, ${opacity})`;
    }
    else {
        return null;
    }
}
function MonthIndexOfSeason(month) {

    switch (month % 3) {
        case (0): return 3;
        case (1): return 1;
        case (2): return 2;
    }
}
function AjaxSelectBoxFilter(url, data, target, targetChild=null, type = 'GET', defaultValue=true, allOption=false, customOption = '') {
    $.ajax({
        url: url,
        type: type,
        data: data,
        beforeSend: function () {
            StartLoading();
        },
        success: function (data) {
            CloseLoading();
            var optionText ="";
            if (allOption === true) {
                optionText = "همه";
            }
            else if (!StringIsNullOrEmpty(customOption)) {
                optionText = customOption;
            }
            let targetSelect = $(`#${target}`);     
            if (!StringIsNullOrEmpty(targetChild)) {
                let targetChildSelect = $(`#${targetChild}`);
                targetChildSelect.empty();
                if (!StringIsNullOrEmpty(optionText)) {
                    targetChildSelect.append(`<option value="" selected>${optionText}</option>`);
                }
            }
            targetSelect.empty();
            if (defaultValue === true){
                targetSelect.append('<option disabled selected>لطفا موردی را انتخاب کنید</option>');
            }
            else if (!StringIsNullOrEmpty(optionText)) {
                targetSelect.append(`<option value="" selected>${optionText}</option>`);
            }
            $.each(data, function (index, value) {
                targetSelect.append('<option value="' + value.id + '">' + value.title + '</option>');
            });
        },
        error: function () {
            CloseLoading();
            ShowDynamicFailedAlert('خطا در دریافت');
        }
    });
}
class ChartManager {
    constructor(canvasId, options = {}) {
        this.canvasId = canvasId;
        this.chart = null;
        this.options = {
            type: "bar",
            unit: null,
            isdate: false,
            backgroundColorArray: null,
            borderColorArray: null,
            y1 : null,
            ...options
        };
    }

    async sendRequest(url, formData, withAvg = false, type = 'POST') {
        if (this.chart) {
            this.chart.destroy();
        }
        let existingChart = Chart.getChart(this.canvasId);
        if (existingChart) {
            existingChart.destroy();
        }
        try {
            StartLoading();
            const response = await $.ajax({
                url: url,
                type: type,
                data: formData,
                cache: false,
                contentType: false,
                processData: false
            });
            CloseLoading();
            if (withAvg === true) {
                this.createChartWithAvg(response.titles, response.values, response?.averages, response?.totalAverage);
            }
            else {
                this.createChart(response.titles, response.datasets)
            }
        } catch (error) {
            CloseLoading();
            ShowFailedAlert();
        }
    }
    createChart(titles, datasets) {
        let titleArray = titles.split(',');
        let chartDatasets = []
        let chartOptions = this.options;
        Object.keys(datasets).forEach((key, index) => {
            let valueArray = datasets[key].split(',');
            let complementary = (index % 2 != 0);
            chartDatasets.push({
                label: key,
                type: this.options?.type ?? "bar",
                data: valueArray,
                borderWidth: 2,
                backgroundColor: this.options.isdate ? titleArray.map((item) => this.seasonalColoring(item, complementary)) : this.getDefaultColor(false, complementary),
                borderColor: this.options.isdate ? titleArray.map((item) => this.seasonalColoring(item, complementary)) : this.getDefaultColor(true, complementary),
                yAxisID: chartOptions?.y1?.index == index ? 'y1': 'y',
            });
        });
        this.drawChart(titleArray, chartDatasets);
    }
    createChartWithAvg(titles, values, averages = null, totalAverage = null) {
        let titleArray = titles.split(',');
        let valueArray = values.split(',');
        let averageArray = averages.split(',');

        //const backgroundColorArray = this.options.backgroundColorArray || this.getDefaultBackgroundColors();
        //const borderColorArray = this.options.borderColorArray || this.getDefaultBorderColors();

        const averageValue = valueArray.reduce((sum, val) => sum + val, 0) / valueArray.length;
        let datasets = [];
        if (!StringIsNullOrEmpty(totalAverage)) {
            datasets.push({
                label: "میانگین مطالعه",
                type: "line",
                data: Array(titleArray.length).fill(totalAverage),
                borderWidth: 2,
                backgroundColor: "rgba(44, 51, 51, 0.7)",
                borderColor: "rgba(44, 51, 51, 1)",
                borderDash: [2, 2],
                pointRadius: titleArray.length > 0 ? 3 / titleArray.length : 2,
            });
        }
        if (!StringIsNullOrEmpty(values)) {
            datasets.push({
                label: "مقدار مطالعه",
                type: this.options.type,
                data: valueArray,
                borderWidth: 2,
                backgroundColor: this.options.isdate ? titleArray.map((item) => this.seasonalColoring(item)) : this.getDefaultColor(false, false),
                borderColor: this.options.isdate ? titleArray.map((item) => this.seasonalColoring(item)) : this.getDefaultColor(true, false)
            });
        }
        if (!StringIsNullOrEmpty(averages)) {
            datasets.push({
                label: "میانگین مطالعه جامعه",
                type: this.options.type,
                data: averageArray,
                borderWidth: 2,
                backgroundColor: this.options.isdate ? titleArray.map((item) => this.seasonalColoring(item, true)) : this.getDefaultColor(false, true),
                borderColor: this.options.isdate ? titleArray.map((item) => this.seasonalColoring(item, true)) : this.getDefaultColor(true, true)
            });
        }
        this.drawChart(titleArray, datasets);
    }
    drawChart(titles, datasets) {
        const ctx = document.getElementById(this.canvasId).getContext('2d');
        this.chart = new Chart(ctx, {
            type: this.options.type,
            data: {
                labels: titles,
                datasets
            },
            options: {
                plugins: {
                    legend: {
                        labels: {
                            font: {
                                family: "Vazir",
                            },
                        },
                    },
                    tooltip: {
                        callbacks: {
                            label: (item) => `${item.label}: ${item.formattedValue} ${this.options.unit ? this.options.unit : ""}`,
                        },
                    },
                },
                scales: {
                    y: {
                        beginAtZero: true,
                        ticks: {
                            font: {
                                family: "Vazir",
                            },
                            //callback: (value) => `${value} ${this.options.unit ? this.options.unit : ""}`
                        },
                        title: {
                            display: true,
                            text: this.options.unit ?? "",
                            font: {
                                family: "Vazir",
                            },
                        },
                    },
                    x: {
                        ticks: {
                            font: {
                                family: "Vazir",
                            },
                        },
                    },
                    y1: this.options?.y1
                }
            }
        });
    }
    getDefaultColor(border = false, avg = false) {
        var opacity = border ? '1' : '0.7';
        if (avg) {
            return `rgba(255, 125, 14, ${opacity})`;
        } else {
            return `rgba(10, 154, 161, ${opacity})`;
        }
    }
    getDefaultBackgroundColors() {
        return [
            'rgba(120, 179, 206, 0.7)',
            'rgba(227, 142, 73, 0.7)',
            'rgba(130, 160, 216, 0.7)',
            'rgba(112, 92, 83, 0.7)',
            'rgba(117, 5, 80, 0.7)',
            'rgba(5, 146, 18, 0.7)',
            'rgba(136, 214, 108, 0.7)',
            'rgba(255, 41, 41, 0.7)',
            'rgba(255,69,0, 0.7)',
            'rgba(255, 235, 85, 0.7)'
        ];
    }

    getDefaultBorderColors() {
        return [
            'rgba(120, 179, 206, 1)',
            'rgba(227, 142, 73, 1)',
            'rgba(130, 160, 216, 1)',
            'rgba(112, 92, 83, 1)',
            'rgba(117, 5, 80, 1)',
            'rgba(5, 146, 18, 1)',
            'rgba(136, 214, 108, 1)',
            'rgba(255, 41, 41, 1)',
            'rgba(255,69,0, 1)',
            'rgba(255, 235, 85, 1)'
        ];
    }

    seasonalColoring(date, complementary = false) {
        let dateArray = date.includes('/') ? date.split('/') : [];
        if (dateArray.length < 3) return "rgba(0, 0, 0, 0.5)";

        let month = parseInt(dateArray[1]);
        let monthIndex = this.getMonthIndexOfSeason(month);
        let day = parseInt(dateArray[2]);
        let step = 0.7 / 125;
        let opacity = ((monthIndex * 30 + day) * step) + 0.3;
        let colorCode = null;
        if (month >= 1 && month <= 3) {
            colorCode = complementary ? "234, 25, 20" : "5, 146, 18";
        } else if (month >= 4 && month <= 6) {
            colorCode = complementary ? "133, 174, 27" : "141, 22, 102"; 
        } else if (month >= 7 && month <= 9) {
            colorCode = complementary ? "0, 176, 96" : "255, 69, 0"; ;
        } else if (month >= 10 && month <= 12) {
            colorCode = complementary ? "244, 184, 74" :"130, 160, 216";
        }
        return colorCode != null ? `rgba(${colorCode}, ${opacity})` : null;
    }

    getMonthIndexOfSeason(month) {
        switch (month % 3) {
            case 0: return 3;
            case 1: return 1;
            case 2: return 2;
            default: return 0;
        }
    }
    avgCompare(main, avg) {
        main = parseInt(main);
        avg = parseInt(avg);
        if (main >= avg) {
            return "rgba(62, 123, 39, 0.7)";
        }
        else {
            return "rgba(229, 32, 32, 0.7)";
        }
    }
}