//#region WaitMe
function StartLoading(element = 'body') {
    $(element).waitMe({
        effect: 'bounce',
        text: 'در حال بارگذاری لطفا صبر کنید...',
        bg: 'rgba(255, 255, 255, 0.7)',
        color: '#000'
    });
}
function CloseLoading(element = 'body') {
    $(element).waitMe('hide');
}
//#endregion

//#region Sweet Alert 
function ShowDynamicWarningAlert(message) {
    Swal.fire(
        '' + message,
        '',
        'warning'
    );
}
function ShowSuccessAlert() {
    Swal.fire(
        'عملیات با موفقیت انجام شد !',
        '',
        'success'
    )
}
function ShowSuccessAddBookToCardAlert() {
    Swal.fire(
        'کتاب با موفقیت به سبد خرید اضافه شد !',
        '',
        'success'
    )
}
function ShowSuccessRemoveBookFromCardAlert() {
    Swal.fire(
        'کتاب با موفقیت از سبد خرید حذف شد !',
        '',
        'success'
    )
}
function ShowFailedAlert() {
    Swal.fire(
        'عملیات با خطا مواجه شد !',
        '',
        'error'
    );
}
function ShowDynamicFailedAlert(title = '', message = '', footer = '') {
    Swal.fire({
        icon: 'error',
        title: `${title}`,
        text: `${message}`,
        footer: `${footer}`
    });
}
function ShowDynamicWarningAlert(title = '', message = '', footer = '') {
    Swal.fire({
        icon: 'warning',
        title: `${title}`,
        text: `${message}`,
        footer: `${footer}`
    });
}
function WrongOldPasswordAlert() {
    Swal.fire(
        'کلمه عبور فعلی اشتباه می باشد !',
        '',
        'error'
    )
}
function MobileFormatInCorrectAlert() {
    Swal.fire(
        'تلفن همراه نامعتبر است',
        '',
        'error'
    )
}
function ActiveCodeInCorrectAlert() {
    Swal.fire(
        'کد فعال سازی نامعتبر است',
        '',
        'error'
    )
}
function UserHasBookAlert() {
    Swal.fire(
        'برخی از کتاب هایی که انتخاب کرده اید در حساب کاربری شما موجود است لطفا سبد خرید خود را دوباره بررسی کنید',
        '',
        'info'
    )
}
function WrongNewPassAndRePassAlert() {
    Swal.fire(
        'کلمه عبور و تکرار آن یکی نیستند !',
        '',
        'error'
    )
}
function ShowObjectExistAlert() {
    Swal.fire({
        icon: 'error',
        title: 'عملیات با خطا مواجه شد !',
        text: 'ادمینی با این نام کاربری و یا ایمیل موجود است !',
    })
}
function ShowRoleDeniedAlert() {
    Swal.fire(
        'عملیات با خطا مواجه شد !',
        'امکان اضافه کردن این نقش به این ادمین ممکن نیست.',
        'error'
    )
}
function BookExistInCartAlert() {
    Swal.fire(
        'کتاب در سبد خرید موجود می باشد !',
        '',
        'error'
    )
}
function BookExistInAccountAlert() {
    Swal.fire(
        'کتاب در حساب کاربری شما موجود می باشد !',
        '',
        'error'
    )
}
function AskImportWhichExcelAlert() {
    Swal.fire({
        icon: 'info',
        title: 'کدام یک از اکسل های زیر را میخواهید وارد کنید ؟',
        html: ` <a href="/Admin/User/Add/AddUsersWithExce" class="btn btn-outline-info" >اکسل افزودن و آپدیت کاربران</a>
                <a href="/Admin/User/Add/AddAdvisorToUsersWithExce" class="btn btn-outline-info" >اکسل اتصال کاربر به مشاور</a>
              <a href="/Admin/User/Edit?handler=InputExcel" class="btn btn-outline-info" >اکسل آپدیت کاربران</a>
              <a href="/Admin/User/Delete/DeleteUsersWithExcel" class="btn btn-outline-danger mt-1" >حذف کاربران</a>`,
        showConfirmButton: false,
    });
}
function AskAdvisorImportWhichExcelAlert() {
    Swal.fire({
        icon: 'info',
        title: 'کدام یک از اکسل های زیر را میخواهید وارد کنید ؟',
        html: ` <a class="btn btn-outline-info" href="/Admin/AdvisorProfile/Add/AddWithExcel")">
                    اکسل افزودن و آپدیت مشاورین
                </a>
                <a class="btn btn-outline-danger" href="/Admin/AdvisorProfile/Delete/DeleteAdvisorsWithExcel")">
                    اکسل حذف مشاورین
                </a>
                <a class="btn btn-outline-info" href="/Admin/AdvisorProfile/Add/AddAdvisorToParentExcel")">
                    اکسل اتصال مشاور به سر مشاور
                </a>`,
        showConfirmButton: false,
    });
}
function AskWhichAdminActionWantAlert() {
    Swal.fire({
        icon: 'info',
        title: 'گزارش کدام یک از عملکرد های ادمین را میخواهی مشاهده کنی ؟',
        html: `<a href="/Admin/AdminAction/Index?pageId=1&typeId=1" class="btn btn-outline-info" >کاربران</a>
              <a href="/Admin/AdminAction/Index?pageId=1&typeId=3" class="btn btn-outline-info" >ادمین ها</a> `,
        showConfirmButton: false,
    });
}
//#endregion

function FormSubmit(response) {
    CloseLoading();
    if (response.status.toLowerCase() === "success") {
        ShowSuccessAlert();
        $('#exampleModal').modal('hide');
        $('#TableDiv').load(location.href + " #TableDiv");
    }
    if (response.status === "ObjectExist") {
        ShowObjectExistAlert();
    }
    if (response.status === "RoleDenied") {
        ShowRoleDeniedAlert();
    }
    if (response.status === "Error") {
        ShowFailedAlert();
    }
    if (response.status === "WrongOldPassword") {
        WrongOldPasswordAlert();
    }
    if (response.status === "WrongNewPassAndRePass") {
        WrongNewPassAndRePassAlert();
    }
    if (response.status === "adminTypeRequired") {
        ShowDynamicFailedAlert('لطفا نوع ادمین را انتخاب کنید !');
    }
}

//#region Admin Manage
function AddAdmin() {
    $.ajax({
        url: "/Admin/AdminManage/Add",
        type: "get",
        data: {
        },
        beforeSend: function () {
            StartLoading();
        },
        success: function (response) {
            CloseLoading();
            $('#modalbody').html(response);
            $('#exampleModalLabel').html('افزودن ادمین');

            $('#FormInModal').data('validator', null);
            $.validator.unobtrusive.parse('#FormInModal');

            $('#exampleModal').modal('show');
        },
        error: function () {
            CloseLoading();
        }
    });
}
function EditAdmin(AdminId) {
    $.ajax({
        url: "/Admin/AdminManage/Edit/" + AdminId,
        type: "get",
        data: {
        },
        beforeSend: function () {
            StartLoading();
        },
        success: function (response) {
            CloseLoading();
            $('#modalbody').html(response);
            $('#exampleModalLabel').html('ویرایش ادمین');

            $('#FormInModal').data('validator', null);
            $.validator.unobtrusive.parse('#FormInModal');

            $('#exampleModal').modal('show');
        },
        error: function () {
            CloseLoading();
        }
    });
}
function AdminChangePass(AdminId) {
    $.ajax({
        url: "/Admin/Home/Edit/" + AdminId,
        type: "get",
        data: {
        },
        beforeSend: function () {
            StartLoading();
        },
        success: function (response) {
            CloseLoading();
            debugger;
            $('#modalbody').html(response);
            $('#exampleModalLabel').html('ویرایش کلمه عبور');

            $('#FormInModal').data('validator', null);
            $.validator.unobtrusive.parse('#FormInModal');

            $('#exampleModal').modal('show');
        },
        error: function () {
            CloseLoading();
        }
    });
}

//#endregion

//#region BookShop Ajax
function AddBookToCard(BookId) {
    $.ajax({
        url: "/BookShop/AddBookToCard/" + BookId,
        type: "get",
        data: {
        },
        beforeSend: function () {
            StartLoading();
        },
        success: function (response) {
            CloseLoading();
            if (response.status === "Success") {
                ShowSuccessAddBookToCardAlert();
                $('#TableDiv').load(location.href + " #TableDiv");
            }
            if (response.status === "BookExistInCart") {
                BookExistInCartAlert();
            }
            if (response.status === "UserHasBook") {
                BookExistInAccountAlert();
            }
            if (response.status === "Error") {
                ShowFailedAlert();
            }
        },
        error: function () {
            CloseLoading();
        }
    });
}
function RemoveBookFromCard(BookId) {
    $.ajax({
        url: "/BookShop/RemoveBookFromCard/" + BookId,
        type: "get",
        data: {
        },
        beforeSend: function () {
            StartLoading();
        },
        success: function (response) {
            CloseLoading();
            if (response.status === "Success") {
                ShowSuccessRemoveBookFromCardAlert();
                $('#TableDiv').load(location.href + " #TableDiv");
            }
            if (response.status === "Error") {
                ShowFailedAlert();
            }
        },
        error: function () {
            CloseLoading();
        }
    });
}
function LoginFormSubmit(response) {
    CloseLoading();
    if (response.status === "MobileFormatInCorrect") {
        MobileFormatInCorrectAlert();
    }
    else {
        $('#Modal-Static-Body').html(response);
        $('#Modal-Static-Label').html('فعال سازی حساب');
    }
}
function CmpProfileFormSubmit(response) {
    debugger;
    CloseLoading();
    if (response.status === "Error") {
        ShowFailedAlert();
    }
    if (response.status === "RedirectToUserBook") {
        $('#exampleModal').modal('hide');
        var host = window.location.host;
        var url = "https://" + host + "/BookShop/SelectedBook";
        window.location.replace(url);
    }
    if (response.status === "UserHasBook") {
        $('#exampleModal').modal('hide');
        $('#TableDiv').load(location.href + " #TableDiv");
        UserHasBookAlert();
        return;
    }
}
function RedirectToIndexBookShop() {
    var host = window.location.host;
    var url = "https://" + host + "/BookShop";
    window.location.replace(url);
}
function LoginBookShop() {
    $.ajax({
        url: "/BookShop/Login",
        type: "get",
        data: {
        },
        beforeSend: function () {
            StartLoading();
        },
        success: function (response) {
            CloseLoading();
            $('#Modal-Static-Body').html(response);
            $('#Modal-Static-Label').html('تأیید شماره همراه کتابخوان');
            $('#FormInModal').data('validator', null);
            $.validator.unobtrusive.parse('#FormInModal');
            $('#Modal-Static').modal('show');
        },
        error: function () {
            CloseLoading();
        }
    });
}
function ActivationFormSubmit(response) {
    CloseLoading();
    if (response.status === null || response.status === undefined) {
        $('#Modal-Static-Body').html(response);
        $('#Modal-Static-Label').html('انتخاب حلی کد');
        return;
    }
    if (response.status === "ActiveCodeIncorrect") {
        ActiveCodeInCorrectAlert();
    }
    if (response.status === "Error") {
        ShowFailedAlert();
    }
    if (response.status.startsWith("UserProfileNotCmp")) {
        $('#Modal-Static').modal('hide');
        var mobileNumber = response.status.split(",")[1];
        var helliCode = response.status.split(",")[2];
        OpenModal('/BookShop/CompleteProfile/' + mobileNumber + '/' + helliCode, 'تکمیل پروفایل');
    }
    if (response.status === "RedirectToUserBook") {
        $('#Modal-Static').modal('hide');
        var host = window.location.host;
        var url = "https://" + host + "/BookShop/SelectedBook";
        window.location.replace(url);
    }
    if (response.status === "UserHasBook") {
        $('#Modal-Static').modal('hide');
        $('#TableDiv').load(location.href + " #TableDiv");
        UserHasBookAlert();
    }
}

//#endregion

//#region ViewDataAdminSubmit
function ViewDataAdminSubmit(ActionId) {
    $.ajax({
        url: "/Admin/AdminAction/Index/ViewDataAdminSubmit/" + ActionId,
        type: "get",
        data: {
        },
        beforeSend: function () {
            StartLoading();
        },
        success: function (response) {
            CloseLoading();
            $('#Modal-lg-Body').html(response);
            $('#Modal-lg-Label').html('اطلاعات ارسالی ادمین');
            $('#Modal-lg').modal('show');

            var script = document.createElement('script');
            script.src = "/js/site.js";
            document.body.appendChild(script);
        },
        error: function () {
            CloseLoading();
        }
    });
}

function showTemporaryModal(message, status = "info") {
    const modal = document.createElement("div");
    modal.className = "temporary-modal";

    const messageHolder = document.createElement("div");
    messageHolder.innerText = message;
    console.log("wtf");
    if (status === "success") {
        messageHolder.className = "bg-success text-white";
    } else if (status === "warning" ) {
        messageHolder.className = "bg-warning text-dark";
    } else if (status === "error") {
        messageHolder.className = "bg-danger text-white";
    }else {
        messageHolder.className = "bg-secondary text-white";
    }
    modal.appendChild(messageHolder);
    document.body.appendChild(modal);
    setTimeout(() => {
        modal.classList.add("active");
    }, 10);

    setTimeout(() => {
        modal.classList.remove("active");
    }, 2010);
}
//#endregion