var popup, dataTable;
var entity = 'Talleres';
var apiurl = 'api/' + entity;

$(document).ready(function () {
    
    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        $.fn.dataTable.tables({ visible: true, api: true }).columns.adjust();
    });

    dataTable = $('#grid').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": apiurl,
            "type": 'POST',
            "datatype": 'json'
        },

        "responsive": true,
        "columns": [
            { "data": "razonsocial_tall" }, // business name
            { "data": "alias_tall" },
            { "data": "lC_tall" }, 
            { "data": "hpdV_tall" },
            { "data": "ensenA_tall" },
            { "data": "direccion_tall" }, 
            { "data": "poblacion_tall" }, // population
            { "data": "cp_tall" },
            { "data": "provincia_tall" }, // province
            {
                "data": "pais_tall", // country
            },
            {
                "data": "fechaDesde_tall", // date from
                "render": function (data) {
                    var dateSplit = data.split('T');
                    var dateSplit1 = dateSplit[0].split('-');
                    return dateSplit1[2] + "/" + dateSplit1[1] + "/" + dateSplit1[0];
                }
            }, 
            { "data": "regioN_tall" },
            {
                "data": "id_tall",
                "render": function (data) {
                    var btnEdit = "<a onclick=ShowPopup('" + entity + "/Edit/" + data + "')><i class='fa fa-pencil'></i>Ver Detalles</a>";
                    var btnDelete = "<a onclick=Delete('" + data + "')><i class='fa fa-trash'></i>Borrar</a>";
                    var btnActions =
                        "<div class=btn-group style='display: flex;'>" +
                        "<button type='button' class='btn btn-default'> Acciones</button>" +
                        "<button type='button' class='btn btn-default dropdown-toggle' data-toggle=dropdown>" +
                        "<span class='caret'></span>" +
                        "<span class='sr-only'>Toggle Dropdown</span>" +
                        "</button>" +
                        "<ul class='dropdown-menu' role='menu'>" +
                        "<li style='cursor: pointer;'>" + btnEdit + "</li>" +
                        "<li style='cursor: pointer;'>" + btnDelete + "</li>" +
                        "</ul>" +
                        "</div >";
                    return btnActions;
                }
            },
        ],
        "columnDefs": [
            {
                "orderable": false, "targets": [12],
            }
        ],
        "fixedColumns": true,
        "dom": 'Blfrtip',
        "language": {
            "emptyTable": "no data found.",
            "processing": "<i class='fa fa-spinner fa-spin fa-3x fa-fw'>"
        },
        "buttons": [
            //{
            //    extend: 'copyHtml5',
            //    text: '<button class="btn btn-default btn-sm"><i class="fa fa-files-o" style="color:#626262;"></i> Copy</button>',
            //    titleAttr: 'Copiar',
            //    exportOptions: {
            //        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11]
            //    },
            //    action: newExportAction
            //},
            //{
            //    extend: 'excelHtml5',
            //    text: '<button class="btn btn-default btn-sm"><i class="fa fa-file-excel-o" style="color:#626262;"></i> Excel</button>',
            //    titleAttr: 'Excel',
            //    exportOptions: {
            //        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11]
            //    },
            //    action: newExportAction
            //},
            //{
            //    extend: 'csvHtml5',
            //    text: '<button class="btn btn-default btn-sm"><i class="fa fa-file-text-o" style="color:#626262;"></i> CSV</button>',
            //    titleAttr: 'CSV',
            //    exportOptions: {
            //        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11]
            //    },
            //    action: newExportAction
            //},
            //{
            //    extend: 'pdfHtml5',
            //    text: '<button class="btn btn-default btn-sm"><i class="fa fa-file-pdf-o" style="color:#626262;"></i> PDF</button>',
            //    titleAttr: 'PDF',
            //    exportOptions: {
            //        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11]
            //    },
            //    action: newExportAction
            //},
            //{
            //    extend: 'print',
            //    text: '<button class="btn btn-default btn-sm"><i class="fa fa-print" style="color:#626262;"></i> Print</button>',
            //    titleAttr: 'Imprimir',
            //    exportOptions: {
            //        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11]
            //    },
            //    action: newExportAction
            //}
        ],
        "lengthChange": true,
        "bDestroy": true,
    });
});

function SubmitAdd(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        var data = $(form).serializeJSON();
        data = JSON.stringify(data);
        
        $.ajax({
            type: 'PUT',
            url: apiurl,
            data: data,
            contentType: 'application/json',
            success: function (data) {
                if (data.success) {
                    popup.modal('hide');
                    ShowMessage(data.message);
                    dataTable.ajax.reload(null, false);
                } else {
                    ShowMessageError(data.message);
                }
            }
        });

    }
    return false;
}

function SubmitEdit(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        var data = $(form).serializeJSON();
        var id = data.id_tall;
        data = JSON.stringify(data);
        
        $.ajax({
            type: 'PUT',
            url: apiurl + '/' + id,
            data: data,
            contentType: 'application/json',
            success: function (data) {
                if (data.success) {
                    popup.modal('hide');
                    ShowMessage(data.message);
                    dataTable.ajax.reload(null, false);
                } else {
                    ShowMessageError(data.message);
                }
            }
        });

    }
    return false;
}

function ShowPopup(url) {
    var modalId = 'modalDefault';
    var modalPlaceholder = $('#' + modalId + ' .modal-dialog .modal-content');
    $.get(url)
        .done(function (response) {
            modalPlaceholder.html(response);
            popup = $('#' + modalId + '').modal({
                keyboard: false,
                backdrop: 'static'
            });
        });
}

function Delete(id) {
    swal({
        title: "¿Desea eliminar el Taller seleccionado?",
        //text: "You will not be able to restore the data!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#dd4b39",
        cancelButtonText: "Volver",
        confirmButtonText: "Eliminar",
        closeOnConfirm: true
    }, function () {
        $.ajax({
            type: 'DELETE',
            url: apiurl + '/' + id,
            success: function (data) {
                if (data.success) {
                    ShowMessage(data.message);
                    dataTable.ajax.reload();
                } else {
                    ShowMessageError(data.message);
                }
            }
        });
    });
}
