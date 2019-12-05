var popup, dataTable;
var entity = 'Usuarios';
var apiurl = 'api/' + entity;

$(document).ready(function () {
    var oldExportAction = function (self, e, dt, button, config) {
        if (button[0].className.indexOf('buttons-excel') >= 0 || button[0].className.indexOf('buttons-csv') >= 0 || button[0].className.indexOf('buttons-pdf') >= 0) { 
            if ($.fn.dataTable.ext.buttons.excelHtml5.available(dt, config)) {
                $.fn.dataTable.ext.buttons.excelHtml5.action.call(self, e, dt, button, config);
            }
            else {
                $.fn.dataTable.ext.buttons.excelFlash.action.call(self, e, dt, button, config);
            }
        } else if (button[0].className.indexOf('buttons-print') >= 0) {
            $.fn.dataTable.ext.buttons.print.action(e, dt, button, config);
        }
    };
    var newExportAction = function (e, dt, button, config) {
        var self = this;
        var oldStart = dt.settings()[0]._iDisplayStart;

        dt.one('preXhr', function (e, s, data) {
            // Just this once, load all data from the server...
            data.start = 0;
            data.length = 2147483647;

            dt.one('preDraw', function (e, settings) {
                // Call the original action function 
                oldExportAction(self, e, dt, button, config);

                dt.one('preXhr', function (e, s, data) {
                    // DataTables thinks the first item displayed is index 0, but we're not drawing that.
                    // Set the property to what it was before exporting.
                    settings._iDisplayStart = oldStart;
                    data.start = oldStart;
                });

                // Reload the grid with the original page. Otherwise, API functions like table.cell(this) don't work properly.
                setTimeout(dt.ajax.reload, 0);

                // Prevent rendering of the full data to the DOM
                return false;
            });
        });

        // Requery the server with the new one-time export settings
        dt.ajax.reload();
    };

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
        //"scrollX": true,
        "responsive": true,
        "columns": [
            { "data": "nombre_usu" },
            { "data": "email" },
            {
                "data": "tipo_usu",
                "render": function (data) {
                    if (data == "1") {
                        return "ADM";
                    } else if(data == "0") {
                        return "SAC";
                    }else {
                        return "TAL";
                    }
                }
            },
            {
                "data": "pais_usu",
                "render": function (data) {
                    if (data == "1") {
                        return "PORTUGAL";
                    } else if (data == "0") {
                        return "ESPAÑA";
                    } else {
                        return "PENINSULA";
                    }
                }   
            },
            {
                "data": "activo_usu",
                "render": function (data) {
                    var activeLabel = "";
                    if (data == 1) {
                        activeLabel = "<span class='label label-success'>Activar</span>";
                    } else {
                        activeLabel = "<span class='label label-danger'>Desactivar</span>";
                    }
                    return activeLabel;
                }
            },
            {
                "data": "id",
                "render": function (data, type, row, meta) {
                    var btnEdit = "<a onclick=ShowPopup('" + entity + "/Edit/" + data + "')><i class='fa fa-pencil'></i>Editar</a>";
                    var status = row['activo_usu'];
                    if (status == "1") {
                        var btnDeactivate = "<a onclick=Deactivate('" + data + "')><i class='fa fa-toggle-off'></i>Desactivar</a>";
                    } else {
                        var btnDeactivate = "<a onclick=Deactivate('" + data + "')><i class='fa fa-toggle-on'></i>Activar</a>";
                    }
                    
                    var btnDelete = "<a onclick=Delete('" + data + "')><i class='fa fa-trash'></i>Borrar</a>";
                    var btnChangePassword = "<a onclick=ShowPopup('" + entity + "/UpdatePassword/" + data + "')><i class='fa fa-unlock-alt'></i>Contraseña</a>";
                    var btnActions =
                        "<div class=btn-group style='display: flex;'>" +
                        "<button type='button' class='btn btn-default'> Acciones</button>" +
                        "<button type='button' class='btn btn-default dropdown-toggle' data-toggle=dropdown>" +
                        "<span class='caret'></span>" +
                        "<span class='sr-only'>Toggle Dropdown</span>" +
                        "</button>" +
                        "<ul class='dropdown-menu' role='menu'>" +
                        "<li style='cursor: pointer;'>" + btnEdit + "</li>" +
                        "<li style='cursor: pointer;'>" + btnDeactivate+"</li>" +
                        "<li style='cursor: pointer;'>" + btnDelete + "</li>" +
                        "<li style='cursor: pointer;'>" + btnChangePassword + "</li>" +
                        "</ul>" +
                        "</div >";
                    return btnActions;
                }
            },
        ],
        "columnDefs": [
            { "orderable": false, "targets": [5] }
        ],
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
            //        columns: [0, 1, 2, 3, 4]
            //    },
            //    action: newExportAction
            //},
            //{
            //    extend: 'excelHtml5',
            //    text: '<button class="btn btn-default btn-sm"><i class="fa fa-file-excel-o" style="color:#626262;"></i> Excel</button>',
            //    titleAttr: 'Excel',
            //    exportOptions: {
            //        columns: [0, 1, 2, 3, 4]
            //    },
            //    action: newExportAction
            //},
            //{
            //    extend: 'csvHtml5',
            //    text: '<button class="btn btn-default btn-sm"><i class="fa fa-file-text-o" style="color:#626262;"></i> CSV</button>',
            //    titleAttr: 'CSV',
            //    exportOptions: {
            //        columns: [0, 1, 2, 3, 4]
            //    },
            //    action: newExportAction
            //},
            //{
            //    extend: 'pdfHtml5',
            //    text: '<button class="btn btn-default btn-sm"><i class="fa fa-file-pdf-o" style="color:#626262;"></i> PDF</button>',
            //    titleAttr: 'PDF',
            //    exportOptions: {
            //        columns: [0, 1, 2, 3, 4]
            //    },
            //    action: newExportAction
            //},
            //{
            //    extend: 'print',
            //    text: '<button class="btn btn-default btn-sm"><i class="fa fa-print" style="color:#626262;"></i> Print</button>',
            //    titleAttr: 'Imprimir',
            //    exportOptions: {
            //        columns: [0, 1, 2, 3, 4]
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
        var id = data.Id;
        data = JSON.stringify(data);
        
        $.ajax({
            type: 'PUT',
            url: apiurl + '/' + id,
            data: data,
            contentType: 'application/json',
            success: function (data) {
                if (data.success) {
                    if (data.message.errors) {
                        if (data.message.errors[0].code == "DuplicateUserName") {
                            ShowMessageError("Email del usuario ya en uso");
                        } else {
                            ShowMessageError(data.message.errors[0].description);
                        }
                        
                    } else {
                        popup.modal('hide');
                        ShowMessage(data.message);
                        dataTable.ajax.reload(null, false);
                    }
                } else {
                    ShowMessageError(data.message);
                }
            }
        });

    }
    return false;
}

function UpdatePassword(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        var data = $(form).serializeJSON();
        var id = data.Id;
        data = JSON.stringify(data);

        $.ajax({
            type: 'PATCH',
            url: 'api/ChangePassword/' + id,
            data: data,
            contentType: 'application/json',
            success: function (data) {
                if (data.success) {
                    popup.modal('hide');
                    ShowMessage(data.message);
                    dataTable.ajax.reload();
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
        title: "¿Desea eliminar el usuario seleccionado?",
        //text: "¿Desea eliminar el usuario seleccionado?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#dd4b39",
        confirmButtonText: "Eliminar",
        cancelButtonText: "Volver",
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

function Deactivate(id) {
    swal({
        title: "Desea cambiar el estado del usuario?",
        type: "info",
        showCancelButton: true,
        cancelButtonText: "Volver",
        cancelButtonColor: "#f2380a",
        confirmButtonColor: "#19b02f",
        confirmButtonText: "Actualizar",
        closeOnConfirm: true
    }, function () {
        $.ajax({
            type: 'POST',
            url: 'api/Deactivate/' + id,
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