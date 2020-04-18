var dataTable;

$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/Document/getall/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "year", "width": "2%" },
            { "data": "grant", "width": "8%" },
            { "data": "catagory","width": "7%" },
            { "data": "subCatagory", "width": "8%" },
            { "data": "about", "width": "8%" },
            { "data": "other", "width": "7%" },
            { "data": "region", "width": "8%" },
            { "data": "fileName", "width": "7%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/Document/Download?id=+${data}" target="_blank" class='btn btn-info text-white' style='cursor:pointer; width:100px;')                            
>
                            Download
                        </a>
                        
                        <a href="/Document/Upsert?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:70px;'>
                            Edit
                        </a>
                        
                        <a class='btn btn-danger text-white' style='cursor:pointer; width:70px;'
                            onclick=Delete('/Document/Delete?id='+${data})>
                            Delete
                        </a>
                        </div>`;
                }, "width": "45%" 
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
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

function Download(url) {
           /* $.ajax({
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            }); */
        $.ajax({
            url: url,
            method: 'GET',       // Worked using POST or PUT. Prefer POST
            responseType: 'blob', // important
        }).done(function () {
            alert('Added');
        });
    }
       

