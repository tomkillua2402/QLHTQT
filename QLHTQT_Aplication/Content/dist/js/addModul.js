
$(document).ready(function () {
    try {
        if (window.history && window.history.pushState) {
            $(window).on('popstate', function () {
                f_initModul();
            });
        }

        f_initModul();
    } catch (e) { console.log(e); }
});

function f_initModul() {
    try {
        var urlOld = f_loadUrl();
        if (urlOld != "") {
            f_loadModulesToMaster(urlOld, true);
        }
        else {
            f_loadModulesToMaster('Home', true);
            
        }
    }
    catch (e) { console.log(e); }
}

function f_loadUrl() {
    var hash = window.location.hash;
    hash = hash.replace('#', '');

    var end = hash.indexOf('?');
    if (end == -1)
        end = hash.length;

    return hash.substring(0, end);
}
var preload = '<div id="loading" class="loader">Loading...</div>'
function f_loadModulesToMaster(link, DeleteOld) {
    console.log(link);
    console.log(DeleteOld);
    if (DeleteOld == undefined)
        DeleteOld = true;

    //H.showLoading();
    try {
        var linkHtml = "Views/" + link + "/Index.cshtml",
            linkJs = "Views/" + link + "/index.js",
            addTo = $('#main_content');
       
        if (DeleteOld) {
            addTo.empty();
        }

        // Get the HTML
        $.get(encodeURI(linkHtml), function (html) {

            addTo.append(html);
            console.log(encodeURI(linkHtml));

            $.getScript(encodeURI(linkJs), function () {
                //H.hideLoading();

                if ($('#breadcrumbTopMenu2').length > 0) {
                    $('#breadcrumbTopMenu').html($('#breadcrumbTopMenu2').html());
                    $('#breadcrumbTopMenu2').remove();
                }
            });
        });

    }
    catch (e) { console.log(e); }
}
