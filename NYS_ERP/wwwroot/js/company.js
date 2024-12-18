var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/company/getall',
            dataSrc: 'data'
        },
        "columns": [
            {
                data: 'comCode',
                "width": "15%"
            },
            {
                data: 'comText',
                "width": "25%"
            },
            {
                data: 'address1',
                "width": "15%"
            },
            {
                data: 'address2',
                "width": "15%"
            },
            {
                data: 'comCode',
                "render": function (data) {
                    return `<div class="btn-group w-100" role="group">
                        <a href="/company/edit?comCode=${data}" class="btn btn-primary mx-2"> 
                            <i class="bi bi-pencil-square"></i> Edit
                        </a>
                        <a href="javascript:void(0);" class="btn btn-danger mx-2 delete-btn" data-id="${data}">
                            <i class="bi bi-trash-fill"></i> Delete
                        </a>
                    </div>`;
                },
                "width": "35%"
            }
        ]
    });

    $('#tblData').on('click', '.delete-btn', function () {
        var comCode = $(this).data('id');
        window.location.href = `/company/delete?comCode=${comCode}`;
    });
}