function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/WorkCenterAna/GetAll"
        },
        "columns": [
            { "data": "company.comcode", "width": "10%" },
            { "data": "workCenter.wcmdoctype", "width": "10%" },
            { "data": "wcmdocnum", "width": "15%" },
            { "data": "wcmdocfrom", "width": "15%" },
            { "data": "wcmdocuntil", "width": "15%" },
            { "data": "language.lancode", "width": "10%" },
            { "data": "worktime", "width": "10%" },
            {
                "data": {
                    comcode: "comcode",
                    wcmdoctype: "wcmdoctype",
                    wcmdocnum: "wcmdocnum",
                    wcmdocfrom: "wcmdocfrom",
                    wcmdocuntil: "wcmdocuntil",
                    lancode: "lancode",
                    oprdoctype: "oprdoctype"
                },
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a href="/WorkCenterAna/Upsert?comcode=${data.comcode}&wcmdoctype=${data.wcmdoctype}&wcmdocnum=${data.wcmdocnum}&wcmdocfrom=${data.wcmdocfrom}&wcmdocuntil=${data.wcmdocuntil}&lancode=${data.lancode}&oprdoctype=${data.oprdoctype}"
                               class="btn btn-primary mx
                               class="btn btn-primary mx