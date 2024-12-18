﻿var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/materialtype/getall',
            dataSrc: 'data'  
        },
        "columns": [
            {
                data: 'mtCode',
                "width": "15%"
            },
            {
                data: 'mtText',
                "width": "25%"
            },
            {
                data: 'isPassive',
                "width": "5%",
                render: function (data, type, row) {
                    return data === 1 ? 'Evet' : 'Hayır';
                }
            },
            {
                data: 'companyText',
                "width": "10%"
            },
            {
                data: 'mtCode',
                "render": function (data) {
                    return `<div class="btn-group w-100" role="group">
                        <a href="/materialtype/edit?mtCode=${data}" class="btn btn-primary mx-2"> 
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
        var uniCode = $(this).data('id');
        window.location.href = `/materialtype/delete?mtCode=${uniCode}`;
    });
}
