$(document).on("click", ".sortable th", function () {
    let sortForm = $(this).closest(".sortable").data("form");
    let sortInput = $(this).closest(".sortable").data("input");
    let sortStr = $(this).closest(".sortable").data("sortstr") || "";
    if (StringIsNullOrEmpty(sortForm) || StringIsNullOrEmpty(sortInput)) {
        return;
    }
    let sort = $(this).data("sort");
    if (StringIsNullOrEmpty(sort)) {
        return;
    }
    $(this).find("i.fa").remove();
    if (sortStr.includes(`-${sort}`)) {
        sortStr = sortStr.replace(`-${sort}`, sort);
        //$(this).append(' <i class="fa fa-sort-up"></i>');
    }
    else if (sortStr.includes(sort)) {
        sortStr = sortStr.replace(sort, `-${sort}`);
        //$(this).append(' <i class="fa fa-sort-down"></i>');
    } else {
        sortStr += sortStr ? `,${sort}` : sort;
        //$(this).append(' <i class="fa fa-sort-up"></i>');
    }
    $(this).closest(".sortable").data("sortstr", sortStr);
    $(`#${sortInput}`).val(sortStr);
    $(`#${sortForm}`).submit();
});
function loadSorts(table) {
    const $table = $(`#${table}`);
    const sortStr = $table.closest(".sortable").data("sortstr") || "";
    const sorts = sortStr.split(",");

    sorts.forEach((sort) => {
        let sortIcon = "fa-sort-up";

        if (sort.startsWith('-')) {
            sort = sort.replace('-', '');
            sortIcon = "fa-sort-down";
        }
        const iTag = `<i class="fa ${sortIcon}"></i>`;
        const $th = $table.find(`th[data-sort="${sort}"]`);
        $th.append(iTag);
    });
}
$(".removeSortBtn").on("click", function () {
    const inputId = $(this).data("input");
    const $input = $(`#${inputId}`);
    $input.val("");
});
