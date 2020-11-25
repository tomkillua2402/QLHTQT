var http_arr = new Array();
function doUpload(id, callback) {
    debugger
    var files = document.getElementById(id).files;
    for (i = 0; i < files.length; i++) {
        uploadFile(id, files[i], i, function (fn) {
            callback(fn);
        });
    }
    return false;
}

function uploadFile(id, file, index, callback) {
    var http = new XMLHttpRequest();
    http_arr.push(http);

    var data = new FormData();
    data.append('filename', file.name);
    data.append('myfile', file);
    data.append('accept', $('#' + id).attr("accept"));
    http.open('POST', '/UploadFile/UploadFiles?type=' + $("#displaytype_" + id).val());
    http.send(data);
    http.onreadystatechange = function (event) {
        if (http.readyState == 4 && http.status == 200) {
            try {
                var server = JSON.parse(http.responseText);
                if (server.status) {
                    debugger
                    renderFileUpload(id, server.files[0], $("#displaytype_" + id).val());
                    callback(server.files);
                    document.getElementById("progress-group").innerHTML = '';
                } else {
                    document.getElementById("progress-group").innerHTML = server.message;
                }
            } catch (e) {
                document.getElementById("progress-group").innerHTML = 'Có lỗi xảy ra';
            }
            document.getElementsByClassName("loading").display = 'none';

        }
        http.removeEventListener('progress', null);
    }
}

function cancelUpload() {
    for (i = 0; i < http_arr.length; i++) {
        http_arr[i].abort();
    }
}

function renderFileUpload(id, path, type) {
    debugger

    switch (type) {
        case 'File':
            return renderFileItem(id, path);
        default:
            return renderImageItem(id, path, '');
    };
}
function renderImageItem(id, path, type) {
    var arrpath = path.split(',');
    if ($("#" + id).attr('data-overwrite') == 'False') {
        for (var i = 0; i < arrpath.length; i++) {
            $("." + id).append('<div class="col-md-3 upload-item"><img src="' + arrpath[i] + '" /><a href="javascript:void(0)" onclick="remove_file(\'' + id + '\', \'' + arrpath[i] + '\', $(this))"><i class="fa fa-times"></i></a></div>');
        }
    }
    else {
        var sbHtml = '';
        sbHtml += '<div class="col-md-12 upload-item">';
        sbHtml += '<img src="' + path + '"/>';
        sbHtml += '<a href="javascript:void(0)" onclick="remove_file(\'' + id + '\', \'' + path + '\', $(this))"><i class="fa fa-times"></i></a>';
        sbHtml += '</div>';
        $("." + id).html(sbHtml);
    }
}
function renderFileItem(id, path) {
    debugger
    if ($("#" + id).attr('data-overwrite') == 'False') {
        if (Array.isArray(path)) {
            var arr = [];
            for (var i = 0; i < path.length; i++) {
                $("." + id).append('<div class="upload-file-item"><div class="col-md-12"><input class="form-control" type="text" name="filename" value="' + path[i].Name + '"></div><a href="javascript:void(0)" onclick="remove_file(\'' + id + '\', \'' + path[i].Url + '\', $(this))"><i class="fa fa-times"></i></a></div>');
            }
        } //<div class="col-md-6"><a style="overflow-wrap: break-word;">' + path[i].Url + '</a></div>
        else {
            var sbHtml = '';
            sbHtml += '<div class="upload-file-item">'
            //sbHtml += '<div class="col-md-6">';
            //sbHtml += '<a style="overflow-wrap: break-word;">' + path + '</a>';
            //sbHtml += '</div>';
            sbHtml += '<div class="col-md-12">';
            sbHtml += '<input class="form-control" type="text" name="filename" value="' + path + '">';
            sbHtml += '</div>';
            sbHtml += '<a href="javascript:void(0)" onclick="remove_file(\'' + id + '\', \'' + path + '\', $(this))"><i class="fa fa-times"></i></a>';
            sbHtml += '</div>';
            $("." + id).append(sbHtml);
        }
    }
}
function remove_file(id, file, self) {
    debugger
    var arrFile = $("#" + id.split('_')[0]).val().split(',');
    arrFile = $.grep(arrFile, function (item) {
        return file != item;
    });
    $("#" + id.split('_')[0]).val(arrFile.join(','));
    self.parent().remove();
    $("#" + id).val('');
}
