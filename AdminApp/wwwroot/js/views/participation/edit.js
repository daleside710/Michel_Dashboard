var adjunto1_par = "";
var adjunto2_par = "";
var adjunto3_par = "";
var adjunto4_par = "";
var adjunto5_par = "";

$(".upload_area").on('click', ' .reset', function () {
	$(this).parent().find("input[type=hidden]").val('');
	$(this).parent().find(".adjunto").val('');
	$(this).hide();

	$(this).parent().find(".adjunto-file p").html('<u>Selecciona</u> o arrastra un archivo<br />(M&aacute;x 4MB, JPG/PNG/PDF)');
});

$(".upload_area").on('click', '.adjunto-file', function () {
	$(this).parent().find("input[class=adjunto]").click();
	$(this).removeClass("borderdotLine");
	$(".titError2").hide();
});

// file selected
$(".upload_area").on('change', '.adjunto', function () {
	var fd = new FormData();
	var file = $(this)[0].files[0];
	var archivo = this.files[0];
	var extension = file.name.split('.').pop();
	var p_tag;
	if ((extension.toLowerCase() === "pdf" || extension.toLowerCase() === "jpg" || extension.toLowerCase() === "png") && file.size < 4000000) {
		$("#msg_vista_configuracion_archivos").html('');
		p_tag = $(this).parent().find("p");
		p_tag.html('<em>Subiendo adjunto...</em><br />El proceso puede tardar unos minutos.');
		fd.append('file', file);

		uploadFile(fd, archivo, p_tag);
		$(".first-file").removeClass("borderdotLine");
	} else {
		p_tag = $(this).parent().find("p");
		p_tag.parent().addClass("borderdotLine");

		p_tag.html('<u>Seleccione</u> o arrastre un archivo<br />(Máx 4MB, JPG/PNG/PDF)');
		var msg = "El archivo no cumple con los requisitos de tamaño o extensión...";
		$(".titError2").html(msg);
		$(".titError2").show();

		switch (p_tag.attr('class')) {
			case "p1":
				adjunto1_par = "";
				break;
			case "p2":
				adjunto2_par = "";
				break;
			case "p3":
				adjunto3_par = "";
				break;
			case "p4":
				adjunto4_par = "";
				break;
			case "p5":
				adjunto5_par = "";
				break;
			default:
				adjunto1_par = "";
				break;
		}
	}
});

$(".adduploadbtn").on('click', function () {
	var count = $(".upload_area > div").length;
	if (count > 5) {
		return false;
	}
	var uploadbtn = '<div class="col-xl-6 col-lg-6 col-md-6 col-sm-12 col-xs-12 frmCol">' +
		'<label class="control-label etFrm" > Subir imagen ' + count + '</label >' +
		'<div class="form-group">' +
		'<input type="file" name="ImageFiles" class="adjunto" >' +
		//'<input type="hidden" name="adjunto'+count+'_par"/>' + 
		'<div class="upload-area adjunto-file">' +
		'<p class="p' + count + '"><u>Selecciona</u> o arrastra un archivo<br />(M&aacute;x 4MB, JPG/PNG/PDF)</p>' +
		'</div>' +
		'</div>' +
		'</div>';
	$(".adduploadbtn-area").before(uploadbtn);
	if (count > 4) {
		$(".adduploadbtn").css({ "opacity": 0.4, "cursor": "default" });
		return false;
	}
});

function uploadFile(formdata, archivo, p_tag) {
	$.ajax({
		url: "api/Upload",
		data: formdata,
		processData: false,
		contentType: false,
		type: "POST",
		success: function (response) {
			if (response.success) {
				p_tag.html("<strong>" + archivo.name + "</strong>");

				var filename = response.tempfileName + ":" + archivo.name;
				switch (p_tag.attr('class')) {
					case "p1":
						adjunto1_par = filename;
						$("input[name*='adjunto1_par']").val(filename);
						break;
					case "p2":
						adjunto2_par = filename;
						$("input[name*='adjunto2_par']").val(filename);
						break;
					case "p3":
						adjunto3_par = filename;
						$("input[name*='adjunto3_par']").val(filename);
						break;
					case "p4":
						adjunto4_par = filename;
						$("input[name*='adjunto4_par']").val(filename);
						break;
					case "p5":
						adjunto5_par = filename;
						$("input[name*='adjunto5_par']").val(filename);
						break;
					default:
						adjunto1_par = filename;
						$("input[name*='adjunto1_par']").val(filename);
						break;
				}

				$(p_tag).parent().parent().parent().find('button').show();
			} else {
				ShowMessageError(response.message);
				adjunto_par = "";
			}
		}
	});
}

function AttachmentValidar() {

	var param = {
		"adjunto1_par": $("input[name*='adjunto1_par']").val(),
		"adjunto2_par": $("input[name*='adjunto2_par']").val(),
		"adjunto3_par": $("input[name*='adjunto3_par']").val(),
		"adjunto4_par": $("input[name*='adjunto4_par']").val(),
		"adjunto5_par": $("input[name*='adjunto5_par']").val()
	};
    
	$.ajax({
		type: 'POST',
		dataType: "json",
		url: 'api/AttachmentValidar/' + id_par,
		contentType: 'application/json',
		data: JSON.stringify(param),
		success: function (data) {
			if (data.success) {
				ShowMessage("Adjuntos actualizados con éxito");

				var html = '';

				var length = data.files.length;
				for (var i = 0; i < length; i++) {
					html += '<i class="fa fa-sticky-note-o" style="padding-right: 10px;"></i>';
					html += '<a onclick="downloadFile(' + data.id_par + ', \'' + data.files[i].filename + '\')" href="javascript:void(0);">' + data.files[i].label + '</a>';
					html += '<br>';
				}

                $('#attachment_links').html(html);

                // After click Actualizar Adjuntos button
                $("input[name*='adjunto1_par']").val($(".p1 strong").html());
                $("input[name*='adjunto2_par']").val($(".p2 strong").html());
                $("input[name*='adjunto3_par']").val($(".p3 strong").html());
                $("input[name*='adjunto4_par']").val($(".p4 strong").html());
                $("input[name*='adjunto5_par']").val($(".p5 strong").html());
                
			} else {
				ShowMessageError(data.message);
			}
		}
	});
}