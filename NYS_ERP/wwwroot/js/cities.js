var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/city/getall',  
            dataSrc: 'data'  
        },
        "columns": [
            {
                data: 'cityCode',  
                "width": "15%"
            },
            {
                data: 'cityText', 
                "width": "15%"
            },
            {
                data: 'countryText',  
                "width": "15%"
            },
            {
                data: 'companyText',  
                "width": "25%"
            },
            {
                data: 'cityCode',  
                "render": function (data) {
                    return `<div class="btn-group w-100" role="group">
                        <a href="/city/edit?cityCode=${data}" class="btn btn-primary mx-2">
                            <i class="bi bi-pencil-square"></i> Edit
                        </a>
                        <a href="javascript:void(0);" class="btn btn-danger mx-2 delete-btn" data-id="${data}">
                            <i class="bi bi-trash-fill"></i> Delete
                        </a>
                    </div>`;
                },
                "width": "25%"
            }
        ]
    });

    $('#tblData').on('click', '.delete-btn', function () {
        var cityCode = $(this).data('id');
        window.location.href = `/city/delete?cityCode=${cityCode}`;
    });
}
