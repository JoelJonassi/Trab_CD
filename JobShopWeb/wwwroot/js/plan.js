var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Plan/GetAllPlans",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idSimulation", "width": "30%" },
            { "data": "idJob", "width": "30%" },
            { "data": "idOperation", "width": "20%" },
            { "data": "idMachine", "width": "30%" },
            { "data": "initialTime", "width": "30%" },
            { "data": "finalTime", "width": "30%" },

            {
                "data": "idSimulation",
                "render": function (data) {
                    return `<div class="text-center">
                                 <a href="/plan/Upsert/${data}" class='btn btn-success text-white'
                                    style='cursor:pointer;'> <i class="fa fa-eye" aria-hidden="true"></i></a>
                                    &nbsp;
                                <a href="/Plan/Upsert/${data}" class='btn btn-success text-white'
                                    style='cursor:pointer;'> <i class='far fa-edit'></i></a>
                                    &nbsp;
                                <a onclick=Delete("/Plan/Delete/${data}") class='btn btn-danger text-white'
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