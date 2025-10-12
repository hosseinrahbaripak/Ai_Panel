function FormSubmitHandler(response, targetId = "TableDiv") {
    CloseLoading();
    if (response.status === undefined || response.status === null) {
        // It means the response is text/html
        $(`#${targetId}`).html(response);

    }
    if (response.status === "MobileFormatInCorrect") {
        MobileFormatInCorrectAlert();
    }
    if (response.status === "objectExist") {
        ShowDynamicFailedAlert('رکوردی با این مشخصات موجود میباشد !');
    }
    if (response.status === "objectNotValid") {
        ShowDynamicFailedAlert('لطفا تمامی اطلاعات را به درستی وارد کنید !');
    }
    if (response.status === "systemError") {
        ShowDynamicFailedAlert('خطایی در سرور رخ داده است لطفا با پشتیبانی تماس بگیرید !');
    }
    if (response.status === "fileNotSave") {
        ShowDynamicFailedAlert('خطایی در هنگام ذخیره فایل رخ داد لطفا با پشتیبانی تماس بگیرید !');
    }
    if (response.status === "failedToImportDeleteUserExcel") {
        ShowDynamicWarningAlert('عملیات با خطا مواجه شد !', '', `<a href='/excel/export/${response.excelFileName}' class="btn btn-outline-danger" target="_blank">دانلود اکسل گزارش حذف کردن کاربران</a>`);
    }
    if (response.status === "success") {
        ShowSuccessAlert();
        var modal = $('#exampleModal');
        if (modal !== null && modal !== undefined) {
            $('#exampleModal').modal('hide');
            $('#Modal-lg').modal('hide');
        }
        var divShouldRefresh = $(`#${targetId}`);
        if (divShouldRefresh !== null && divShouldRefresh !== undefined) {
            $(`#${targetId}`).load(location.href + " #" + targetId);
        }
    }
}

function AddBookGroup() {
    $.ajax({
        url: "/Admin/BookCategory/Add",
        type: "get",
        data: {},
        beforeSend: function () {
            StartLoading();
        },
        success: function (response) {
            CloseLoading();

            $('#exampleModal').modal('show');
            $('#exampleModalLabel').html('افزودن گروه گتاب');
            $('#modalbody').html(response);

            $('#FormInModal').data('validator', null);
            $.validator.unobtrusive.parse('#FormInModal');
        },
        error: function () {
            CloseLoading();
            ShowFailedAlert();
        }
    });
}

function EditBookGroup(groupId) {
    $.ajax({
        url: "/Admin/BookCategory/Edit/" + groupId,
        type: "get",
        data: {},
        beforeSend: function () {
            StartLoading();
        },
        success: function (response) {
            CloseLoading();
            $('#exampleModal').modal('show');
            $('#exampleModalLabel').html('ویرایش گروه گتاب');
            $('#modalbody').html(response);

            $('#FormInModal').data('validator', null);
            $.validator.unobtrusive.parse('#FormInModal');
        },
        error: function () {
            CloseLoading();
            ShowFailedAlert();
        }
    });
}

