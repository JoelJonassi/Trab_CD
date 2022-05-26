var dataTable;

$(document).ready(function () {
    loadDataTable();
});

$("#btnExport").click(function (e) {
    window.open('data:application/vnd.ms-excel,' + $('#tblData').html());
    e.preventDefault();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/job/GetAllJobs",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idJob", "width": "20%" },
            { "data": "nameJob", "width": "20%" },
            { "data": "idOperation", "width": "20%" },
            
            {
                "data": "idJob",
                "render": function (data) {
                    return `
                                <div class="text-center">

                                <a href="/Job/Upsert/${data}" class='btn btn-success text-white'
                                    style='cursor:pointer;'> <i class="fa fa-eye" aria-hidden="true"></i></a>
                                    &nbsp;
                                <a href="/Job/Upsert/${data}" class='btn btn-success text-white'
                                    style='cursor:pointer;'> <i class='far fa-edit'></i></a>
                                    &nbsp;
                                <a onclick=Delete("/Job/Delete/${data}") class='btn btn-danger text-white'
                                    style='cursor:pointer;'> <i class='far fa-trash-alt'></i></a>
                                </div>
                            `;
                }, "width": "30%"
            }
        ]
    });
}



function Delete(url) {
    swal({
        title: "Are you sure you want to Delete?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true

    }).then((willDelete) => {
        if (willDelete)
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
    });
}