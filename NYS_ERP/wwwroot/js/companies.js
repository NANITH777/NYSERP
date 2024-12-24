$(document).ready(function () {
    $('#tblData').DataTable({
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'copy',
                className: 'btn btn-secondary btn-sm',
                text: '<i class="bi bi-clipboard"></i> Copy'
            },
            {
                extend: 'excel',
                className: 'btn btn-secondary btn-sm',
                text: '<i class="bi bi-file-earmark-excel"></i> Excel'
            },
            {
                extend: 'pdf',
                className: 'btn btn-secondary btn-sm',
                text: '<i class="bi bi-file-earmark-pdf"></i> PDF'
            },
            {
                extend: 'print',
                className: 'btn btn-secondary btn-sm',
                text: '<i class="bi bi-printer"></i> Print'
            }
        ],
        language: {
            url: "//cdn.datatables.net/plug-ins/1.13.3/i18n/en.json"
        }
    });
});