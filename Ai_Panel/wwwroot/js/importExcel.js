
//let excelInterval;
//document.getElementById("submitExcel").style.display = "none";
//function SubmitExcel(url,checkExcelStatusUrl) {
//    clearInterval(excelInterval);
//    //let excel = document.getElementById("Excel").files[0];
//    var formData = new FormData(document.querySelector('form'));
//    fetch(url, { method: "POST", body: formData });
//    document.getElementById("ExcelStatus").innerHTML = "در حال پردازش اکسل لطفا منتظر بمانید";

//    document.getElementById("submitExcel").style.display = "block";
        
//    var btn = document.getElementById("submitExcel-btn");
//    document.getElementById("submitExcel-btn").disabled = true;
//    document.getElementById("submitExcel-btn").style.pointerEvents = "none";

//    CheckExcelStatus(checkExcelStatusUrl);
//}
//function CheckExcelStatus(url) {
//    excelInterval = setInterval(ExcelStatus, 5000,url);
//}
//function ExcelStatus(url) {
//    const status = document.getElementById("ExcelStatus");
//    status.classList.add("text-primary");
//    fetch(url, { method: "GET" }).then(response => response.json())
//        .then(data => {
//            if (data.message === '') {
//                status.innerHTML = "تعداد ردیف خوانده شده : " + data.row;
//            }
//            else {
//                status.innerHTML = data.message;
//                if (data.success) {
//                    status.classList.add("text-success");
//                }
//                else {
//                    if (data.reportExcelFileName != "") {
//                        document.getElementById("ReportImportExcel").classList.remove('d-none');
//                        document.getElementById("ReportImportExcel").href = `/excel/export/${data.reportExcelFileName}`;
//                    }
//                    status.classList.add("text-danger");
//                }
//                clearInterval(excelInterval);
//                document.getElementById("submitExcel").style.display = "none";

//                var btn = document.getElementById("submitExcel-btn");
//                document.getElementById("submitExcel-btn").disabled = false;
//                document.getElementById("submitExcel-btn").style.pointerEvents = "auto";

//                document.getElementById("Excel").value = '';
//            }

//        });

//}