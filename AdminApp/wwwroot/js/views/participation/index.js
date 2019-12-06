var popup, dataTable;
var entity = 'Participaciones';
var apiurl = 'api/' + entity;
var filter_option = "todos";
$(document).ready(function () {

    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        $.fn.dataTable.tables({ visible: true, api: true }).columns.adjust();
    });
    $('body').on('change', '#filter_select', function () {
        console.log($(this).val());
        filter_option = $(this).val();
    });
    
    dataTable = $('#grid').DataTable({
        "initComplete": function () {
            var input = $('.dataTables_filter input').unbind(),
                self = this.api(),
                $searchButton = $('<button class="btn btn-default">')
                    .text('Buscar')
                    .click(function () {
                        self.search(input.val()).draw();
                    })
            $('.dataTables_filter').append($searchButton);
            var $filterOption = $('<label style="margin-left:20px;">Filter Option: <select class="form-control input-sm" id="filter_select">'+
                '<option value="todos">TODOS</option>'+
                '<option value="id_par">ID</option>'+
                '<option value="registrofecha_par">Fecha Registro</option>'+
                '<option value="registrohora_par">Hora Registro</option>'+
                '<option value="nombre_par">Nombre</option>'+
                '<option value="apellidos_par">Apellidos</option>'+
                '<option value="telefono_par">Teléfono</option>'+
                '<option value="email_par">Email</option>'+
                '<option value="dni_par">DNI</option>'+
                '<option value="regalo_par">Regalo</option>'+
                '<option value="pais_par">País</option>'+
                '<option value="estado_par">Estado</option>'+
                '</select></label>');
            $('.dataTables_length').append($filterOption);
        },
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": apiurl,
            "type": 'POST',
            "datatype": 'json',
            "data": function (request) {
                request.filter = filter_option;
            }
        },
        "responsive": true,
        "order": [[0, "desc"]],
        "columns": [
            { "data": "id_par" }, 
            {
                "data": "registrofecha_par",
                "render": function (data) {
                    var dateSplit = data.split('T');
                    var dateSplit1 = dateSplit[0].split('-');
                    return dateSplit1[2] + "/" + dateSplit1[1] + "/" + dateSplit1[0];
                }
            },
            { "data": "registrohora_par" }, 
            { "data": "nombre_par" },
            { "data": "apellidos_par" },
            { "data": "telefono_par" }, 
            {
                "data": "email_par",
                "render": function (data) {
                    return data.toLowerCase();
                }
            }, 
            { "data": "dni_par" },
            {
                "data": "premioSelFrnt_par",
                "render": function (data, type, row, meta) {
                    var valorpremio_par = row["valorpremio_par"];
                    
                    if (valorpremio_par == null) {
                        if (row['regalo']) {
                            return row['regalo']['producto'];
                        }
                    } else {
                        return valorpremio_par;
                    }
                    
                    return "";
                }
            },
            { "data": "pais_par" },
            {
                "data": "id_est",
                "render": function (data, type, row, meta) {
                    var id_est = row['id_est'];
                    var solicitar_adjunto = row['solicitar_adjunto'];
                    var adjunto_adjunto = row['adjunto_adjunto'];
                    if (id_est == "3") {
                        return '<p style="color: #C40F12">Rechazado</p>';
                    } else if (id_est == "2") {
                        return '<p style="color: #079328">Validado</p>';
                    } else if (id_est == "1") {
                        if (solicitar_adjunto == "1" && adjunto_adjunto == "1") {
                            return '<p style="color: #EC721F">Actualizando</p>';
                        } else if (solicitar_adjunto == "1" && adjunto_adjunto == "0") {
                            return '<p style="color: #EC721F">Modificando</p>';
                        } else {
                            return '<p style="color: #EC721F">Pendiente</p>';
                        }
                    } else {
                        return "-";
                    }
                }
            },
            {
                "data": "id_par",
                "render": function (data) {
                    var btnEdit = "<div><a onclick=ShowPopup('" + entity + "/Edit/" + data + "') class='btn btn-block btn-default btn-sm'><i class='fa fa-edit'></i> Detalles</a></div>";
                    return btnEdit;
                }
            },
        ],
        "columnDefs": [
            {
                "orderable": false, "targets": [11],
            },
        ],
        "fixedColumns": true,
        "dom": 'Blfrtip',
        "language": {
            "emptyTable": "no data found.",
            "processing": "<i class='fa fa-spinner fa-spin fa-3x fa-fw'>"
        },
        "buttons": [],
        "lengthChange": true,
        "bDestroy": true,
    });
});

/**
 * 
 * Main Edit Action
 */

function SubmitEdit(form) {
    $('.alert').remove();
    $.validator.unobtrusive.parse(form);
    
    if ($(form).valid()) {
        var data = $(form).serializeJSON();
        var id = data.id_par;
        
		var nombre_par = data.nombre_par;
		var apellidos_par = data.apellidos_par;
        var factura1_par = data.factura1_par;
        var factura2_par = data.factura2_par;
        var fechaCompra1_par = data.fechaCompra1_par;
		var fechaCompra2_par = data.fechaCompra2_par;
		var errorMsgContainer;
		var errorMsg = "Se deben completar los datos siguientes para poder Actualizar: Nombre y Apellidos, Nº Factura, Fecha, Modelo Neumático y Dimensión.";

		if (nombre_par === "" || apellidos_par === "" ||
			factura1_par === "" && factura2_par === "") {

            errorMsgContainer = '<div class="alert alert-danger alert-dismissible" role="alert">'+
                '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button >'+
                errorMsg +
                '</div>';
            $('#par_edit').prepend(errorMsgContainer);
            return false;
        }

        if (fechaCompra1_par === "" && fechaCompra2_par === "") {
            errorMsgContainer = '<div class="alert alert-danger alert-dismissible" role="alert">' +
                '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button >' +
                errorMsg +
                '</div>';
            $('#par_edit').prepend(errorMsgContainer);
            return false;
        }

        data = JSON.stringify(data);
        
        $.ajax({
            type: 'POST',
            url: apiurl + '/' + id,
            data: data,
            contentType: 'application/json',
            success: function (data) {
                if (data.success) {
                    //popup.modal('hide');
                    ShowMessage(data.message);
					dataTable.ajax.reload(null, false);
					$('#fullname').text(nombre_par + ' ' + apellidos_par);
                } else {
                    if (data.message == "duplicate") {
                        errorMsg = "Factura validada para el mismo taller en la participación #" + data.validated_id_par;
                        console.log(errorMsg);
                        var errorMsgContainer = '<div class="alert alert-danger alert-dismissible" role="alert">' +
                            '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button >' +
                            errorMsg +
                            '</div>';
                        $('#par_edit').prepend(errorMsgContainer);
                        return false;
                    }
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
function downloadFile(id, filename) {
    window.open(entity + "/DownloadFile?id=" + id + "&filename=" + filename, '_blank');
}

