var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Job/GetAllJobs",
            "type": "GET",
            "datatype": "json"
        },

        "columns": [
            { "data": "idJob", "width": "10%" },
            { "data": "nameJob", "width": "10%" },
            { "data": "idOperation", "width": "10%" },
            { "data": "nameOperation", "width": "10%" },
            { "data": "idMachine", "width": "10%" },       
            { "data": "nameMachine", "width": "10%" },
            { "data": "time", "width": "10%" },
            
            {
                "data": "idJob",
                "render": function (data) {
                    return `<div class="text-center">

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
        if (willDelete) {
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
        }
    });
}